namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ErrorFilterDefinitionFactory
{
    internal static ErrorFilterDefinition Create() => new()
    {
        With = new JsonObject
        {
            ["status"] = 503,
            ["type"] = "https://example.com/errors/service-unavailable"
        }
    };
}
