namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EventFilterDefinitionFactory
{
    internal static EventFilterDefinition Create() => new()
    {
        With = new JsonObject
        {
            ["type"] = "com.example.event"
        },
        Correlate = new EquatableDictionary<string, CorrelationKeyDefinition>(
            new Dictionary<string, CorrelationKeyDefinition>
            {
                ["orderId"] = CorrelationKeyDefinitionFactory.Create()
            })
    };

    internal static EventFilterDefinition CreateSimple() => new()
    {
        With = new JsonObject
        {
            ["type"] = "com.example.event"
        }
    };
}
