namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationClientDefinitionBuilder"/> interface
/// </summary>
public sealed class OAuth2AuthenticationClientDefinitionBuilder
    : IOAuth2AuthenticationClientDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the OAUTH2 client_id to use
    /// </summary>
    protected string? Id { get; set; }

    /// <summary>
    /// Gets/sets the OAUTH2 client_secret to use, if any
    /// </summary>
    protected string? Secret { get; set; }

    /// <summary>
    /// Gets/sets a JWT containing a signed assertion with the application credentials
    /// </summary>
    protected string? Assertion { get; set; }

    /// <summary>
    /// Gets/sets the authentication method to use to authenticate the client
    /// </summary>
    protected string? Authentication { get; set; }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithId(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        Id = id;
        IOAuth2AuthenticationClientDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        Secret = secret;
        IOAuth2AuthenticationClientDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithAssertion(string assertion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(assertion);
        Assertion = assertion;
        IOAuth2AuthenticationClientDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithAuthenticationMethod(string method)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
        Authentication = method;
        IOAuth2AuthenticationClientDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationClientDefinition Build() => new()
    {
        Id = Id,
        Secret = Secret,
        Assertion = Assertion,
        Authentication = Authentication
    };

}
