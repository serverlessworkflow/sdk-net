namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationRequestDefinitionBuilder"/> interface
/// </summary>
public sealed class OAuth2AuthenticationRequestDefinitionBuilder
    : IOAuth2AuthenticationRequestDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the encoding of the authentication request. Defaults to 'application/x-www-form-urlencoded'
    /// </summary>
    protected string? Encoding { get; set; }

    /// <inheritdoc/>
    public IOAuth2AuthenticationRequestDefinitionBuilder WithEncoding(string encoding)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encoding);
        Encoding = encoding;
        IOAuth2AuthenticationRequestDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationRequestDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(Encoding)) throw new NullReferenceException("The request encoding must be set");
        return new() { Encoding = Encoding };
    }

}
