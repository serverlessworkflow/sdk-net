namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class BasicAuthenticationSchemeDefinitionFactory
{
    internal static BasicAuthenticationSchemeDefinition Create() => new()
    {
        Username = "admin",
        Password = "p@ssw0rd"
    };
}
