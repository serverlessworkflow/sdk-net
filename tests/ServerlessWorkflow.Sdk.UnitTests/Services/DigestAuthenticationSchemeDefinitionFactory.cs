namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class DigestAuthenticationSchemeDefinitionFactory
{
    internal static DigestAuthenticationSchemeDefinition Create() => new()
    {
        Username = "admin",
        Password = "p@ssw0rd"
    };
}
