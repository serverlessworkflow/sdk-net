namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a task life cycle event
/// </summary>
public interface ITaskLifeCycleEvent
{

    /// <summary>
    /// Gets the type of task life cycle event
    /// </summary>
    string Type { get; }

    /// <summary>
    /// Gets the task life cycle event's data, if any
    /// </summary>
    JsonObject? Data { get; }

}
