namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OAuth2TokenDefinitionFactory
{
    internal static OAuth2TokenDefinition Create() => new()
    {
        Token = "eyJhbGciOiJIUzI1NiJ9",
        Type = "urn:ietf:params:oauth:token-type:access_token"
    };
}
