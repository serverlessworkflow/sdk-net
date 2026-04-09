#pragma warning disable IDE0130 // Namespace does not match folder structure

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines extensions for <see cref="IWorkflowInstance"/>s.
/// </summary>
public static class IWorkflowInstanceExtensions
{

    /// <summary>
    /// Gets the qualified name of the <see cref="IWorkflowInstance"/>, which is a combination of the qualified name of its definition and its state id.
    /// </summary>
    /// <param name="instance">The <see cref="IWorkflowInstance"/> for which to get the qualified name.</param>
    /// <returns>The qualified name of the workflow instance.</returns>
    public static string GetQualifiedName(this IWorkflowInstance instance) => $"{instance.Definition.GetQualifiedName()}-{instance.State.Id}";

    /// <summary>
    /// Gets an object describing the <see cref="IWorkflowInstance"/>
    /// </summary>
    /// <param name="workflow">The <see cref="IWorkflowInstance"/> to get the descriptor of</param>
    /// <returns>A new <see cref="WorkflowDescriptor"/> describing the current task execution context</returns>
    public static WorkflowDescriptor GetDescriptor(this IWorkflowInstance workflow) => new()
    {
        Id = workflow.State.Id,
        Definition = workflow.Definition,
        Input = workflow.State.Input,
        StartedAt = workflow.State.StartedAt?.GetDescriptor()
    };

}