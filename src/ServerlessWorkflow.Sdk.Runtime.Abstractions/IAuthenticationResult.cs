namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of an authentication result
/// </summary>
public interface IAuthenticationResult
{

    /// <summary>
    /// Gets the authentication scheme
    /// </summary>
    string Scheme { get; }

    /// <summary>
    /// Gets the authentication value
    /// </summary>
    string Value { get; }

}