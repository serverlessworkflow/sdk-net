namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EventEmissionDefinitionFactory
{
    internal static EventEmissionDefinition Create() => new()
    {
        Event = EventDefinitionFactory.Create()
    };
}
