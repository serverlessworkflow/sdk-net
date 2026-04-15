// Copyright © 2024-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using ServerlessWorkflow.Sdk.Models.Calls;
using System.Reflection;

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute HTTP <see cref="CallTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="httpClientFactory">The service used to create <see cref="HttpClient"/>s</param>
/// <param name="authenticationHandler">The service used to handle authentication policies</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class HttpCallTaskExecutor(IServiceProvider serviceProvider, ILogger<HttpCallTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IHttpClientFactory httpClientFactory, IAuthenticationHandler authenticationHandler, ITaskExecutionContext<CallTaskDefinition> task)
    : TaskExecutor<CallTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    HttpCallDefinition? http;
    AuthenticationPolicyDefinition? authentication;

    /// <inheritdoc/>
    protected override async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        try
        {
            http = JsonSerializer.Deserialize(Task.Definition.With!, Sdk.Serialization.Json.JsonSerializationContext.Default.HttpCallDefinition) ?? throw new InvalidOperationException("Failed to deserialize HTTP call definition from 'with'");
            authentication = http.Endpoint.Match(
                endpoint => endpoint.Authentication,
                _ => null
            );
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while initializing the HTTP call task '{task}': {ex}", Task.Instance.Reference, ex);
            await SetErrorAsync(Error.Validation(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), $"Invalid/missing call parameters for function 'http': {ex.Message}"), cancellationToken).ConfigureAwait(false);
        }
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (http == null) throw new InvalidOperationException("The executor must be initialized before execution");
        var arguments = GetExpressionEvaluationArguments();
        var defaultMediaType = http.Body is JsonValue value && value.GetValueKind() == JsonValueKind.String ? MediaTypeNames.Text.Plain : MediaTypeNames.Application.Json;
        var mediaType = defaultMediaType;
        if (http.Headers != null && http.Headers.TryGetValue("Content-Type", out var contentType) && !string.IsNullOrWhiteSpace(contentType)) mediaType = contentType.Split(';', StringSplitOptions.RemoveEmptyEntries)[0].Trim();
        HttpContent? requestContent = null;
        if (http.Body != null)
        {
            if (mediaType.StartsWith("text"))
            {
                var rawContent = http.Body.ToString();
                if (!string.IsNullOrWhiteSpace(rawContent) && rawContent.IsRuntimeExpression()) rawContent = await Task.Workflow.Expressions.EvaluateAsync<string>(rawContent, Task.Instance.Input, arguments, cancellationToken).ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(rawContent)) requestContent = new StringContent(rawContent, Encoding.UTF8, mediaType);
            }
            else if (mediaType == MediaTypeNames.Application.Octet)
            {
                var buffer = Convert.FromBase64String(http.Body.ToString()!);
                requestContent = new StreamContent(new MemoryStream(buffer));
            }
            else
            {
                var evaluatedBody = await Task.Workflow.Expressions.EvaluateAsync(http.Body, Task.Instance.Input, arguments, cancellationToken).ConfigureAwait(false);
                if (evaluatedBody != null) requestContent = new StringContent(evaluatedBody.ToJsonString(), Encoding.UTF8, mediaType);
            }
        }
        var endpointUri = http.Endpoint.Match(
            endpoint => endpoint.Uri,
            uri => uri
        );
        using var httpClient = httpClientFactory.CreateClient();
        if (authentication != null)
        {
            var authResult = await authenticationHandler.HandleAsync(authentication, Task.Workflow.Definition, cancellationToken).ConfigureAwait(false);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authResult.Scheme, authResult.Value);
        }
        var parameters = Task.Instance.Input is JsonObject jsonObject ? jsonObject.Where(kvp => kvp.Value is JsonValue).ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.GetValue<object>()) : [];
        var uri = StringFormatter.Format(endpointUri.OriginalString, parameters);
        if (uri.IsRuntimeExpression()) uri = await Task.Workflow.Expressions.EvaluateAsync<string>(uri, Task.Instance.Input, this.GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        using var request = new HttpRequestMessage(new HttpMethod(http.Method), uri) { Content = requestContent };
        if (http.Headers != null)
        {
            foreach (var header in http.Headers)
            {
                var headerValue = header.Value;
                if (headerValue.IsRuntimeExpression()) headerValue = await Task.Workflow.Expressions.EvaluateAsync<string>(headerValue, Task.Instance.Input, arguments, cancellationToken).ConfigureAwait(false);
                request.Headers.TryAddWithoutValidation(header.Key, headerValue);
            }
        }
        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var successRange = http.Redirect ? 399 : 299;
        if ((int)response.StatusCode < 200 || (int)response.StatusCode > successRange)
        {
            var detail = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            logger.LogError("Failed to request '{method} {uri}'. The remote server responded with a non-success status code '{statusCode}'.", http.Method, endpointUri, response.StatusCode);
            if (logger.IsEnabled(LogLevel.Debug)) logger.LogDebug("Response content:\r\n{responseContent}", detail ?? "None");
            await SetErrorAsync(Error.Communication(new Uri(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute), (ushort)response.StatusCode, detail), cancellationToken).ConfigureAwait(false);
            return;
        }
        JsonNode? content = null;
        var responseMediaType = response.Content.Headers.ContentType?.MediaType;
        if (responseMediaType != null)
        {
            if (responseMediaType.Contains("json"))
            {
                var text = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                try { content = JsonNode.Parse(text); }
                catch { content = JsonValue.Create(text); }
            }
            else
            {
                content = JsonValue.Create(await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
            }
        }
        else
        {
            content = JsonValue.Create(await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false));
        }
        var result = http.Output switch
        {
            HttpOutputFormat.Response => JsonSerializer.SerializeToNode(new HttpResponse()
            {
                Request = new()
                {
                    Method = request.Method.Method,
                    Uri = request.RequestUri!,
                    Headers = [.. request.Headers.ToDictionary(h => h.Key, h => string.Join(',', h.Value))]
                },
                StatusCode = (int)response.StatusCode,
                Headers = [.. response.Headers.ToDictionary(h => h.Key, h => string.Join(',', h.Value))],
                Content = content
            })?.AsObject(),
            _ => content
        };
        await SetResultAsync(result, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}

