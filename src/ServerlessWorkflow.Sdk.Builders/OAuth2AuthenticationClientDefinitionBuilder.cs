namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationClientDefinitionBuilder"/> interface
/// </summary>
public sealed class OAuth2AuthenticationClientDefinitionBuilder
    : IOAuth2AuthenticationClientDefinitionBuilder
{

    string? id;
    string? secret;
    string? assertion;
    string? authentication;

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithId(string id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id);
        this.id = id;
        return this;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithSecret(string secret)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(secret);
        this.secret = secret;
        return this;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithAssertion(string assertion)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(assertion);
        this.assertion = assertion;
        return this;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationClientDefinitionBuilder WithAuthenticationMethod(string method)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(method);
        authentication = method;
        return this;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationClientDefinition Build() => new()
    {
        Id = id,
        Secret = secret,
        Assertion = assertion,
        Authentication = authentication
    };

}
