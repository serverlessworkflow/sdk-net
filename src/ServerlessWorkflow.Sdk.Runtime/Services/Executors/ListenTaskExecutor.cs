namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="ListenTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="cloudEventBus">The service used to publish and subscribe to cloud events</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class ListenTaskExecutor(IServiceProvider serviceProvider, ILogger<ListenTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ICloudEventBus cloudEventBus, ITaskExecutionContext<ListenTaskDefinition> task)
    : TaskExecutor<ListenTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    IDisposable? subscription;
    uint eventOffset;

    static JsonPointer GetPathFor(uint offset) => JsonPointer.Create("foreach", $"{offset - 1}", "do");

    /// <inheritdoc/>
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskState state, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(state, definition, contextData, arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnEventProcessingErrorAsync(executor, CancellationTokenSource!.Token).ConfigureAwait(false),
            async () => await OnEventProcessingCompletedAsync(executor, CancellationTokenSource!.Token).ConfigureAwait(false)
        );
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (Task.Definition.Foreach == null)
        {
            var events = await cloudEventBus.SubscribeAsync(cancellationToken).ConfigureAwait(false);
            var collected = new List<JsonNode?>();
            var tcs = new TaskCompletionSource();
            events.Subscribe(
                onNext: e =>
                {
                    var eventData = Task.Definition.Listen.Read switch
                    {
                        EventReadMode.Envelope => JsonSerializer.SerializeToNode(e),
                        _ => JsonSerializer.SerializeToNode(e)
                    };
                    collected.Add(eventData);
                    tcs.TrySetResult();
                },
                onError: ex => tcs.TrySetException(ex),
                onCompleted: () => tcs.TrySetResult()
            );
            await tcs.Task.ConfigureAwait(false);
            var result = new JsonObject { ["events"] = new JsonArray([.. collected]) };
            await SetResultAsync(result, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            ITaskState? lastSubtask = null;
            await foreach (var subtask in Task.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) lastSubtask = subtask;
            if (lastSubtask != null && lastSubtask.IsOperative && Task.Definition.Foreach.Do != null)
            {
                var taskDefinition = new DoTaskDefinition() 
                { 
                    Do = Task.Definition.Foreach.Do 
                };
                var arguments = GetExpressionEvaluationArguments();
                var taskExecutor = await CreateTaskExecutorAsync(lastSubtask, taskDefinition, Task.Workflow.State.ContextData, arguments, CancellationTokenSource!.Token).ConfigureAwait(false);
                await taskExecutor.ExecuteAsync(CancellationTokenSource!.Token).ConfigureAwait(false);
            }
            var events = await cloudEventBus.SubscribeAsync(cancellationToken).ConfigureAwait(false);
            subscription = events.SubscribeAsync(OnStreamingEventAsync, OnStreamingErrorAsync, OnStreamingCompletedAsync);
        }
    }

    async Task OnStreamingEventAsync(ICloudEvent e)
    {
        eventOffset++;
        if (Task.Definition.Foreach?.Do == null)
        {
            return;
        }
        var taskDefinition = new DoTaskDefinition() 
        {
            Do = Task.Definition.Foreach.Do 
        };
        var arguments = GetExpressionEvaluationArguments() ?? [];
        JsonNode? eventData = Task.Definition.Listen.Read switch
        {
            EventReadMode.Envelope => JsonSerializer.SerializeToNode(e),
            _ => JsonSerializer.SerializeToNode(e)
        };
        if (Task.Definition.Foreach.Output?.As != null)
        {
            eventData = await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.Foreach.Output.As, eventData ?? new JsonObject(), arguments, CancellationTokenSource!.Token).ConfigureAwait(false);
        }
        if (Task.Definition.Foreach.Export?.As != null)
        {
            var context = (await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.Foreach.Export.As, eventData ?? new JsonObject(), arguments, CancellationTokenSource!.Token).ConfigureAwait(false))?.AsObject();
            if (context != null) await Task.SetContextDataAsync(context, CancellationTokenSource!.Token).ConfigureAwait(false);
        }
        arguments[Task.Definition.Foreach.Item ?? RuntimeExpressions.Arguments.Each] = eventData!;
        arguments[Task.Definition.Foreach.At ?? RuntimeExpressions.Arguments.Index] = eventOffset - 1;
        var taskInstance = await Task.Workflow.CreateTaskAsync(taskDefinition, GetPathFor(eventOffset), Task.State.Input, Task, false, CancellationTokenSource!.Token).ConfigureAwait(false);
        var taskExecutor = await CreateTaskExecutorAsync(taskInstance, taskDefinition, Task.Workflow.State.ContextData, arguments, CancellationTokenSource!.Token).ConfigureAwait(false);
        await taskExecutor.ExecuteAsync(CancellationTokenSource!.Token).ConfigureAwait(false);
    }

    Task OnStreamingErrorAsync(Exception ex) => SetErrorAsync(new Error()
    {
        Type = ErrorType.Communication,
        Title = ErrorTitle.Communication,
        Status = ErrorStatus.Communication,
        Detail = ex.Message,
        Instance = new Uri(Task.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
    }, CancellationTokenSource!.Token);

    async Task OnStreamingCompletedAsync()
    {
        ITaskState? last = null;
        await foreach (var subtask in Task.GetSubTasksAsync(CancellationTokenSource!.Token).ConfigureAwait(false)) last = subtask;
        var output = last?.Output;
        await SetResultAsync(output, Task.Definition.Then, CancellationTokenSource!.Token).ConfigureAwait(false);
    }

    async Task OnEventProcessingErrorAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var error = executor.Task.State.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnEventProcessingCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        Executors.Remove(executor);
        if (Task.Workflow.State.ContextData != executor.Task.Workflow.State.ContextData) await Task.SetContextDataAsync(executor.Task.Workflow.State.ContextData, cancellationToken).ConfigureAwait(false);
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
