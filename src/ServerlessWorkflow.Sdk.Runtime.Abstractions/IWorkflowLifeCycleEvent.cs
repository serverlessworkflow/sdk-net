namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a workflow lifecycle event
/// </summary>
public interface IWorkflowLifeCycleEvent
{

    /// <summary>
    /// Gets the type of workflow lifecycle event
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Gets the workflow lifecycle event's data, if any
    /// </summary>
    object? Data { get; }

}