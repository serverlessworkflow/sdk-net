namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a workflow process, which holds the methods to manage a workflow's execution
/// </summary>
public interface IWorkflowProcess
    : IObservable<IWorkflowLifeCycleEvent>
{

    /// <summary>
    /// Gets the <see cref="IWorkflowInstance"/> being executed
    /// </summary>
    IWorkflowInstance Instance { get; }

    /// <summary>
    /// Gets the workflow's expression evaluator
    /// </summary>
    IRuntimeExpressionEvaluator Expressions { get; }

    /// <summary>
    /// Gets the service used to run workflows
    /// </summary>
    IWorkflowRuntime Runtime { get; }

    /// <summary>
    /// Waits for the workflow to reach a non-running state, such as completion, suspension, cancellation, or failure.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task WaitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ResumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}
