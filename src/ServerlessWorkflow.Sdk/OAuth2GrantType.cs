namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Exposes the default OAUTH2 grant types
/// </summary>
public static class OAuth2GrantType
{

    /// <summary>
    /// Gets the 'authorization_code' OAUTH2 grant types
    /// </summary>
    public const string AuthorizationCode = "authorization_code";
    /// <summary>
    /// Gets the 'client_credentials' OAUTH2 grant types
    /// </summary>
    public const string ClientCredentials = "client_credentials";
    /// <summary>
    /// Gets the 'implicit' OAUTH2 grant types
    /// </summary>
    public const string Implicit = "implicit";
    /// <summary>
    /// Gets the 'password' OAUTH2 grant types
    /// </summary>
    public const string Password = "password";
    /// <summary>
    /// Gets the 'refresh_token' OAUTH2 grant types
    /// </summary>
    public const string RefreshToken = "refresh_token";
    /// <summary>
    /// Gets the 'token_exchange' OAUTH2 grant types
    /// </summary>
    public const string TokenExchange = "urn:ietf:params:oauth:grant-type:token-exchange";

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the OAUTH2 grant types supported by default
    /// </summary>
    public static readonly IEnumerable<string> All = AsEnumerable();

    /// <summary>
    /// Gets a new <see cref="IEnumerable{T}"/> containing the OAUTH2 grant types supported by default
    /// </summary>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the OAUTH2 grant types supported by default</returns>
    public static IEnumerable<string> AsEnumerable()
    {
        yield return AuthorizationCode;
        yield return ClientCredentials;
        yield return Implicit;
        yield return Password;
        yield return RefreshToken;
        yield return TokenExchange;
    }

}
