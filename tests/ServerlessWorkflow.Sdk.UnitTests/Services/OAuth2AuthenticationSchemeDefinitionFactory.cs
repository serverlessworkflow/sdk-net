namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OAuth2AuthenticationSchemeDefinitionFactory
{
    internal static OAuth2AuthenticationSchemeDefinition Create() => new()
    {
        Authority = new Uri("https://auth.example.com"),
        Grant = OAuth2GrantType.ClientCredentials,
        Client = OAuth2AuthenticationClientDefinitionFactory.Create(),
        Endpoints = OAuth2AuthenticationEndpointsDefinitionFactory.Create(),
        Request = OAuth2AuthenticationRequestDefinitionFactory.Create(),
        Scopes = ["read", "write"],
        Audiences = ["api"],
        Issuers = ["https://auth.example.com"]
    };
}
