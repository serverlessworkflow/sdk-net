namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build a authentication definition with scheme <see cref="AuthenticationScheme.OAuth2"/>
/// </summary>
public interface IOAuth2AuthenticationBuilder
    : IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Configures the authentication definition to use the specified <see cref="OAuth2GrantType"/> when requesting an access token
    /// </summary>
    /// <param name="grantType">The <see cref="OAuth2GrantType"/> to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder UseGrantType(string grantType);

    /// <summary>
    /// Configures the authentication definition to use the specified authority to generate an access token
    /// </summary>
    /// <param name="authority">The uri of the OAuth2 authority to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithAuthority(Uri authority);

    /// <summary>
    /// Configures the authentication definition to use the specified client ID when requesting an access token
    /// </summary>
    /// <param name="clientId">The client ID to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithClientId(string clientId);

    /// <summary>
    /// Configures the authentication definition to use the specified client secret when requesting an access token
    /// </summary>
    /// <param name="clientSecret">The username to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithClientSecret(string clientSecret);

    /// <summary>
    /// Configures the authentication definition to use the specified username to authenticate
    /// </summary>
    /// <param name="username">The username to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithUserName(string username);

    /// <summary>
    /// Configures the authentication definition to use the specified password to authenticate
    /// </summary>
    /// <param name="password">The password to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithPassword(string password);

    /// <summary>
    /// Configures the authentication definition to use the specified scopes when requesting an access token
    /// </summary>
    /// <param name="scopes">An array containing the scopes to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder UseScopes(params string[] scopes);

    /// <summary>
    /// Configures the authentication definition to use the specified audiences when requesting an access token
    /// </summary>
    /// <param name="audiences">An array containing the audiences to use</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder UseAudiences(params string[] audiences);

    /// <summary>
    /// Configures the token that represents the identity of the party on behalf of whom the request is being made.Typically, the subject of this token will be the subject of the security token issued in response to the request.
    /// </summary>
    /// <param name="tokenType">The type of the specified token</param>
    /// <param name="token">The subject token</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithSubjectToken(string tokenType, string token);

    /// <summary>
    /// Configures the token that represents the identity of the acting party.Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
    /// </summary>
    /// <param name="tokenType">The type of the specified token</param>
    /// <param name="token">The actor token</param>
    /// <returns>The configured <see cref="IOAuth2AuthenticationBuilder"/></returns>
    IOAuth2AuthenticationBuilder WithActorToken(string tokenType, string token);

}
