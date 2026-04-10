namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowProcessFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="executionContextFactory">The service used to create <see cref="IWorkflowExecutionContext"/>s</param>
public sealed class WorkflowProcessFactory(IServiceProvider serviceProvider, IWorkflowExecutionContextFactory executionContextFactory)
    : IWorkflowProcessFactory
{

    /// <inheritdoc/>
    public async Task<IWorkflowProcess> CreateAsync(WorkflowDefinition definition, IWorkflowState state, WorkflowExecutionsOptions executionsOptions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(state);
        var executionContext = executionContextFactory.Create(definition, state, executionsOptions);
        var process = ActivatorUtilities.CreateInstance<WorkflowProcess>(serviceProvider, executionContext);
        await process.RunAsync().ConfigureAwait(false);
        return process;
    }

}
