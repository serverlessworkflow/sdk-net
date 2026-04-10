namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IWorkflowExecutionContext"/>s
/// </summary>
public interface IWorkflowExecutionContextFactory
{

    /// <summary>
    /// Creates a new <see cref="IWorkflowExecutionContext"/>
    /// </summary>
    /// <param name="definition">The <see cref="WorkflowDefinition"/> to create the context for</param>
    /// <param name="state">The <see cref="IWorkflowState"/> to create the context for</param>
    /// <param name="executionsOptions">The options used to configure the workflow's execution</param>
    /// <returns>A new <see cref="IWorkflowExecutionContext"/></returns>
    IWorkflowExecutionContext Create(WorkflowDefinition definition, IWorkflowState state, WorkflowExecutionsOptions executionsOptions);

}