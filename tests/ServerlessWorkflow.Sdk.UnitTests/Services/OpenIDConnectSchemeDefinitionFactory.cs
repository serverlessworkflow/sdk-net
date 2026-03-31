namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OpenIDConnectSchemeDefinitionFactory
{
    internal static OpenIDConnectSchemeDefinition Create() => new()
    {
        Authority = new Uri("https://auth.example.com"),
        Grant = OAuth2GrantType.AuthorizationCode,
        Client = OAuth2AuthenticationClientDefinitionFactory.Create(),
        Scopes = ["openid", "profile"]
    };
}
