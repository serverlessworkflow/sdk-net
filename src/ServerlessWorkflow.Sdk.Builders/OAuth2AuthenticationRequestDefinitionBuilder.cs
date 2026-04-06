namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationRequestDefinitionBuilder"/> interface
/// </summary>
public sealed class OAuth2AuthenticationRequestDefinitionBuilder
    : IOAuth2AuthenticationRequestDefinitionBuilder
{

    string? encoding;

    /// <inheritdoc/>
    public IOAuth2AuthenticationRequestDefinitionBuilder WithEncoding(string encoding)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encoding);
        this.encoding = encoding;
        return this;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationRequestDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(encoding)) throw new NullReferenceException("The request encoding must be set");
        return new() 
        { 
            Encoding = encoding 
        };
    }

}
