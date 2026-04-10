namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Exposes default workflow instance statuses
/// </summary>
public static class WorkflowStatus
{

    /// <summary>
    /// Indicates that the workflow is pending execution
    /// </summary>
    public const string Pending = "pending";
    /// <summary>
    /// Indicates that the workflow is being executed
    /// </summary>
    public const string Running = "running";
    /// <summary>
    /// Indicates that the workflow's execution has been suspended
    /// </summary>
    public const string Suspended = "suspended";
    /// <summary>
    /// Indicates that the workflow's execution is waiting for event(s)
    /// </summary>
    public const string Waiting = "waiting";
    /// <summary>
    /// Indicates that the workflow ran to completion
    /// </summary>
    public const string Completed = "completed";
    /// <summary>
    /// Indicates that the workflow's execution has been cancelled
    /// </summary>
    public const string Cancelled = "cancelled";
    /// <summary>
    /// Indicates that the workflow encountered an unhandled error during its execution and consequently faulted
    /// </summary>
    public const string Faulted = "faulted";

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing default workflow statuses
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing default workflow statuses</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Pending;
        yield return Running;
        yield return Suspended;
        yield return Waiting;
        yield return Completed;
        yield return Cancelled;
        yield return Faulted;
    }

}