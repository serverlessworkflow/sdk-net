namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="TryTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="taskProcessFactory">The service used to create <see cref="ITaskProcess"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskProcess"/></param>
public sealed class TryTaskExecutor(IServiceProvider serviceProvider, ILogger<TryTaskExecutor> logger, ITaskProcessFactory taskProcessFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskProcess<TryTaskDefinition> task)
    : TaskExecutor<TryTaskDefinition>(serviceProvider, logger, taskProcessFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var taskDefinition = new DoTaskDefinition() { Do = Task.Instance.Definition.Try };
        var tryInstance = await Task.Workflow.Instance.CreateTaskAsync(taskDefinition, "try", Task.Instance.State.Input, null, Task.Instance, false, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTryExecutorAsync(tryInstance, taskDefinition, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override async Task RetryCoreAsync(Error cause, CancellationToken cancellationToken)
    {
        var taskDefinition = new DoTaskDefinition() { Do = Task.Instance.Definition.Try };
        var retryInstance = await Task.Workflow.Instance.CreateTaskAsync(taskDefinition, "retry/try", Task.Instance.State.Input, null, Task.Instance, false, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTryExecutorAsync(retryInstance, taskDefinition, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task<ITaskExecutor> CreateTryExecutorAsync(ITaskInstance instance, TaskDefinition definition, CancellationToken cancellationToken)
    {
        var executor = await base.CreateTaskExecutorAsync(instance, definition, Task.Instance.State.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnTryFaultedAsync(executor, ex, CancellationTokenSource?.Token ?? default).ConfigureAwait(false),
            async () => await OnTryCompletedAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false)
        );
        return executor;
    }

    async Task OnTryFaultedAsync(ITaskExecutor executor, Exception ex, CancellationToken cancellationToken)
    {
        var error = ex is RuntimeErrorException errorEx ? errorEx.Error : new Error()
        {
            Status = ErrorStatus.Runtime,
            Type = ErrorType.Runtime,
            Title = ErrorTitle.Runtime,
            Detail = ex.Message
        };
        Executors.Remove(executor);
        var hasRetryPolicy = Task.Instance.Definition.Catch.Retry != null;
        if (hasRetryPolicy)
        {
            var retryPolicy = Task.Instance.Definition.Catch.Retry!.Match(
                policy => policy,
                reference =>
                {
                    if (Task.Workflow.Definition.Use?.Retries?.TryGetValue(reference, out var referencedRetry) == true) return referencedRetry;
                    return null;
                }
            );
            if (retryPolicy != null)
            {
                var limit = retryPolicy.Limit;
                var limitReached = false;
                if (limit?.Attempt?.Count != null)
                {
                    var retryCount = 0;
                    await foreach (var subtask in Task.Instance.GetSubTasksAsync(cancellationToken).ConfigureAwait(false))
                    {
                        if (subtask.State.Name?.StartsWith("retry") == true) retryCount++;
                    }
                    if (retryCount >= limit.Attempt.Count) limitReached = true;
                }
                if (limitReached) await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
                else await RetryAsync(error, cancellationToken).ConfigureAwait(false);
                return;
            }
        }
        if (Task.Instance.Definition.Catch.Do != null)
        {
            var handlerDefinition = new DoTaskDefinition() { Do = Task.Instance.Definition.Catch.Do };
            var handlerInstance = await Task.Workflow.Instance.CreateTaskAsync(handlerDefinition, "catch/do", Task.Instance.State.Input, null, Task.Instance, false, cancellationToken).ConfigureAwait(false);
            var arguments = Task.Arguments.DeepClone().AsObject()!;
            arguments[Task.Instance.Definition.Catch.As ?? RuntimeExpressions.Arguments.Error] = JsonSerializer.SerializeToNode(error)!;
            var handlerExecutor = await base.CreateTaskExecutorAsync(handlerInstance, handlerDefinition, Task.Instance.State.ContextData, arguments, cancellationToken).ConfigureAwait(false);
            handlerExecutor.SubscribeAsync(
                _ => System.Threading.Tasks.Task.CompletedTask,
                async handlerEx => await OnHandlerFaultAsync(handlerExecutor, cancellationToken).ConfigureAwait(false),
                async () => await OnHandlerCompletedAsync(handlerExecutor, cancellationToken).ConfigureAwait(false)
            );
            await handlerExecutor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            return;
        }
        await SetResultAsync(null, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    async Task OnTryCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        if (Task.Instance.State.ContextData != executor.Task.Instance.State.ContextData)
            await Task.SetContextDataAsync(executor.Task.Instance.State.ContextData, cancellationToken).ConfigureAwait(false);
        var output = executor.Task.Output ?? new JsonObject();
        Executors.Remove(executor);
        var then = executor.Task.Instance.State.Next == FlowDirective.End ? FlowDirective.End : Task.Instance.Definition.Then;
        await SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
    }

    async Task OnHandlerFaultAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var error = executor.Task.Instance.State.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnHandlerCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        if (Task.Instance.State.ContextData != executor.Task.Instance.State.ContextData)
            await Task.SetContextDataAsync(executor.Task.Instance.State.ContextData, cancellationToken).ConfigureAwait(false);
        var output = executor.Task.Output ?? new JsonObject();
        Executors.Remove(executor);
        var then = executor.Task.Instance.State.Next == FlowDirective.End ? FlowDirective.End : Task.Instance.Definition.Then;
        await SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
    }

}
