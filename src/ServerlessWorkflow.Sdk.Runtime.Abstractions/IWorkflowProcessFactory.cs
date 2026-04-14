namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IWorkflowProcess"/>es
/// </summary>
public interface IWorkflowProcessFactory
{

    /// <summary>
    /// Creates a new <see cref="IWorkflowProcess"/>
    /// </summary>
    /// <param name="definition">The <see cref="WorkflowDefinition"/> to create the process for</param>
    /// <param name="state">The <see cref="IWorkflowInstance"/> to create the process for</param>
    /// <param name="executionOptions">The options used to configure the workflow's execution</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowProcess"/></returns>
    Task<IWorkflowProcess> CreateAsync(WorkflowDefinition definition, IWorkflowInstance state, WorkflowExecutionsOptions executionOptions, CancellationToken cancellationToken = default);

}