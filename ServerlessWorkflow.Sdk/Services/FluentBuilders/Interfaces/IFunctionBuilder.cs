namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="FunctionDefinition"/>s
/// </summary>
public interface IFunctionBuilder
    : IMetadataContainerBuilder<IFunctionBuilder>
{

    /// <summary>
    /// Sets the name of the <see cref="FunctionDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="FunctionDefinition"/> to build</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder WithName(string name);

    /// <summary>
    /// Sets the type of the <see cref="FunctionDefinition"/> to build
    /// </summary>
    /// <param name="type">The type of the <see cref="FunctionDefinition"/> to build</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder OfType(string type);

    /// <summary>
    /// Sets the <see cref="FunctionDefinition"/>'s operation expression. Sets the <see cref="FunctionDefinition"/>'s <see cref="FunctionDefinition.Type"/> to <see cref="FunctionType.Expression"/>
    /// </summary>
    /// <param name="operation">The <see cref="FunctionDefinition"/>'s operation expression</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder ForOperation(string operation);

    /// <summary>
    /// Sets the <see cref="FunctionDefinition"/>'s operation uri. Sets the <see cref="FunctionDefinition"/>'s <see cref="FunctionDefinition.Type"/> to <see cref="FunctionType.Rest"/>
    /// </summary>
    /// <param name="operation">The <see cref="FunctionDefinition"/>'s operation uri</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder ForOperation(Uri operation);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use the specified authentication definition
    /// </summary>
    /// <param name="authentication">The name of the authentication definition to use</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder UseAuthentication(string authentication);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use the specified authentication definition
    /// </summary>
    /// <param name="authenticationDefinition">The authentication definition to use</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder UseAuthentication(AuthenticationDefinition authenticationDefinition);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use an authentication definition with scheme <see cref="AuthenticationScheme.Basic"/>
    /// </summary>
    /// <param name="name">The name of the authentication definition to use</param>
    /// <param name="configurationAction">An <see cref="Action{T}"/> to setup the authentication definition to use</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder UseBasicAuthentication(string name, Action<IBasicAuthenticationBuilder> configurationAction);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use an authentication definition with scheme <see cref="AuthenticationScheme.Bearer"/>
    /// </summary>
    /// <param name="name">The name of the authentication definition to use</param>
    /// <param name="configurationAction">An <see cref="Action{T}"/> to setup the authentication definition to use</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder UseBearerAuthentication(string name, Action<IBearerAuthenticationBuilder> configurationAction);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use an authentication definition with scheme <see cref="AuthenticationScheme.OAuth2"/>
    /// </summary>
    /// <param name="name">The name of the authentication definition to use</param>
    /// <param name="configurationAction">An <see cref="Action{T}"/> to setup the authentication definition to use</param>
    /// <returns>The configured <see cref="IFunctionBuilder"/></returns>
    IFunctionBuilder UseOAuth2Authentication(string name, Action<IOAuth2AuthenticationBuilder> configurationAction);

    /// <summary>
    /// Builds the <see cref="FunctionDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="FunctionDefinition"/></returns>
    FunctionDefinition Build();

}
