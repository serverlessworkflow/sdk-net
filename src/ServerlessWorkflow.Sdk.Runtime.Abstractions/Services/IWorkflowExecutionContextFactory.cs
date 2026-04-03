namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IWorkflowExecutionContext"/>s
/// </summary>
public interface IWorkflowExecutionContextFactory
{

    /// <summary>
    /// Creates a new <see cref="IWorkflowExecutionContext"/> implementation
    /// </summary>
    /// <returns>A new <see cref="IWorkflowExecutionContext"/></returns>
    IWorkflowExecutionContext Create();

}