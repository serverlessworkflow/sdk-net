namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="DoTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class DoTaskExecutor(IServiceProvider serviceProvider, ILogger<DoTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<DoTaskDefinition> task)
    : TaskExecutor<DoTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    Map<string, TaskDefinition> Tasks => Task.Definition.Do;

    JsonPointer GetPathFor(int index, string name) => JsonPointer.Parse($"{Task.Instance.Reference}/{index}/{name}");

    MapEntry<string, TaskDefinition>? GetNextTask(string? currentName)
    {
        if (currentName == null) return null;
        var keys = Tasks.Keys;
        var index = keys.ToList().IndexOf(currentName);
        if (index < 0 || index >= keys.Count - 1) return null;
        var nextKey = keys[index + 1];
        return new(nextKey, Tasks[nextKey]);
    }

    /// <inheritdoc/>
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance state, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(state, definition, contextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnSubTaskFaultAsync(executor, ex, CancellationTokenSource?.Token ?? default).ConfigureAwait(false),
            async () => await OnSubtaskCompletedAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false)
        );
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        ITaskInstance? last = null;
        await foreach (var subtask in Task.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) last = subtask;
        MapEntry<string, TaskDefinition>? nextEntry;
        if (last == null) nextEntry = Tasks.FirstOrDefault();
        else if (last.Status == null || last.IsOperative || last.Status == TaskStatus.Suspended) nextEntry = Tasks.FirstOrDefault(e => e.Key == last.Name) ?? throw new NullReferenceException($"Failed to find a task with the specified name '{last.Name}' at '{Task.Instance.Reference}'");
        else  nextEntry = GetNextTask(last.Name);
        if (last != null && (last.Status == null || last.IsOperative || last.Status == TaskStatus.Suspended))
        {
            ITaskInstance? lastCompleted = null;
            await foreach (var subtask in Task.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) if (subtask.Status != null && !subtask.IsOperative && subtask.Status != TaskStatus.Suspended) lastCompleted = subtask;
            if (lastCompleted != null) last = lastCompleted;
        }
        if (nextEntry == null)
        {
            await SetResultAsync(last?.Output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            return;
        }
        var nextIndex = Tasks.Keys.ToList().IndexOf(nextEntry.Key);
        var input = last == null ? Task.Instance.Input : last.Output ?? new JsonObject();
        var next = await Task.Workflow.CreateTaskAsync(nextEntry.Value, GetPathFor(nextIndex, nextEntry.Key), input, Task, Task.Instance.IsExtension, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTaskExecutorAsync(next, nextEntry.Value, Task.Workflow.Instance.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task OnSubTaskFaultAsync(ITaskExecutor executor, Exception ex, CancellationToken cancellationToken)
    {
        var error = executor.Task.Instance.Error;
        if (error is null)
        {
            if (ex is RuntimeErrorException rex) error = rex.Error;
            else error = Error.Runtime(new(executor.Task.Instance.Reference.ToString(), UriKind.Relative), $"An unhandled exception was thrown during the execution of task '{executor.Task.Instance.Reference}': {ex}");
        }
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnSubtaskCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var lastState = executor.Task.Instance;
        var output = executor.Task.Instance.Output ?? new JsonObject();
        Executors.Remove(executor);
        if (Task.Workflow.Instance.ContextData != executor.Task.Workflow.Instance.ContextData) await Task.SetContextDataAsync(executor.Task.Workflow.Instance.ContextData, cancellationToken).ConfigureAwait(false);
        var nextEntry = GetNextTask(lastState.Name);
        if (nextEntry == null)
        {
            var then = lastState.Status != TaskStatus.Skipped && lastState.Next == FlowDirective.End ? FlowDirective.End : Task.Definition.Then;
            await SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
            return;
        }
        var nextIndex = Tasks.Keys.ToList().IndexOf(nextEntry.Key);
        var flowDirective = lastState.Status == TaskStatus.Skipped ? FlowDirective.Continue : lastState.Next;
        switch (flowDirective)
        {
            case FlowDirective.End:
                await SetResultAsync(output, FlowDirective.End, cancellationToken).ConfigureAwait(false);
                break;
            case FlowDirective.Exit:
                await SetResultAsync(output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
                break;
            default:
                var next = await Task.Workflow.CreateTaskAsync(nextEntry.Value, GetPathFor(nextIndex, nextEntry.Key), output, Task, false, cancellationToken).ConfigureAwait(false);
                var nextExecutor = await CreateTaskExecutorAsync(next, nextEntry.Value, Task.Workflow.Instance.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
                await nextExecutor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                break;
        }
    }

}
