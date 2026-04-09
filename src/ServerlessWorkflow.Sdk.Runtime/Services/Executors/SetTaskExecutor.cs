namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="SetTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="taskProcessFactory">The service used to create <see cref="ITaskProcess"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskProcess"/></param>
public sealed class SetTaskExecutor(IServiceProvider serviceProvider, ILogger<SetTaskExecutor> logger, ITaskProcessFactory taskProcessFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskProcess<SetTaskDefinition> task)
    : TaskExecutor<SetTaskDefinition>(serviceProvider, logger, taskProcessFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var result = await Task.Workflow.Expressions.EvaluateAsync(Task.Instance.Definition.Set, Task.Instance.State.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        await SetResultAsync(result, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}
