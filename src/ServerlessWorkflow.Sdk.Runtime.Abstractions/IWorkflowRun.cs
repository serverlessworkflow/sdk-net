namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a workflow run
/// </summary>
public interface IWorkflowRun
{

    /// <summary>
    /// Gets/sets the start time of the run
    /// </summary>
    DateTimeOffset StartedAt { get; }

    /// <summary>
    /// Gets/sets the end time of the run, if the workflow has completed
    /// </summary>
    DateTimeOffset? EndedAt { get; }

}