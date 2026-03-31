namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class BearerAuthenticationSchemeDefinitionFactory
{
    internal static BearerAuthenticationSchemeDefinition Create() => new()
    {
        Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9"
    };
}
