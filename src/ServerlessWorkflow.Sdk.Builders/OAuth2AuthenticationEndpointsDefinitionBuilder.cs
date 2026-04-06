namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IOAuth2AuthenticationEndpointsDefinitionBuilder"/> interface
/// </summary>
public sealed class OAuth2AuthenticationEndpointsDefinitionBuilder
    : IOAuth2AuthenticationEndpointsDefinitionBuilder
{

    Uri token = new("/oauth2/token", UriKind.Relative);
    Uri revocation = new("/oauth2/revoke", UriKind.Relative);
    Uri introspection = new("/oauth2/introspect", UriKind.Relative);

    /// <inheritdoc/>
    public IOAuth2AuthenticationEndpointsDefinitionBuilder WithTokenEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        token = uri;
        IOAuth2AuthenticationEndpointsDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationEndpointsDefinitionBuilder WithRevocationEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        revocation = uri;
        IOAuth2AuthenticationEndpointsDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IOAuth2AuthenticationEndpointsDefinitionBuilder WithIntrospectionEndpoint(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri);
        if (uri.IsAbsoluteUri) throw new ArgumentException("The specified uri must be relative to the configured authority", nameof(uri));
        introspection = uri;
        IOAuth2AuthenticationEndpointsDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public OAuth2AuthenticationEndpointsDefinition Build()
    {
        if (token == null) throw new NullReferenceException("The token endpoint must be configured");
        if (revocation == null) throw new NullReferenceException("The revocation endpoint must be configured");
        if (introspection == null) throw new NullReferenceException("The introspection endpoint must be configured");
        return new()
        {
            Token = token,
            Revocation = revocation,
            Introspection = introspection
        };
    }

}
