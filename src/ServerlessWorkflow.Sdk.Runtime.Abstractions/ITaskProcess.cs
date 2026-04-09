namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a task process, which holds the methods to manage a task's execution
/// </summary>
public interface ITaskProcess
{

    /// <summary>
    /// Gets the <see cref="ITaskInstance"/> being executed
    /// </summary>
    ITaskInstance Instance { get; }

    /// <summary>
    /// Gets the <see cref="IWorkflowProcess"/> the <see cref="ITaskProcess"/> belongs to
    /// </summary>
    IWorkflowProcess Workflow { get; }

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
    Task RetryAsync(Error cause, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets an error that has occurred during the task's execution
    /// </summary>
    /// <param name="error">The error that has occurred</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

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
/// Defines the fundamentals of a task process, which holds the methods to manage a task's execution
/// </summary>
/// <typeparam name="TDefinition">The type of the task's definition</typeparam>
public interface ITaskProcess<TDefinition>
    : ITaskProcess
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets the <see cref="ITaskInstance"/> being executed
    /// </summary>
    new ITaskInstance<TDefinition> Instance { get; }

}