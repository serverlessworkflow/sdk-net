namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the state of a single run of a task
/// </summary>
public interface ITaskRun
{

    /// <summary>
    /// Gets the start time of the run
    /// </summary>
    DateTimeOffset StartedAt { get; }

    /// <summary>
    /// Gets the end time of the run, if the task has completed
    /// </summary>
    DateTimeOffset? EndedAt { get; }

    /// <summary>
    /// Gets the run's outcome or, in other words, the status of the task when the run ended
    /// </summary>
    string? Outcome { get; }

}
