namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Represents the <see cref="Exception"/> thrown when an <see cref="IRuntimeError"/> has been raised during the execution of a workflow
/// </summary>
/// <param name="error">The <see cref="IRuntimeError"/> that has been raised</param>
public sealed class RuntimeErrorException(IRuntimeError error)
    : Exception
{

    /// <summary>
    /// Gets the <see cref="IRuntimeError"/> that has been raised
    /// </summary>
    public IRuntimeError Error { get; } = error;

}
