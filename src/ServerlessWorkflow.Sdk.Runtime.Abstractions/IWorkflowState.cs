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
    /// Gets a reference to the workflow's definition
    /// </summary>
    WorkflowDefinitionReference Definition { get; }

    /// <summary>
    /// Gets the workflow's status
    /// </summary>
    string Status { get; }

    /// <summary>
    /// Gets the date and time at which the workflow has been created
    /// </summary>
    DateTimeOffset CreatedAt { get; }

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
    JsonObject ContextData { get; }

    /// <summary>
    /// Gets the workflow's output data, if any
    /// </summary>
    JsonNode? Output { get; }

    /// <summary>
    /// Gets the error, if any, that has occurred during the workflow's execution
    /// </summary>
    Error? Error { get; }

    /// <summary>
    /// Gets a value indicating whether the workflow is in an operative state
    /// </summary>
    bool IsOperative => Status == TaskStatus.Pending || Status == TaskStatus.Running || Status == TaskStatus.Suspended;

    /// <summary>
    /// Starts the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ResumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the workflow's output
    /// </summary>
    /// <param name="output">The workflow's output</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetOutputAsync(JsonNode? output, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the error that has occurred during the workflow's execution
    /// </summary>
    /// <param name="error">The error that has occurred during the workflow's execution</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}