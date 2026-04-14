namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a task instance
/// </summary>
public interface ITaskInstance
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
    bool IsOperative => Status == TaskStatus.Pending || Status == TaskStatus.Running || Status == TaskStatus.Suspended;

    /// <summary>
    /// Gets/sets a list that contains the task's runs, if any
    /// </summary>
    IReadOnlyCollection<ITaskRun>? Runs { get; }

    /// <summary>
    /// Gets/sets a list that contains the task's retry attempts, if any
    /// </summary>
    IReadOnlyCollection<ITaskRetryAttempt>? Retries { get; }

    /// <summary>
    /// Starts the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the task's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes the task's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ResumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retries the task's execution
    /// </summary>
    /// <param name="cause">The error that caused the retry</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task RetryAsync(Error cause, CancellationToken cancellationToken = default);

    /// <summary>
    /// Skips the task
    /// </summary>
    /// <param name="output">The task's output, if any</param>
    /// <param name="next">The flow directive that must be performed next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SkipAsync(JsonNode? output, string next, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the task's output
    /// </summary>
    /// <param name="output">The task's output</param>
    /// <param name="next">The flow directive that must be performed next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetOutputAsync(JsonNode? output, string next, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the error that has occurred during the task's execution
    /// </summary>
    /// <param name="error">The error that has occurred during the task's execution</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the task's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}