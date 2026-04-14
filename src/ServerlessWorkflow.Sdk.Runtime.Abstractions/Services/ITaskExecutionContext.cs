namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of the context of a task's execution
/// </summary>
public interface ITaskExecutionContext
{

    /// <summary>
    /// Gets the workflow the task to execute belongs to
    /// </summary>
    IWorkflowExecutionContext Workflow { get; }

    /// <summary>
    /// Gets the <see cref="TaskDefinition"/> of the task to execute
    /// </summary>
    TaskDefinition Definition { get; }

    /// <summary>
    /// Gets the task to execute
    /// </summary>
    ITaskInstance Instance { get; }

    /// <summary>
    /// Gets a name/value mapping of the task's arguments, if any
    /// </summary>
    JsonObject? Arguments { get; }

    /// <summary>
    /// Executes the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Streams events
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IObservable{T}"/> used to stream <see cref="ICloudEvent"/>s</returns>
    Task<IObservable<IStreamedCloudEvent>> StreamAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins correlating events
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The resulting <see cref="ICorrelationContext"/></returns>
    Task<ICorrelationContext> CorrelateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes the specified <see cref="ICloudEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="ICloudEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retries the task
    /// </summary>
    /// <param name="cause">The <see cref="Error"/> to retry the task for</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task RetryAsync(Error cause, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets an <see cref="Error"/> that has occurred during the task's execution
    /// </summary>
    /// <param name="error">The <see cref="Error"/> that has occurred</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the task's result, if any
    /// </summary>
    /// <param name="result">The task's result, if any</param>
    /// <param name="then">The <see cref="FlowDirective"/> to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetResultAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the task's context data
    /// </summary>
    /// <param name="contextData">The task's context data</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetContextDataAsync(JsonObject contextData, CancellationToken cancellationToken = default);

    /// <summary>
    /// Skips the task
    /// </summary>
    /// <param name="result">The task's result, if any</param>
    /// <param name="then">The <see cref="FlowDirective"/> to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SkipAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the subtasks the task is made out of
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate <see cref="ITaskInstance">subtasks</see></returns>
    IAsyncEnumerable<ITaskInstance> GetSubTasksAsync(CancellationToken cancellationToken = default);

}

/// <summary>
/// Defines the fundamentals of the context of a task's execution
/// </summary>
/// <typeparam name="TDefinition">The type of task to run</typeparam>
public interface ITaskExecutionContext<TDefinition>
    : ITaskExecutionContext
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets the <see cref="TaskDefinition"/> of the task to execute
    /// </summary>
    new TDefinition Definition { get; }

}
