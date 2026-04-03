namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a task instance
/// </summary>
public interface ITaskInstance
{

    /// <summary>
    /// Gets the current state of the task instance
    /// </summary>
    ITaskState State { get; }

    /// <summary>
    /// Gets the subtasks the task is made out of
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate subtasks</returns>
    IAsyncEnumerable<ITaskInstance> GetSubTasksAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task InitializeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retries the task
    /// </summary>
    /// <param name="cause">The error that caused the retry attempt</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task RetryAsync(IRuntimeError cause, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets an error that has occurred during the task's execution
    /// </summary>
    /// <param name="error">The error that has occurred</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(IRuntimeError error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the task's context data
    /// </summary>
    /// <param name="context">The updated context data</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetContextDataAsync(JsonObject context, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the task's result
    /// </summary>
    /// <param name="result">The task's result, if any</param>
    /// <param name="then">The flow directive to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetResultAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Skips the task
    /// </summary>
    /// <param name="result">The task's result, if any</param>
    /// <param name="then">The flow directive to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SkipAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the task's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}

/// <summary>
/// Defines the fundamentals of a task instance with a strongly-typed state
/// </summary>
/// <typeparam name="TState">The type of the task's state</typeparam>
public interface ITaskInstance<TState>
    : ITaskInstance
     where TState : class, ITaskState
{

    /// <summary>
    /// Gets the current state of the task instance
    /// </summary>
    new TState State { get; }

}