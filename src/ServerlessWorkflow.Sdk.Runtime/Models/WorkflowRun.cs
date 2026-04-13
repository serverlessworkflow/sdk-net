namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents a single run of a workflow, including start and end times
/// </summary>
[DataContract]
public sealed class WorkflowRun
    : IWorkflowRun
{

    /// <summary>
    /// Gets/sets the start time of the run
    /// </summary>
    [DataMember(Name = "startedAt", Order = 1), JsonPropertyName("startedAt"), JsonPropertyOrder(1)]
    public required DateTimeOffset StartedAt { get; set; }

    /// <summary>
    /// Gets/sets the end time of the run, if the workflow has completed
    /// </summary>
    [DataMember(Name = "endedAt", Order = 2), JsonPropertyName("endedAt"), JsonPropertyOrder(2)]
    public DateTimeOffset? EndedAt { get; set; }

}