namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class SchemaDefinitionFactory
{
    internal static SchemaDefinition Create() => new()
    {
        Format = SchemaFormat.Json,
        Document = new JsonObject
        {
            ["type"] = "object",
            ["properties"] = new JsonObject
            {
                ["name"] = new JsonObject { ["type"] = "string" }
            }
        }
    };

    internal static SchemaDefinition CreateWithResource() => new()
    {
        Format = SchemaFormat.Json,
        Resource = ExternalResourceDefinitionFactory.Create()
    };
}
