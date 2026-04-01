namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EventConsumptionStrategyDefinitionFactory
{
    internal static EventConsumptionStrategyDefinition CreateOne() => new()
    {
        One = EventFilterDefinitionFactory.CreateSimple()
    };

    internal static EventConsumptionStrategyDefinition CreateAll() => new()
    {
        All = [EventFilterDefinitionFactory.CreateSimple()]
    };

    internal static EventConsumptionStrategyDefinition CreateAny() => new()
    {
        Any = [EventFilterDefinitionFactory.CreateSimple()]
    };
}
