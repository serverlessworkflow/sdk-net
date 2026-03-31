namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OAuth2AuthenticationEndpointsDefinitionFactory
{
    internal static OAuth2AuthenticationEndpointsDefinition Create() => new()
    {
        Token = new Uri("/oauth2/token", UriKind.RelativeOrAbsolute),
        Revocation = new Uri("/oauth2/revoke", UriKind.RelativeOrAbsolute),
        Introspection = new Uri("/oauth2/introspect", UriKind.RelativeOrAbsolute)
    };
}
