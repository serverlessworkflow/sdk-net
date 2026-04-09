namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the state of a task instance
/// </summary>
public interface ITaskState
{

    /// <summary>
    /// Gets the task's unique identifier
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the unique identifier of the workflow the task belongs to.
    /// </summary>
    string WorkflowId { get; }

    /// <summary>
    /// Gets the task's name, if any
    /// </summary>
    string? Name { get; }

    /// <summary>
    /// Gets a relative uri that references the task's definition
    /// </summary>
    JsonPointer Reference { get; }

    /// <summary>
    /// Gets a boolean indicating whether or not the task is part of an extension
    /// </summary>
    bool IsExtension { get; }

    /// <summary>
    /// Gets the id of the task's parent, if any
    /// </summary>
    string? ParentId { get; }

    /// <summary>
    /// Gets the date and time the task was created at
    /// </summary>
    DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// Gets the date and time the task has been started at, if applicable
    /// </summary>
    DateTimeOffset? StartedAt { get; }

    /// <summary>
    /// Gets the date and time the task has ended, if applicable
    /// </summary>
    DateTimeOffset? EndedAt { get; }

    /// <summary>
    /// Gets the task's status
    /// </summary>
    string? Status { get; }

    /// <summary>
    /// Gets the reason, if any, why the task is in its actual status
    /// </summary>
    string? StatusReason { get; }

    /// <summary>
    /// Gets the error, if any, that has occurred during the task's execution
    /// </summary>
    Error? Error { get; }

    /// <summary>
    /// Gets the task's input data
    /// </summary>
    JsonNode Input { get; }

    /// <summary>
    /// Gets the task's context data
    /// </summary>
    JsonObject ContextData { get; }

    /// <summary>
    /// Gets the task's output data, if any
    /// </summary>
    JsonNode? Output { get; }

    /// <summary>
    /// Gets the flow directive that must be performed next, if the task ran to completion
    /// </summary>
    string? Next { get; }

    /// <summary>
    /// Gets a value indicating whether the task is in an operative state
    /// </summary>
    bool IsOperative => Status == TaskInstanceStatus.Pending || Status == TaskInstanceStatus.Running || Status == TaskInstanceStatus.Suspended;

}