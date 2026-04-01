namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class LinearBackoffDefinitionFactory
{
    internal static LinearBackoffDefinition Create() => new()
    {
        Increment = Duration.FromSeconds(5)
    };
}
