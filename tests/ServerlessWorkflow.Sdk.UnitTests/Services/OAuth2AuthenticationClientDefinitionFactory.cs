namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OAuth2AuthenticationClientDefinitionFactory
{
    internal static OAuth2AuthenticationClientDefinition Create() => new()
    {
        Id = "my-client-id",
        Secret = "my-client-secret",
        Authentication = "client_secret_post"
    };
}
