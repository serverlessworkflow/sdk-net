using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using ServerlessWorkflow.Sdk.Models.Calls;

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute OpenAPI <see cref="CallTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="httpClientFactory">The service used to create <see cref="HttpClient"/>s</param>
/// <param name="authenticationHandler">The service used to handle authentication policies</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class OpenApiCallTaskExecutor(IServiceProvider serviceProvider, ILogger<OpenApiCallTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IHttpClientFactory httpClientFactory, IAuthenticationHandler authenticationHandler, ITaskExecutionContext<CallTaskDefinition> task)
    : TaskExecutor<CallTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    OpenApiCallDefinition? openApi;
    OpenApiDocument? document;
    OpenApiOperation? operation;
    HttpMethod httpMethod = null!;
    List<string> servers = null!;
    IDictionary<string, object>? parameters;
    string? path;
    string? query;
    readonly Dictionary<string, string> headers = [];
    readonly Dictionary<string, string> cookies = [];
    object? body;

    /// <inheritdoc/>
    protected override async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        openApi = JsonSerializer.Deserialize(Task.Definition.With!, Sdk.Serialization.Json.JsonSerializationContext.Default.OpenApiCallDefinition) ?? throw new InvalidOperationException("Failed to deserialize OpenAPI call definition from 'with'");
        var documentEndpointUri = openApi.Document.Endpoint.Match(
            endpoint => endpoint.Uri,
            uri => uri
        );
        var documentAuthentication = openApi.Document.Endpoint.Match(
            endpoint => endpoint.Authentication,
            _ => null
        );
        using var httpClient = httpClientFactory.CreateClient();
        if (documentAuthentication != null)
        {
            var authResult = await authenticationHandler.HandleAsync(documentAuthentication, Task.Workflow.Definition, cancellationToken).ConfigureAwait(false);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authResult.Scheme, authResult.Value);
        }
        using var request = new HttpRequestMessage(HttpMethod.Get, documentEndpointUri);
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            logger.LogError("Failed to retrieve the OpenAPI document at location '{uri}'. The remote server responded with a non-success status code '{statusCode}'.", documentEndpointUri, response.StatusCode);
            if (logger.IsEnabled(LogLevel.Debug)) logger.LogDebug("Response content:\r\n{responseContent}", responseContent ?? "None");
            response.EnsureSuccessStatusCode();
        }
        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        document = new OpenApiStreamReader().Read(responseStream, out _);
        var operationId = openApi.OperationId;
        if (operationId.IsRuntimeExpression()) operationId = await Task.Workflow.Expressions.EvaluateAsync<string>(operationId, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        var op = document.Paths
            .SelectMany(p => p.Value.Operations)
            .FirstOrDefault(o => o.Value.OperationId == operationId);
        if (op.Value == null) throw new NullReferenceException($"Failed to find an operation with id '{operationId}' in OpenAPI document at '{documentEndpointUri}'");
        httpMethod = ToHttpMethod(op.Key);
        operation = op.Value;
        servers = document.Servers.Select(s => s.Url).ToList();
        if (servers.Count == 0) servers.Add(documentEndpointUri.GetLeftPart(UriPartial.Authority));
        var pathEntry = document.Paths.Single(p => p.Value.Operations.Any(o => o.Value.OperationId == operation.OperationId));
        path = pathEntry.Key;
        await BuildParametersAsync(cancellationToken).ConfigureAwait(false);
        if (parameters == null || parameters.Count < 1) return;
        var allParameters = pathEntry.Value.Parameters.ToList();
        allParameters.AddRange(operation.Parameters);
        foreach (var param in allParameters.Where(p => p.In == ParameterLocation.Cookie))
        {
            if (parameters.TryGetValue(param.Name, out var value) && value != null) cookies.Add(param.Name, value.ToString()!);
            else if (param.Required) throw new NullReferenceException($"Failed to find the definition of the required parameter '{param.Name}' in the OpenAPI operation with id '{operationId}'");
        }
        foreach (var param in allParameters.Where(p => p.In == ParameterLocation.Header))
        {
            if (parameters.TryGetValue(param.Name, out var value) && value != null) headers.Add(param.Name, value.ToString()!);
            else if (param.Required) throw new NullReferenceException($"Failed to find the definition of the required parameter '{param.Name}' in the OpenAPI operation with id '{operationId}'");
        }
        foreach (var param in allParameters.Where(p => p.In == ParameterLocation.Path))
        {
            if (parameters.TryGetValue(param.Name, out var value) && value != null) path = path.Replace($"{{{param.Name}}}", value.ToString());
            else if (param.Required) throw new NullReferenceException($"Failed to find the definition of the required parameter '{param.Name}' in the OpenAPI operation with id '{operationId}'");
        }
        var queryParameters = new Dictionary<string, string>();
        foreach (var param in allParameters.Where(p => p.In == ParameterLocation.Query))
        {
            if (parameters.TryGetValue(param.Name, out var value) && value != null) queryParameters.Add(param.Name, value.ToString()!);
            else if (param.Required) throw new NullReferenceException($"Failed to find the definition of the required parameter '{param.Name}' in the OpenAPI operation with id '{operationId}'");
        }
        query = string.Join("&", queryParameters.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        if (operation.RequestBody != null)
        {
            if (parameters.TryGetValue("body", out var bodyValue) && bodyValue != null)
            {
                body = bodyValue;
            }
            else
            {
                body = parameters;
            }
            if (body == null && operation.RequestBody.Required) throw new NullReferenceException($"Failed to determine the required body parameter for the OpenAPI operation with id '{operationId}'");
        }
    }

    async Task BuildParametersAsync(CancellationToken cancellationToken = default)
    {
        if (openApi == null || operation == null) throw new InvalidOperationException("The executor must be initialized before execution");
        if (openApi.Parameters == null) return;
        var arguments = GetExpressionEvaluationArguments();
        var evaluated = await Task.Workflow.Expressions.EvaluateAsync(openApi.Parameters, Task.Input, arguments, cancellationToken).ConfigureAwait(false);
        if (evaluated is JsonObject jsonObject)
        {
            parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            foreach (var property in jsonObject) parameters[property.Key] = property.Value?.Deserialize<object>()!;
        }
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (openApi == null || operation == null) throw new InvalidOperationException("The executor must be initialized before execution");
        JsonNode? output = null;
        var success = false;
        foreach (var server in servers)
        {
            var requestUri = $"{server}{path}";
            if (requestUri.StartsWith("//")) requestUri = $"https:{requestUri}";
            if (!string.IsNullOrWhiteSpace(query)) requestUri += $"?{query}";
            using var request = new HttpRequestMessage(httpMethod, requestUri);
            foreach (var header in headers) request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            if (cookies.Count > 0) request.Headers.Add("Cookie", string.Join(";", cookies.Select(kvp => $"{kvp.Key}={kvp.Value}")));
            if (body != null)
            {
                var bodyJson = body is JsonNode jsonNode ? jsonNode.ToJsonString() : JsonSerializer.Serialize(body);
                request.Content = new StringContent(bodyJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            }
            using var httpClient = httpClientFactory.CreateClient();
            if (openApi.Authentication != null)
            {
                var authResult = await authenticationHandler.HandleAsync(openApi.Authentication, Task.Workflow.Definition, cancellationToken).ConfigureAwait(false);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authResult.Scheme, authResult.Value);
            }
            using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.ServiceUnavailable) continue;
            var successRange = openApi.Redirect ? 399 : 299;
            if ((int)response.StatusCode < 200 || (int)response.StatusCode > successRange)
            {
                var detail = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                logger.LogError("Failed to execute the OpenAPI operation '{operationId}' at '{uri}'. The remote server responded with a non-success status code '{statusCode}'.", operation.OperationId, response.RequestMessage!.RequestUri, response.StatusCode);
                if (logger.IsEnabled(LogLevel.Debug)) logger.LogDebug("Response content:\r\n{responseContent}", detail ?? "None");
                await SetErrorAsync(RuntimeError.Communication(new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute), (ushort)response.StatusCode, detail), cancellationToken).ConfigureAwait(false);
                return;
            }
            var responseText = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (!string.IsNullOrWhiteSpace(responseText))
            {
                try { output = JsonNode.Parse(responseText); }
                catch { output = JsonValue.Create(responseText); }
            }
            output = openApi.Output switch
            {
                HttpOutputFormat.Response => JsonSerializer.SerializeToNode(new HttpResponse()
                {
                    Request = new()
                    {
                        Method = request.Method.Method,
                        Uri = request.RequestUri!,
                        Headers = [.. request.Headers.ToDictionary(h => h.Key, h => string.Join(',', h.Value))]
                    },
                    Headers = [.. response.Headers.ToDictionary(h => h.Key, h => string.Join(',', h.Value))],
                    StatusCode = (int)response.StatusCode,
                    Content = output
                })?.AsObject(),
                _ => output
            };
            success = true;
            break;
        }
        if (!success) throw new HttpRequestException($"Failed to execute the Open API operation with id '{operation.OperationId}': No service available", null, HttpStatusCode.ServiceUnavailable);
        await SetResultAsync(output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    static HttpMethod ToHttpMethod(OperationType operationType) => operationType switch
    {
        OperationType.Get => HttpMethod.Get,
        OperationType.Post => HttpMethod.Post,
        OperationType.Put => HttpMethod.Put,
        OperationType.Delete => HttpMethod.Delete,
        OperationType.Options => HttpMethod.Options,
        OperationType.Head => HttpMethod.Head,
        OperationType.Patch => HttpMethod.Patch,
        OperationType.Trace => HttpMethod.Trace,
        _ => throw new NotSupportedException($"The specified operation type '{operationType}' is not supported")
    };

}
