namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute workflow <see cref="RunTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class WorkflowRunTaskExecutor(IServiceProvider serviceProvider, ILogger<WorkflowRunTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<RunTaskDefinition> task)
    : TaskExecutor<RunTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        throw new NotSupportedException("The workflow process type is not yet supported by the SDK runtime. Use a full workflow runtime implementation (e.g. Synapse) to execute sub-workflows.");
    }

}
