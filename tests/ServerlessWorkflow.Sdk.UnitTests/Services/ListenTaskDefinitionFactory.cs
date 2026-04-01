namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ListenTaskDefinitionFactory
{
    internal static ListenTaskDefinition Create() => new()
    {
        Listen = ListenerDefinitionFactory.Create()
    };
}
