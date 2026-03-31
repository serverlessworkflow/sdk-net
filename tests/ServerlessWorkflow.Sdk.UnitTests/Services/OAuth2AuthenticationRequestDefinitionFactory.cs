namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OAuth2AuthenticationRequestDefinitionFactory
{
    internal static OAuth2AuthenticationRequestDefinition Create() => new()
    {
        Encoding = OAuth2RequestEncoding.FormUrl
    };
}
