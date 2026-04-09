namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="DoTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="taskProcessFactory">The service used to create <see cref="ITaskProcess"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskProcess"/></param>
public sealed class DoTaskExecutor(IServiceProvider serviceProvider, ILogger<DoTaskExecutor> logger, ITaskProcessFactory taskProcessFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskProcess<DoTaskDefinition> task)
    : TaskExecutor<DoTaskDefinition>(serviceProvider, logger, taskProcessFactory, executorFactory, schemaHandlerProvider, task)
{

    Map<string, TaskDefinition> Tasks => Task.Instance.Definition.Do;

    static string GetPathFor(int index, string name) => $"do/{index}/{name}";

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
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance instance, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(instance, definition, contextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnSubTaskFaultAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false),
            async () => await OnSubtaskCompletedAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false)
        );
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        ITaskInstance? last = null;
        await foreach (var subtask in Task.Instance.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) last = subtask;
        MapEntry<string, TaskDefinition>? nextEntry;
        if (last == null) nextEntry = Tasks.FirstOrDefault();
        else if (last.State.Status == null || last.State.IsOperative || last.State.Status == TaskInstanceStatus.Suspended) nextEntry = Tasks.FirstOrDefault(e => e.Key == last.State.Name) ?? throw new NullReferenceException($"Failed to find a task with the specified name '{last.State.Name}' at '{Task.Instance.State.Reference}'");
        else  nextEntry = GetNextTask(last.State.Name);
        if (last != null && (last.State.Status == null || last.State.IsOperative || last.State.Status == TaskInstanceStatus.Suspended))
        {
            ITaskInstance? lastCompleted = null;
            await foreach (var subtask in Task.Instance.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) if (subtask.State.Status != null && !subtask.State.IsOperative && subtask.State.Status != TaskInstanceStatus.Suspended) lastCompleted = subtask;
            if (lastCompleted != null) last = lastCompleted;
        }
        if (nextEntry == null)
        {
            await SetResultAsync(last?.State.Output, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
            return;
        }
        var nextIndex = Tasks.Keys.ToList().IndexOf(nextEntry.Key);
        var input = last == null ? Task.Instance.State.Input : last.State.Output ?? new JsonObject();
        var next = await Task.Workflow.Instance.CreateTaskAsync(nextEntry.Value, GetPathFor(nextIndex, nextEntry.Key), input, null, Task.Instance, Task.Instance.State.IsExtension, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTaskExecutorAsync(next, nextEntry.Value, Task.Instance.State.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task OnSubTaskFaultAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var error = executor.Task.Instance.State.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnSubtaskCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var lastState = executor.Task.Instance.State;
        var output = executor.Task.Output ?? new JsonObject();
        Executors.Remove(executor);
        if (Task.Instance.State.ContextData != executor.Task.Instance.State.ContextData) await Task.SetContextDataAsync(executor.Task.Instance.State.ContextData, cancellationToken).ConfigureAwait(false);
        var nextEntry = GetNextTask(lastState.Name);
        if (nextEntry == null)
        {
            var then = lastState.Status != TaskInstanceStatus.Skipped && lastState.Next == FlowDirective.End ? FlowDirective.End : Task.Instance.Definition.Then;
            await SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
            return;
        }
        var nextIndex = Tasks.Keys.ToList().IndexOf(nextEntry.Key);
        var flowDirective = lastState.Status == TaskInstanceStatus.Skipped ? FlowDirective.Continue : lastState.Next;
        switch (flowDirective)
        {
            case FlowDirective.End:
                await SetResultAsync(output, FlowDirective.End, cancellationToken).ConfigureAwait(false);
                break;
            case FlowDirective.Exit:
                await SetResultAsync(output, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
                break;
            default:
                var next = await Task.Workflow.Instance.CreateTaskAsync(nextEntry.Value, GetPathFor(nextIndex, nextEntry.Key), output, null, Task.Instance, false, cancellationToken).ConfigureAwait(false);
                var nextExecutor = await CreateTaskExecutorAsync(next, nextEntry.Value, Task.Instance.State.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
                await nextExecutor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                break;
        }
    }

}
