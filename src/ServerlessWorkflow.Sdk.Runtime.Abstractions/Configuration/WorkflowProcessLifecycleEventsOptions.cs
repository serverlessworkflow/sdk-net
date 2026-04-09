namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure the lifecycle events of an <see cref="IWorkflowProcess"/>
/// </summary>
public sealed class WorkflowProcessLifecycleEventsOptions
{

    /// <summary>
    /// Gets or sets a boolean value indicating whether or not to publish lifecycle events
    /// </summary>
    public bool Publish { get; set; } = true;

    /// <summary>
    /// Gets or sets the source to use when publishing lifecycle events.
    /// </summary>
    public Uri Source { get; set; } = new Uri("https://serverlessworkflow.io/runtime");

}