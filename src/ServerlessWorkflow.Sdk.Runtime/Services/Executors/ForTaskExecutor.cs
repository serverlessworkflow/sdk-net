namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="ForTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class ForTaskExecutor(IServiceProvider serviceProvider, ILogger<ForTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<ForTaskDefinition> task)
    : TaskExecutor<ForTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    JsonArray? collection;

    string GetPathFor(string subTaskName) => $"for/{subTaskName}/do";

    /// <inheritdoc/>
    protected override async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance instance, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        var executor = await base.CreateTaskExecutorAsync(instance, definition, contextData, arguments, cancellationToken).ConfigureAwait(false);
        executor.SubscribeAsync(
            _ => System.Threading.Tasks.Task.CompletedTask,
            async ex => await OnIterationFaultAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false),
            async () => await OnIterationCompletedAsync(executor, CancellationTokenSource?.Token ?? default).ConfigureAwait(false)
        );
        return executor;
    }

    /// <inheritdoc/>
    protected override async Task InitializeCoreAsync(CancellationToken cancellationToken)
    {
        var result = await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.For.In, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        collection = result?.AsArray() ?? throw new InvalidOperationException("The 'for.in' expression must evaluate to an array");
    }

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (collection == null) throw new InvalidOperationException("The executor must be initialized before execution");
        ITaskInstance? lastSubtask = null;
        await foreach (var subtask in Task.Instance.GetSubTasksAsync(cancellationToken).ConfigureAwait(false)) lastSubtask = subtask;
        var index = 0;
        if (lastSubtask != null)
        {
            var parts = lastSubtask.State.Reference.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && int.TryParse(parts[^1], out var lastIndex)) index = lastIndex;
            if (index == collection.Count - 1)
            {
                await SetResultAsync(Task.Input, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
                return;
            }
            if (!lastSubtask.State.IsOperative) index++;
        }
        var item = collection[index];
        var taskDefinition = new DoTaskDefinition() { Do = Task.Definition.Do };
        var taskInstance = await Task.Workflow.Instance.CreateTaskAsync(taskDefinition, GetPathFor(index.ToString()), Task.Input, null, Task.Instance, false, cancellationToken).ConfigureAwait(false);
        var contextData = Task.ContextData.DeepClone().AsObject()!;
        var arguments = Task.Arguments.DeepClone().AsObject()!;
        arguments[Task.Definition.For.Each] = item?.DeepClone();
        arguments[Task.Definition.For.At ?? RuntimeExpressions.Arguments.Index] = index;
        var executor = await CreateTaskExecutorAsync(taskInstance, taskDefinition, contextData, arguments, cancellationToken).ConfigureAwait(false);
        await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task OnIterationFaultAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        if (collection == null) throw new InvalidOperationException("The executor must be initialized before execution");
        var error = executor.Task.Instance.State.Error ?? throw new NullReferenceException();
        Executors.Remove(executor);
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

    async Task OnIterationCompletedAsync(ITaskExecutor executor, CancellationToken cancellationToken)
    {
        if (collection == null) throw new InvalidOperationException("The executor must be initialized before execution");
        var output = executor.Task.Output ?? new JsonObject();
        Executors.Remove(executor);
        if (Task.ContextData != executor.Task.ContextData) await Task.Instance.SetContextDataAsync(executor.Task.ContextData, cancellationToken).ConfigureAwait(false);
        var lastReference = executor.Task.Instance.State.Reference.ToString();
        var parts = lastReference.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var index = 0;
        if (parts.Length >= 2 && int.TryParse(parts[^2], out var parsedIndex)) index = parsedIndex + 1;
        if (index >= collection.Count)
        {
            await SetResultAsync(output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            return;
        }
        switch (executor.Task.Instance.State.Next)
        {
            case FlowDirective.Continue:
                var taskDefinition = new DoTaskDefinition() { Do = Task.Definition.Do };
                var next = await Task.Workflow.Instance.CreateTaskAsync(taskDefinition, GetPathFor(index.ToString()), output, null, Task.Instance, false, cancellationToken).ConfigureAwait(false);
                var item = collection[index];
                var contextData = Task.ContextData.DeepClone().AsObject()!;
                var arguments = Task.Arguments.DeepClone().AsObject()!;
                arguments[Task.Definition.For.Each] = item?.DeepClone();
                arguments[Task.Definition.For.At ?? RuntimeExpressions.Arguments.Index] = index;
                var nextExecutor = await CreateTaskExecutorAsync(next, taskDefinition, contextData, arguments, cancellationToken).ConfigureAwait(false);
                await nextExecutor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
                break;
            case FlowDirective.End:
                await SetResultAsync(output, FlowDirective.End, cancellationToken).ConfigureAwait(false);
                break;
            case FlowDirective.Exit:
                await SetResultAsync(output, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
                break;
            default:
                await SetErrorAsync(RuntimeError.Configuration(new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute), "Unable to continue with a specific task within a loop"), cancellationToken).ConfigureAwait(false);
                break;
        }
    }

}
