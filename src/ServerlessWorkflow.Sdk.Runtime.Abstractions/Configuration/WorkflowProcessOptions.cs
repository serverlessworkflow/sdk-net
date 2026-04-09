namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure an <see cref="IWorkflowProcess"/>
/// </summary>
public sealed class WorkflowProcessOptions
{

    /// <summary>
    /// Gets or sets the options used to configure the <see cref="IWorkflowProcess"/>'s lifecycle events
    /// </summary>
    public WorkflowProcessLifecycleEventsOptions LifecycleEvents { get; set; } = new();

}
