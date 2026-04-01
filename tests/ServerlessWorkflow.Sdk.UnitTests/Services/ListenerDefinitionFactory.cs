namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ListenerDefinitionFactory
{
    internal static ListenerDefinition Create() => new()
    {
        To = EventConsumptionStrategyDefinitionFactory.CreateOne(),
        Read = EventReadMode.Data
    };
}
