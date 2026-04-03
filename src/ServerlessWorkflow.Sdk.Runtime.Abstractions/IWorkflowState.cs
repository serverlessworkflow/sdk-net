namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a workflow state
/// </summary>
public interface IWorkflowState
{

    /// <summary>
    /// Gets the workflow's unique identifier
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the workflow's status
    /// </summary>
    string Status { get; }

    /// <summary>
    /// Gets the date and time the workflow has been started at, if applicable
    /// </summary>
    DateTimeOffset? StartedAt { get; }

    /// <summary>
    /// Gets the date and time the workflow has ended, if applicable
    /// </summary>
    DateTimeOffset? EndedAt { get; }

    /// <summary>
    /// Gets the workflow's input data
    /// </summary>
    JsonObject? Input { get; }

    /// <summary>
    /// Gets the workflow's context data
    /// </summary>
    JsonObject? ContextData { get; }

    /// <summary>
    /// Gets the workflow's output data, if any
    /// </summary>
    JsonNode? Output { get; }

    /// <summary>
    /// Gets the error, if any, that has occurred during the workflow's execution
    /// </summary>
    IRuntimeError? Error { get; }

    /// <summary>
    /// Gets a value indicating whether the workflow is in an operative state
    /// </summary>
    bool IsOperative => Status == TaskInstanceStatus.Pending || Status == TaskInstanceStatus.Running || Status == TaskInstanceStatus.Suspended;

}