namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationEndpointsDefinitionBuilder"/> interface
/// </summary>
public sealed class OAuth2AuthenticationEndpointsDefinitionBuilder
    : IOAuth2AuthenticationEndpointsDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the relative path to the token endpoint. Defaults to /oauth2/token
    /// </summary>
    protected Uri Token { get; set; } = new("/oauth2/token", UriKind.Relative);

    /// <summary>
    /// Gets/sets the relative path to the revocation endpoint. Defaults to /oauth2/revoke
    /// </summary>
    protected Uri Revocation { get; set; } = new("/oauth2/revoke", UriKind.Relative);

    /// <summary>
    /// Gets/sets the relative path to the introspection endpoint. Defaults to /oauth2/introspect
    /// </summary>
    protected Uri Introspection { get; set; } = new("/oauth2/introspect", UriKind.Relative);

    /// <inheritdoc/>
    public IOAuth2AuthenticationEndpointsDefinitionBuilder WithTokenEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        Token = uri;
        IOAuth2AuthenticationEndpointsDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationEndpointsDefinitionBuilder WithRevocationEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        Revocation = uri;
        IOAuth2AuthenticationEndpointsDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationEndpointsDefinitionBuilder WithIntrospectionEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        Introspection = uri;
        IOAuth2AuthenticationEndpointsDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationEndpointsDefinition Build()
    {
        if (Token == null) throw new NullReferenceException("The token endpoint must be configured");
        if (Revocation == null) throw new NullReferenceException("The revocation endpoint must be configured");
        if (Introspection == null) throw new NullReferenceException("The introspection endpoint must be configured");
        return new()
        {
            Token = Token,
            Revocation = Revocation,
            Introspection = Introspection
        };
    }

}
