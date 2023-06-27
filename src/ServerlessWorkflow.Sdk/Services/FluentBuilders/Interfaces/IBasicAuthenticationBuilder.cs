namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build a authentication definition with scheme <see cref="AuthenticationScheme.Basic"/>
/// </summary>
public interface IBasicAuthenticationBuilder
    : IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Configures the authentication definition to use the specified username to authenticate
    /// </summary>
    /// <param name="username">The username to use</param>
    /// <returns>The configured <see cref="IBasicAuthenticationBuilder"/></returns>
    IBasicAuthenticationBuilder WithUserName(string username);

    /// <summary>
    /// Configures the authentication definition to use the specified password to authenticate
    /// </summary>
    /// <param name="password">The password to use</param>
    /// <returns>The configured <see cref="IBasicAuthenticationBuilder"/></returns>
    IBasicAuthenticationBuilder WithPassword(string password);

}
