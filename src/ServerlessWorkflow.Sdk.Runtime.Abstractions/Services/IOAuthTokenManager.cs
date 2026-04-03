namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to manage <see cref="IOAuth2Token"/>s
/// </summary>
public interface IOAuth2TokenManager
{

    /// <summary>
    /// Gets an <see cref="IOAuth2Token"/>
    /// </summary>
    /// <param name="configuration">The configuration that defines how to generate the <see cref="IOAuth2Token"/> to get</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>An <see cref="IOAuth2Token"/></returns>
    Task<IOAuth2Token> GetTokenAsync(OAuth2AuthenticationSchemeDefinitionBase configuration, CancellationToken cancellationToken = default);

}
