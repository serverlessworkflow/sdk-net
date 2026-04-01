namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Exposes all default types of events that can be emitted during a task lifecycle
/// </summary>
public static class TaskLifeCycleEventType
{
    /// <summary>
    /// Indicates that the task has been initialized
    /// </summary>
    public const string Initialized = "initialized";
    /// <summary>
    /// Indicates that the task is running
    /// </summary>
    public const string Running = "running";
    /// <summary>
    /// Indicates that the task has been suspended
    /// </summary>
    public const string Suspended = "suspended";
    /// <summary>
    /// Indicates that the task has been cancelled
    /// </summary>
    public const string Cancelled = "cancelled";
    /// <summary>
    /// Indicates that the task has faulted
    /// </summary>
    public const string Faulted = "faulted";
    /// <summary>
    /// Indicates that the task ran to completion
    /// </summary>
    public const string Completed = "completed";
    /// <summary>
    /// Indicates that the task has been skipped
    /// </summary>
    public const string Skipped = "skipped";

}