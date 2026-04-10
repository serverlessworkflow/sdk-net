namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowProcess"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="workflow">The <see cref="IWorkflowExecutionContext"/> in which to execute the workflow process</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
public sealed class WorkflowProcess(ILogger<WorkflowProcess> logger, IWorkflowExecutionContext workflow, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory)
    : IWorkflowProcess
{

    readonly CancellationTokenSource cancellationTokenSource = new();
    readonly TaskCompletionSource taskCompletionSource = new();
    readonly ConcurrentDictionary<ITaskExecutor, bool> executors = [];
    readonly Subject<IWorkflowLifeCycleEvent> lifeCycleEvents = new();
    readonly Stopwatch stopwatch = new();

    /// <summary>
    /// Runs the <see cref="WorkflowProcess"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public async Task RunAsync()
    {
        try
        {
            switch (workflow.State.Status)
            {
                case null or WorkflowStatus.Pending:
                    await StartAsync(cancellationTokenSource.Token).ConfigureAwait(false);
                    break;
                case WorkflowStatus.Running:
                case WorkflowStatus.Suspended:
                    await ResumeAsync(cancellationTokenSource.Token).ConfigureAwait(false);
                    break;
                case WorkflowStatus.Completed:
                    taskCompletionSource.SetResult();
                    return;
                default:
                    if (logger.IsEnabled(LogLevel.Warning)) logger.LogWarning("The workflow instance '{instance}' is in an unexpected status phase '{status}'", workflow.State.GetQualifiedName(), workflow.State.Status);
                    return;
            }
        }
        catch (Exception ex)
        {
            if (logger.IsEnabled(LogLevel.Error)) logger.LogError("A critical exception occurred while executing the workflow instance '{instance}': {ex}", workflow.State.GetQualifiedName(), ex);
            await workflow.SetErrorAsync(Error.Runtime(new Uri("/", UriKind.Relative), $"A critical exception occurred while executing the workflow instance '{workflow.State.GetQualifiedName()}': {ex}"), cancellationTokenSource.Token).ConfigureAwait(false);
        }
    }

    /// <inheritdoc/>
    public IDisposable Subscribe(IObserver<IWorkflowLifeCycleEvent> observer) => lifeCycleEvents.Subscribe(observer);

    async Task StartAsync(CancellationToken cancellationToken)
    {
        await workflow.StartAsync(cancellationToken).ConfigureAwait(false);
        var taskDefinition = workflow.Definition.Do.First();
        var task = await workflow.CreateTaskAsync(taskDefinition.Value, JsonPointer.Create(taskDefinition.Key), workflow.State.Input ?? [], null, false, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTaskExecutorAsync(task, taskDefinition.Value, workflow.State.ContextData ?? [], null, cancellationToken).ConfigureAwait(false);
        stopwatch.Start();
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public Task WaitAsync(CancellationToken cancellationToken = default) => taskCompletionSource.Task.WaitAsync(CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource.Token, cancellationToken).Token);

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        foreach (var executor in executors.Keys.ToList())
        {
            await executor.SuspendAsync(cancellationToken).ConfigureAwait(false);
            executors.TryRemove(executor, out _);
        }
        stopwatch.Stop();
        await workflow.SuspendAsync(cancellationToken).ConfigureAwait(false);
        lifeCycleEvents.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Suspended));
        taskCompletionSource.TrySetResult();
        cancellationTokenSource?.Cancel();
    }

    /// <inheritdoc/>
    public async Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        await workflow.ResumeAsync(cancellationToken).ConfigureAwait(false);
        var task = await workflow.GetTasksAsync(cancellationToken).FirstOrDefaultAsync(t => string.IsNullOrWhiteSpace(t.ParentId) && (t.Status == null || t.IsOperative || t.Status == TaskStatus.Suspended), cancellationToken);
        if (task == null) return;
        var taskDefinition = workflow.Definition.GetComponent<TaskDefinition>(task.Reference.ToString());
        var executor = await CreateTaskExecutorAsync(task, taskDefinition, [], [], cancellationToken).ConfigureAwait(false);
        stopwatch.Start();
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        stopwatch.Stop();
        await workflow.SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
        lifeCycleEvents.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Faulted));
        lifeCycleEvents.OnError(new RuntimeErrorException(error));
        taskCompletionSource.TrySetException(new RuntimeErrorException(error));
    }

    async Task SetResultAsync(JsonNode? result, CancellationToken cancellationToken = default)
    {
        if (workflow.State.Status != WorkflowStatus.Running) return;
        stopwatch.Stop();
        var output = result;
        await workflow.SetResultAsync(output, cancellationToken).ConfigureAwait(false);
        lifeCycleEvents.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Completed));
        lifeCycleEvents.OnCompleted();
        taskCompletionSource.TrySetResult();
    }

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        foreach (var executor in executors.Keys.ToList())
        {
            await executor.CancelAsync(cancellationToken).ConfigureAwait(false);
            executors.TryRemove(executor, out _);
        }
        stopwatch.Stop();
        await workflow.CancelAsync(cancellationToken).ConfigureAwait(false);
        lifeCycleEvents.OnNext(new WorkflowLifeCycleEvent(WorkflowLifeCycleEventType.Cancelled));
        taskCompletionSource.TrySetCanceled(cancellationToken);
        cancellationTokenSource?.Cancel();
    }

    async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskState task, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(task);
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(contextData);
        var context = executionContextFactory.Create(workflow, definition, task, arguments);
        var executor = executorFactory.Create(context);
        executor.SubscribeAsync
        (
            _ => Task.CompletedTask,
            async ex => await OnTaskFaultedAsync(executor, cancellationToken).ConfigureAwait(false),
            async () => await OnTaskCompletedAsync(executor, cancellationToken).ConfigureAwait(false)
        );
        await executor.InitializeAsync(cancellationToken).ConfigureAwait(false);
        executors.TryAdd(executor, false);
        return executor;
    }

    async Task OnTaskFaultedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        await SetErrorAsync(executor.Task.State.Error ?? throw new Exception("Faulted tasks must document an error"), cancellationToken).ConfigureAwait(false);
        executors.TryRemove(executor, out _);
    }

    async Task OnTaskCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var nextDefinition = (executor.Task.State.Status == TaskStatus.Skipped ? FlowDirective.Continue : executor.Task.State.Next) switch
        {
            FlowDirective.End or FlowDirective.Exit => null,
            _ => workflow.Definition.GetTaskAfter(executor.Task.State)
        };
        var completedTask = executor.Task;
        executors.TryRemove(executor, out _);
        if (nextDefinition == null)
        {
            await SetResultAsync(completedTask.State.Output, cancellationToken).ConfigureAwait(false);
            return;
        }
        var nextTask = await workflow.CreateTaskAsync(nextDefinition.Value, JsonPointer.Create(nextDefinition.Key), completedTask.State.Output ?? new JsonObject(), cancellationToken: cancellationToken).ConfigureAwait(false);
        var nextExecutor = await CreateTaskExecutorAsync(nextTask, nextDefinition.Value, executor.Task.Workflow.State.ContextData, [], cancellationToken).ConfigureAwait(false);
        await nextExecutor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        foreach (var executor in executors.Keys.ToList()) if (executors.TryRemove(executor, out var _)) await executor.DisposeAsync().ConfigureAwait(false);
        stopwatch.Stop();
        cancellationTokenSource.Dispose();
    }

}
