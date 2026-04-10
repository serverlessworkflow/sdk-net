#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IWorkflowExecutionContext"/>s
/// </summary>
public static class IWorkflowExecutionContextExtensions
{

    /// <summary>
    /// Gets a new <see cref="WorkflowDescriptor"/> used to describe the <see cref="IWorkflowExecutionContext"/>
    /// </summary>
    /// <param name="workflow">The <see cref="IWorkflowExecutionContext"/> to describe</param>
    /// <returns>A new <see cref="WorkflowDescriptor"/></returns>
    public static WorkflowDescriptor GetDescriptor(this IWorkflowExecutionContext workflow)
    {
        return new()
        {
            Id = workflow.State.Id,
            Definition = workflow.Definition,
            Input = workflow.State.Input,
            StartedAt = workflow.State.StartedAt?.GetDescriptor()
        };
    }

}
