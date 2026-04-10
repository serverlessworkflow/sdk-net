namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Exposes default task instance statuses
/// </summary>
public static class TaskStatus
{

    /// <summary>
    /// Indicates that the task has been created and is pending execution
    /// </summary>
    public const string Pending = "pending";
    /// <summary>
    /// Indicates that the task is running
    /// </summary>
    public const string Running = "running";
    /// <summary>
    /// Indicates that the task encountered an error or exception during execution
    /// </summary>
    public const string Faulted = "faulted";
    /// <summary>
    /// Indicates that the task has been explicitly omitted or bypassed during execution, probably because that task failed the condition defined by its `if` property
    /// </summary>
    public const string Skipped = "skipped";
    /// <summary>
    /// Indicates that the task was suspended
    /// </summary>
    public const string Suspended = "suspended";
    /// <summary>
    /// Indicates that the task was terminated or aborted before completion
    /// </summary>
    public const string Cancelled = "cancelled";
    /// <summary>
    /// Indicates that the task ran to completion
    /// </summary>
    public const string Completed = "completed";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing default task statuses
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing default task statuses</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return Pending;
        yield return Running;
        yield return Faulted;
        yield return Skipped;
        yield return Cancelled;
        yield return Completed;
    }

}
