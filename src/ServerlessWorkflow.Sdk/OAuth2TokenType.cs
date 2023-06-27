namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all supported token types
/// </summary>
public static class OAuth2TokenType
{

    /// <summary>
    /// Indicates an access token
    /// </summary>
    public const string AccessToken = "urn:ietf:params:oauth:token-type:access_token";

    /// <summary>
    /// Indicates an identity token
    /// </summary>
    public const string IdentityToken = "urn:ietf:params:oauth:token-type:id_token";

}
