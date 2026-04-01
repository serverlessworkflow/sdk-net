#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IWorkflowExecutionContext"/>s.
/// </summary>
public static class IWorkflowExecutionContextExtensions
{

    /// <summary>
    /// Gets an object describing the current workflow execution context
    /// </summary>
    /// <param name="workflow">The <see cref="IWorkflowExecutionContext"/> to get the descriptor of</param>
    /// <returns>A new <see cref="WorkflowDescriptor"/> describing the current workflow execution context</returns>
    public static WorkflowDescriptor GetDescriptor(this IWorkflowExecutionContext workflow) => new()
    {
        Id = workflow.Instance.State.Id,
        Definition = workflow.Definition,
        Input = workflow.Instance.State.Input,
        StartedAt = workflow.Instance.State.StartedAt?.GetDescriptor(),
    };

}
