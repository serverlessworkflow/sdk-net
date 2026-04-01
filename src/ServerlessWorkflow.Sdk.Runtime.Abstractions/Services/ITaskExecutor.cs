namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to execute a task
/// </summary>
public interface ITaskExecutor
    : IObservable<ITaskLifeCycleEvent>, IDisposable, IAsyncDisposable
{

    /// <summary>
    /// Gets the <see cref="ITaskInstance"/> to run
    /// </summary>
    ITaskExecutionContext Task { get; }

    /// <summary>
    /// Initializes the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task InitializeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Runs the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ExecuteAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retries to run the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="cause">The <see cref="Error"/> that caused the retry attempt</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task RetryAsync(Error cause, CancellationToken cancellationToken = default);

    /// <summary>
    /// Faults the handled <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="error"></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the <see cref="ITaskInstance"/>'s result and transitions to '<see cref="TaskInstanceStatus.Completed"/>'.
    /// </summary>
    /// <param name="result">The <see cref="ITaskInstance"/>'s result, if any</param>
    /// <param name="then">The <see cref="FlowDirective"/> to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task SetResultAsync(object? result = null, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}

/// <summary>
/// Defines the fundamentals of a service used to execute a task
/// </summary>
public interface ITaskExecutor<TDefinition>
    : ITaskExecutor
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets the <see cref="TaskInstance"/> to run
    /// </summary>
    new ITaskExecutionContext<TDefinition> Task { get; }

}
