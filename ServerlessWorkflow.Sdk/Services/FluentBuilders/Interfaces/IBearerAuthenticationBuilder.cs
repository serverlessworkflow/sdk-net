namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build a authentication definition with scheme <see cref="AuthenticationScheme.Bearer"/>
/// </summary>
public interface IBearerAuthenticationBuilder
    : IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Configures the authentication definition to use the specified token to authenticate
    /// </summary>
    /// <param name="token">The token to use</param>
    /// <returns>The configured <see cref="IBasicAuthenticationBuilder"/></returns>
    IBearerAuthenticationBuilder WithToken(string token);

}
