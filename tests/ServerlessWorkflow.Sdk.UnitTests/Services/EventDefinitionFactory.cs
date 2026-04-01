namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EventDefinitionFactory
{
    internal static EventDefinition Create() => new()
    {
        With = new JsonObject
        {
            ["type"] = "com.example.event",
            ["source"] = "https://example.com"
        }
    };
}
