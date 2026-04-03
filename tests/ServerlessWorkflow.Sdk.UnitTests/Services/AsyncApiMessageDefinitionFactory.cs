namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class AsyncApiMessageDefinitionFactory
{
    internal static AsyncApiMessageDefinition Create() => new()
    {
        Payload = new JsonObject { ["userId"] = "123", ["email"] = "test@example.com" },
        Headers = new JsonObject { ["correlationId"] = "abc-123" }
    };
}
