namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build an authentication definition
/// </summary>
public interface IAuthenticationDefinitionBuilder
{

    /// <summary>
    /// Sets the name of the authentication definition to build
    /// </summary>
    /// <param name="name">The name of the authentication definition to build</param>
    /// <returns>The configured <see cref="IAuthenticationDefinitionBuilder"/></returns>
    IAuthenticationDefinitionBuilder WithName(string name);

    /// <summary>
    /// Loads the authentication definition from a secret
    /// </summary>
    /// <param name="secret">The name of the secret to load the authentication definition from</param>
    void LoadFromSecret(string secret);

    /// <summary>
    /// Builds the authentication definition
    /// </summary>
    /// <returns>A new authentication definition</returns>
    AuthenticationDefinition Build();

}
