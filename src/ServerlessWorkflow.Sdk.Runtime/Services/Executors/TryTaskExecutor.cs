namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="TryTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class TryTaskExecutor(IServiceProvider serviceProvider, ILogger<TryTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<TryTaskDefinition> task)
    : TaskExecutor<TryTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var taskDefinition = new DoTaskDefinition() { Do = Task.Definition.Try };
        var tryInstance = await Task.Workflow.CreateTaskAsync(taskDefinition, JsonPointer.Create("try"), Task.State.Input, Task, false, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTryExecutorAsync(tryInstance, taskDefinition, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override async Task RetryCoreAsync(Error cause, CancellationToken cancellationToken)
    {
        var taskDefinition = new DoTaskDefinition() { Do = Task.Definition.Try };
        var retryInstance = await Task.Workflow.CreateTaskAsync(taskDefinition, JsonPointer.Create("retry", "try"), Task.State.Input, Task, false, cancellationToken).ConfigureAwait(false);
        var executor = await CreateTryExecutorAsync(retryInstance, taskDefinition, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task<ITaskExecutor> CreateTryExecutorAsync(ITaskState state, TaskDefinition definition, CancellationToken cancellationToken)
    {
        var executor = await base.CreateTaskExecutorAsync(state, definition, Task.Workflow.State.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
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
        var hasRetryPolicy = Task.Definition.Catch.Retry != null;
        if (hasRetryPolicy)
        {
            var retryPolicy = Task.Definition.Catch.Retry!.Match(
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
                    await foreach (var subtask in Task.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) if (subtask.Name?.StartsWith("retry") == true) retryCount++;
                    if (retryCount >= limit.Attempt.Count) limitReached = true;
                }
                if (limitReached) await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
                else await RetryAsync(error, cancellationToken).ConfigureAwait(false);
                return;
            }
        }
        if (Task.Definition.Catch.Do != null)
        {
            var handlerDefinition = new DoTaskDefinition() { Do = Task.Definition.Catch.Do };
            var handlerInstance = await Task.Workflow.CreateTaskAsync(handlerDefinition, JsonPointer.Create("catch", "do"), Task.State.Input, Task, false, cancellationToken).ConfigureAwait(false);
            var arguments = Task.Arguments?.DeepClone().AsObject()! ?? [];
            arguments[Task.Definition.Catch.As ?? RuntimeExpressions.Arguments.Error] = JsonSerializer.SerializeToNode(error)!;
            var handlerExecutor = await base.CreateTaskExecutorAsync(handlerInstance, handlerDefinition, Task.Workflow.State.ContextData, arguments, cancellationToken).ConfigureAwait(false);
            handlerExecutor.SubscribeAsync(
                _ => System.Threading.Tasks.Task.CompletedTask,
                async handlerEx => await OnHandlerFaultAsync(handlerExecutor, cancellationToken).ConfigureAwait(false),
                async () => await OnHandlerCompletedAsync(handlerExecutor, cancellationToken).ConfigureAwait(false)
            );
            await handlerExecutor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            return;
        }
        await SetResultAsync(null, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

    async Task OnTryCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        if (Task.Workflow.State.ContextData != executor.Task.Workflow.State.ContextData) await Task.SetContextDataAsync(executor.Task.Workflow.State.ContextData, cancellationToken).ConfigureAwait(false);
        var output = executor.Task.State.Output ?? new JsonObject();
        Executors.Remove(executor);
        var then = executor.Task.State.Next == FlowDirective.End ? FlowDirective.End : Task.Definition.Then;
        await SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
    }

    async Task OnHandlerFaultAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        var error = executor.Task.State.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnHandlerCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        if (Task.Workflow.State.ContextData != executor.Task.Workflow.State.ContextData) await Task.SetContextDataAsync(executor.Task.Workflow.State.ContextData, cancellationToken).ConfigureAwait(false);
        var output = executor.Task.State.Output ?? new JsonObject();
        Executors.Remove(executor);
        var then = executor.Task.State.Next == FlowDirective.End ? FlowDirective.End : Task.Definition.Then;
        await SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
    }

}
