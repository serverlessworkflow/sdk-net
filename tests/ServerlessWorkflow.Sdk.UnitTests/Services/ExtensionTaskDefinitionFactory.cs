namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ExtensionTaskDefinitionFactory
{
    internal static ExtensionTaskDefinition Create() => new()
    {
        ExtensionData = new Dictionary<string, JsonElement>()
        {
            ["customProperty"] = JsonElement.Parse("\"customValue\"")
        }
    };
}
