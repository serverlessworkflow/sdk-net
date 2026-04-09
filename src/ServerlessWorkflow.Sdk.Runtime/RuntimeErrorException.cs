namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Represents the <see cref="Exception"/> thrown when an <see cref="Error"/> has been raised during the execution of a workflow
/// </summary>
/// <param name="error">The <see cref="Error"/> that has been raised</param>
public sealed class RuntimeErrorException(Error error)
    : Exception
{

    /// <summary>
    /// Gets the <see cref="Error"/> that has been raised
    /// </summary>
    public Error Error { get; } = error;

}
