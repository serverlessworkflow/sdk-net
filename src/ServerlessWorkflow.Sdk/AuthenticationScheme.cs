namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all supported authentication schemes
/// </summary>
public static class AuthenticationScheme
{

    /// <summary>
    /// Gets the 'basic' authentication scheme
    /// </summary>
    public const string Basic = "basic";
    /// <summary>
    /// Gets the 'bearer' authentication scheme
    /// </summary>
    public const string Bearer = "bearer";
    /// <summary>
    /// Gets the 'oauth2' authentication scheme
    /// </summary>
    public const string OAuth2 = "oauth2";

}
