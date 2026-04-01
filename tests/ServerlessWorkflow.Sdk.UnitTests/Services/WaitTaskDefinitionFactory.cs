namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WaitTaskDefinitionFactory
{
    internal static WaitTaskDefinition Create() => new()
    {
        Wait = Duration.FromSeconds(30)
    };
}
