namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowLifeCycleEvent"/>
/// </summary>
/// <param name="Type">The <see cref="IWorkflowLifeCycleEvent"/>'s type</param>
/// <param name="Data">The <see cref="IWorkflowLifeCycleEvent"/>'s data, if any</param>
public sealed record WorkflowLifeCycleEvent(string Type, object? Data = null)
    : IWorkflowLifeCycleEvent
{

    /// <inheritdoc/>
    public string Type { get; init; } = Type;

    /// <inheritdoc/>
    public object? Data { get; init; } = Data;

}
