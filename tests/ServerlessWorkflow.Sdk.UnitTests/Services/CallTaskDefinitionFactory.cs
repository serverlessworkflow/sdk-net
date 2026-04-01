namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class CallTaskDefinitionFactory
{
    internal static CallTaskDefinition Create() => new()
    {
        Call = "http",
        With = new JsonObject
        {
            ["method"] = "GET",
            ["uri"] = "https://api.example.com/data"
        }
    };
}
