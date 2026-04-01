namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ContainerLifetimeDefinitionFactory
{
    internal static ContainerLifetimeDefinition Create() => new()
    {
        Cleanup = ContainerCleanupPolicy.Eventually,
        Duration = Duration.FromMinutes(30)
    };
}
