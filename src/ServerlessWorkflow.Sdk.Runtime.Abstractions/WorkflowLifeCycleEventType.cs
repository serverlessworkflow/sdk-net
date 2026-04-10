namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Exposes all default types of events that can be emitted during a workflow lifecycle
/// </summary>
public static class WorkflowLifeCycleEventType
{
    /// <summary>
    /// Indicates that the workflow has been initialized
    /// </summary>
    public const string Initialized = "initialized";
    /// <summary>
    /// Indicates that the workflow is running
    /// </summary>
    public const string Running = "running";
    /// <summary>
    /// Indicates that the workflow has been suspended
    /// </summary>
    public const string Suspended = "suspended";
    /// <summary>
    /// Indicates that the workflow has been cancelled
    /// </summary>
    public const string Cancelled = "cancelled";
    /// <summary>
    /// Indicates that the workflow has faulted
    /// </summary>
    public const string Faulted = "faulted";
    /// <summary>
    /// Indicates that the workflow ran to completion
    /// </summary>
    public const string Completed = "completed";

}