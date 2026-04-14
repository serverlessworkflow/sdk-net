namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="ForkTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class ForkTaskExecutor(IServiceProvider serviceProvider, ILogger<ForkTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<ForkTaskDefinition> task)
    : TaskExecutor<ForkTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    static JsonPointer GetPathFor(int index, string name) => JsonPointer.Create("fork", "branches", index, name);

    /// <inheritdoc/>
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance state, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(state, definition, contextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnSubTaskFaultAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false),
            async () => await OnSubTaskCompletedAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false)
        );
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var branches = Task.Definition.Fork.Branches;
        var executionTasks = new List<Task>();
        var index = 0;
        foreach (var branch in branches)
        {
            var branchInstance = await Task.Workflow.CreateTaskAsync(branch.Value, GetPathFor(index, branch.Key), Task.Instance.Input, Task, false, cancellationToken).ConfigureAwait(false);
            var executor = await CreateTaskExecutorAsync(branchInstance, branch.Value, Task.Workflow.Instance.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
            executionTasks.Add(executor.ExecuteAsync(cancellationToken));
            index++;
        }
        await System.Threading.Tasks.Task.WhenAll(executionTasks).ConfigureAwait(false);
    }

    async Task OnSubTaskFaultAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        using var @lock = await Lock.LockAsync(cancellationToken).ConfigureAwait(false);
        var error = executor.Task.Instance.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        foreach (var subExecutor in Executors.ToList())
        {
            await subExecutor.CancelAsync(cancellationToken).ConfigureAwait(false);
            Executors.Remove(subExecutor);
        }
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnSubTaskCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        using var @lock = await Lock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (Task.Instance.Status != TaskStatus.Running)
        {
            if (Executors.Remove(executor)) await executor.CancelAsync(cancellationToken).ConfigureAwait(false);
            return;
        }
        if (Task.Definition.Fork.Compete)
        {
            var output = executor.Task.Instance.Output ?? new JsonObject();
            foreach (var concurrentExecutor in Executors.ToList())
            {
                Executors.Remove(concurrentExecutor);
                await concurrentExecutor.CancelAsync(cancellationToken).ConfigureAwait(false);
            }
            await SetResultAsync(output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            Executors.Remove(executor);
            await executor.DisposeAsync().ConfigureAwait(false);
            var allDone = true;
            await foreach (var subtask in Task.GetSubTasksAsync(cancellationToken).ConfigureAwait(false))
            {
                if (subtask.Status != TaskStatus.Skipped && subtask.Status != TaskStatus.Completed && subtask.Status != TaskStatus.Cancelled && subtask.Status != TaskStatus.Faulted)
                {
                    allDone = false;
                    break;
                }
            }
            if (allDone) await SetResultAsync(new JsonObject(), Task.Definition.Then, cancellationToken).ConfigureAwait(false);
        }
    }

}
