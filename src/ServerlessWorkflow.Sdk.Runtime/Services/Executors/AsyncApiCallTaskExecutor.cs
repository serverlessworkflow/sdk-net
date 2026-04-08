using Neuroglia.AsyncApi;
using Neuroglia.AsyncApi.Client;
using Neuroglia.AsyncApi.Client.Services;
using Neuroglia.AsyncApi.IO;
using Neuroglia.AsyncApi.v3;
using ServerlessWorkflow.Sdk.Models.Calls;

namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute AsyncAPI <see cref="CallTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="httpClientFactory">The service used to create <see cref="HttpClient"/>s</param>
/// <param name="authenticationHandler">The service used to handle authentication policies</param>
/// <param name="asyncApiDocumentReader">The service used to read <see cref="IAsyncApiDocument"/>s</param>
/// <param name="asyncApiClientFactory">The service used to create <see cref="IAsyncApiClient"/>s</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class AsyncApiCallTaskExecutor(IServiceProvider serviceProvider, ILogger<AsyncApiCallTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IHttpClientFactory httpClientFactory, IAuthenticationHandler authenticationHandler, IAsyncApiDocumentReader asyncApiDocumentReader, IAsyncApiClientFactory asyncApiClientFactory, ITaskExecutionContext<CallTaskDefinition> task)
    : TaskExecutor<CallTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    AsyncApiCallDefinition? asyncApi;
    V3AsyncApiDocument? document;
    KeyValuePair<string, V3OperationDefinition> operation;
    object? messagePayload;
    object? messageHeaders;
    IDisposable? subscription;
    uint? offset;
    bool keepConsume = true;

    static string GetPathFor(uint offset) => $"foreach/{offset}/do";

    /// <inheritdoc/>
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance instance, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(instance, definition, contextData, arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnMessageProcessingErrorAsync(executor, CancellationTokenSource!.Token).ConfigureAwait(false),
            async () => await OnMessageProcessingCompletedAsync(executor, CancellationTokenSource!.Token).ConfigureAwait(false)
        );
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        asyncApi = JsonSerializer.Deserialize(Task.Definition.With!, Sdk.Serialization.Json.JsonSerializationContext.Default.AsyncApiCallDefinition) ?? throw new InvalidOperationException("Failed to deserialize AsyncAPI call definition from 'with'");
        var documentEndpointUri = asyncApi.Document.Endpoint.Match(
            endpoint => endpoint.Uri,
            uri => uri
        );
        var documentAuthentication = asyncApi.Document.Endpoint.Match(
            endpoint => endpoint.Authentication,
            _ => (AuthenticationPolicyDefinition?)null
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
            logger.LogError("Failed to retrieve the AsyncAPI document at location '{uri}'. The remote server responded with a non-success status code '{statusCode}'.", documentEndpointUri, response.StatusCode);
            if (logger.IsEnabled(LogLevel.Debug)) logger.LogDebug("Response content:\r\n{responseContent}", responseContent ?? "None");
            response.EnsureSuccessStatusCode();
        }
        using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        var doc = await asyncApiDocumentReader.ReadAsync(responseStream, cancellationToken).ConfigureAwait(false);
        if (doc is not V3AsyncApiDocument v3Document) throw new NotSupportedException("Only AsyncAPI v3.0.0 is supported at this time");
        document = v3Document;
        if (string.IsNullOrWhiteSpace(asyncApi.Operation)) throw new NullReferenceException("The 'operation' parameter must be set when performing an AsyncAPI v3 call");
        var operationId = asyncApi.Operation;
        if (operationId.IsRuntimeExpression()) operationId = await Task.Workflow.Expressions.EvaluateAsync<string>(operationId, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        if (string.IsNullOrWhiteSpace(operationId)) throw new NullReferenceException("The operation ref cannot be null or empty");
        operation = document.Operations.FirstOrDefault(o => o.Key == operationId);
        if (operation.Value == null) throw new NullReferenceException($"Failed to find an operation with id '{operationId}' in AsyncAPI document at '{documentEndpointUri}'");
        if (asyncApi.Authentication != null)
        {
            //todo: handle AsyncAPI-specific authentication
        }
        switch (operation.Value.Action)
        {
            case V3OperationAction.Receive:
                await BuildMessagePayloadAsync(cancellationToken).ConfigureAwait(false);
                await BuildMessageHeadersAsync(cancellationToken).ConfigureAwait(false);
                break;
            case V3OperationAction.Send: break;
            default: throw new NotSupportedException($"The specified operation action '{operation.Value.Action}' is not supported");
        }
    }

    async Task BuildMessagePayloadAsync(CancellationToken cancellationToken = default)
    {
        if (asyncApi == null || operation.Value == null) throw new InvalidOperationException("The executor must be initialized before execution");
        if (asyncApi.Message?.Payload == null) return;
        var arguments = GetExpressionEvaluationArguments();
        messagePayload = await Task.Workflow.Expressions.EvaluateAsync(asyncApi.Message.Payload, Task.Input, arguments, cancellationToken).ConfigureAwait(false);
    }

    async Task BuildMessageHeadersAsync(CancellationToken cancellationToken = default)
    {
        if (asyncApi == null || operation.Value == null) throw new InvalidOperationException("The executor must be initialized before execution");
        if (asyncApi.Message?.Headers == null) return;
        var arguments = GetExpressionEvaluationArguments();
        messageHeaders = await Task.Workflow.Expressions.EvaluateAsync(asyncApi.Message.Headers, Task.Input, arguments, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (asyncApi == null || document == null || operation.Value == null) throw new InvalidOperationException("The executor must be initialized before execution");
        return operation.Value.Action switch
        {
            V3OperationAction.Receive => DoExecutePublishOperationAsync(cancellationToken),
            V3OperationAction.Send => DoExecuteSubscribeOperationAsync(cancellationToken),
            _ => throw new NotSupportedException($"The specified operation action '{operation.Value.Action}' is not supported"),
        };
    }

    async Task DoExecutePublishOperationAsync(CancellationToken cancellationToken)
    {
        if (asyncApi == null || document == null || operation.Value == null) throw new InvalidOperationException("The executor must be initialized before execution");
        await using var asyncApiClient = asyncApiClientFactory.CreateFor(document);
        var parameters = new AsyncApiPublishOperationParameters(operation.Key, asyncApi.Server, asyncApi.Protocol)
        {
            Payload = messagePayload,
            Headers = messageHeaders
        };
        await using var result = await asyncApiClient.PublishAsync(parameters, cancellationToken).ConfigureAwait(false);
        if (!result.IsSuccessful) throw new Exception("Failed to execute the AsyncAPI publish operation");
        await SetResultAsync(null, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    async Task DoExecuteSubscribeOperationAsync(CancellationToken cancellationToken)
    {
        if (asyncApi == null || document == null || operation.Value == null) throw new InvalidOperationException("The executor must be initialized before execution");
        if (asyncApi.Subscription == null) throw new NullReferenceException("The 'subscription' must be set when performing an AsyncAPI v3 subscribe operation");
        await using var asyncApiClient = asyncApiClientFactory.CreateFor(document);
        var subscribeParams = new AsyncApiSubscribeOperationParameters(operation.Key, asyncApi.Server, asyncApi.Protocol);
        var result = await asyncApiClient.SubscribeAsync(subscribeParams, cancellationToken).ConfigureAwait(false);
        if (!result.IsSuccessful) throw new Exception("Failed to execute the AsyncAPI subscribe operation");
        if (result.Messages == null)
        {
            await SetResultAsync(null, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            return;
        }
        var observable = result.Messages;
        if (asyncApi.Subscription.Consume.For != null) observable = observable.TakeUntil(Observable.Timer(asyncApi.Subscription.Consume.For.ToTimeSpan()));
        if (asyncApi.Subscription.Consume.Amount.HasValue) observable = observable.Take(asyncApi.Subscription.Consume.Amount.Value);
        if (asyncApi.Subscription.Foreach == null)
        {
            var messages = new List<IAsyncApiMessage>();
            await foreach (var m in observable.ToAsyncEnumerable().WithCancellation(cancellationToken).ConfigureAwait(false))
            {
                if (!string.IsNullOrWhiteSpace(asyncApi.Subscription.Consume.While) && !await Task.Workflow.Expressions.EvaluateConditionAsync(asyncApi.Subscription.Consume.While, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false)) break;
                if (!string.IsNullOrWhiteSpace(asyncApi.Subscription.Consume.Until) && await Task.Workflow.Expressions.EvaluateConditionAsync(asyncApi.Subscription.Consume.Until, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false)) break;
                messages.Add(m);
            }
            var messagesJson = JsonSerializer.SerializeToNode(messages);
            await SetResultAsync(messagesJson, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            subscription = observable.TakeWhile(_ => keepConsume).SelectMany(m =>
            {
                OnStreamingMessageAsync(m).GetAwaiter().GetResult();
                return Observable.Return(m);
            }).SubscribeAsync(_ => System.Threading.Tasks.Task.CompletedTask, OnStreamingErrorAsync, OnStreamingCompletedAsync);
        }
    }

    async Task OnStreamingMessageAsync(IAsyncApiMessage message)
    {
        if (asyncApi == null || document == null || operation.Value == null) throw new InvalidOperationException("The executor must be initialized before execution");
        if (asyncApi.Subscription == null) throw new NullReferenceException("The 'subscription' must be set when performing an AsyncAPI v3 subscribe operation");
        if (!string.IsNullOrWhiteSpace(asyncApi.Subscription.Consume.While) && !await Task.Workflow.Expressions.EvaluateConditionAsync(asyncApi.Subscription.Consume.While, Task.Input, GetExpressionEvaluationArguments(), CancellationTokenSource!.Token).ConfigureAwait(false))
        {
            keepConsume = false;
            return;
        }
        if (asyncApi.Subscription.Foreach?.Do != null)
        {
            var taskDefinition = new DoTaskDefinition()
            {
                Do = asyncApi.Subscription.Foreach.Do
            };
            var messageData = message as object;
            var currentOffset = offset ?? 0;
            if (!offset.HasValue) offset = 0;
            var arguments = GetExpressionEvaluationArguments();
            arguments ??= [];
            arguments[asyncApi.Subscription.Foreach.Item ?? RuntimeExpressions.Arguments.Each] = JsonSerializer.SerializeToNode(messageData);
            arguments[asyncApi.Subscription.Foreach.At ?? RuntimeExpressions.Arguments.Index] = JsonValue.Create(currentOffset);
            if (asyncApi.Subscription.Foreach.Output?.As != null)
            {
                var messageNode = JsonSerializer.SerializeToNode(messageData) ?? new JsonObject();
                var outputExpression = asyncApi.Subscription.Foreach.Output.As!.Match(obj => (JsonNode)obj, str => (JsonNode)JsonValue.Create(str)!);
                messageData = await Task.Workflow.Expressions.EvaluateAsync(outputExpression, messageNode, arguments, CancellationTokenSource!.Token).ConfigureAwait(false);
            }
            if (asyncApi.Subscription.Foreach.Export?.As != null)
            {
                var messageNode = JsonSerializer.SerializeToNode(messageData) ?? new JsonObject();
                var exportExpression = asyncApi.Subscription.Foreach.Export.As!.Match(obj => (JsonNode)obj, str => (JsonNode)JsonValue.Create(str)!);
                var context = await Task.Workflow.Expressions.EvaluateAsync(exportExpression, messageNode, arguments, CancellationTokenSource!.Token).ConfigureAwait(false);
                if (context is JsonObject contextObj) await Task.Instance.SetContextDataAsync(contextObj, CancellationTokenSource!.Token).ConfigureAwait(false);
            }
            var taskInstance = await Task.Workflow.Instance.CreateTaskAsync(taskDefinition, GetPathFor(currentOffset), Task.Input, null, Task.Instance, false, CancellationTokenSource!.Token).ConfigureAwait(false);
            var taskExecutor = await CreateTaskExecutorAsync(taskInstance, taskDefinition, Task.ContextData, arguments, CancellationTokenSource!.Token).ConfigureAwait(false);
            await taskExecutor.ExecuteAsync(CancellationTokenSource!.Token).ConfigureAwait(false);
            if (Task.ContextData != taskExecutor.Task.ContextData) await Task.Instance.SetContextDataAsync(taskExecutor.Task.ContextData, CancellationTokenSource!.Token).ConfigureAwait(false);
            offset++;
        }
        if (!string.IsNullOrWhiteSpace(asyncApi.Subscription.Consume.Until) && await Task.Workflow.Expressions.EvaluateConditionAsync(asyncApi.Subscription.Consume.Until, Task.Input, GetExpressionEvaluationArguments(), CancellationTokenSource!.Token).ConfigureAwait(false))
        {
            keepConsume = false;
        }
    }

    Task OnStreamingErrorAsync(Exception ex) => SetErrorAsync(new RuntimeError()
    {
        Type = Sdk.ErrorType.Communication,
        Title = ErrorTitle.Communication,
        Status = ErrorStatus.Communication,
        Detail = ex.Message,
        Instance = new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
    }, CancellationTokenSource!.Token);

    async Task OnStreamingCompletedAsync()
    {
        ITaskInstance? last = null;
        await foreach (var t in Task.Instance.GetSubTasksAsync(CancellationTokenSource!.Token).ConfigureAwait(false))
        {
            if (last == null || t.State.StartedAt > last.State.StartedAt) last = t;
        }
        JsonNode? output = null;
        if (last?.State.Output != null) output = last.State.Output;
        await SetResultAsync(output, Task.Definition.Then, CancellationTokenSource!.Token).ConfigureAwait(false);
    }

    async Task OnMessageProcessingErrorAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(executor);
        var error = executor.Task.Instance.State.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnMessageProcessingCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(executor);
        Executors.Remove(executor);
        if (Task.ContextData != executor.Task.ContextData) await Task.Instance.SetContextDataAsync(executor.Task.ContextData, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        if (disposing) subscription?.Dispose();
        return base.DisposeAsync(disposing);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing) subscription?.Dispose();
        base.Dispose(disposing);
    }

}
