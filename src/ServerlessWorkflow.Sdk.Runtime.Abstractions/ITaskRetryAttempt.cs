namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of an object used to describe a task retry attempt
/// </summary>
public interface ITaskRetryAttempt
{

    /// <summary>
    /// Gets the retry attempt number
    /// </summary>
    uint Number { get; }

    /// <summary>
    /// Gets the date and time at which the retry attempt was performed
    /// </summary>
    DateTimeOffset Time { get; }

    /// <summary>
    /// Gets the <see cref="Error"/> that is the cause of the try attempt
    /// </summary>
    Error Cause { get; }

}
