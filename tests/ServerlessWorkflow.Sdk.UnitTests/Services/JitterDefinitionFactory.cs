namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class JitterDefinitionFactory
{
    internal static JitterDefinition Create() => new()
    {
        From = Duration.FromSeconds(1),
        To = Duration.FromSeconds(10)
    };
}
