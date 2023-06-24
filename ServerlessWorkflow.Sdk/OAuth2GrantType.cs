namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Enumerates all <see href="https://datatracker.ietf.org/doc/html/rfc6749#section-4">OAuth 2 grant types</see> supported for workflow runtime token generation
/// </summary>
public static class OAuth2GrantType
{

    /// <summary>
    /// Indicates the <see href="https://datatracker.ietf.org/doc/html/rfc6749#section-4.3">resource-owner password credentials grant type</see>
    /// </summary>
    public const string Password = "password";

    /// <summary>
    /// Indicates the <see href="https://datatracker.ietf.org/doc/html/rfc6749#section-4.4">client credentials grant type</see>
    /// </summary>
    public const string ClientCredentials = "client_credentials";

    /// <summary>
    /// Indicates the <see href="https://datatracker.ietf.org/doc/html/rfc8693">token exchange grant type</see>
    /// </summary>
    public const string TokenExchange = "urn:ietf:params:oauth:grant-type:token-exchange";

}
