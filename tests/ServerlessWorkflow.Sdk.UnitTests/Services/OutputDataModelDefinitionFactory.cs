namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class OutputDataModelDefinitionFactory
{
    internal static OutputDataModelDefinition Create() => new()
    {
        Schema = SchemaDefinitionFactory.Create(),
        As = new JsonObject
        {
            ["result"] = ".output"
        }
    };

    internal static OutputDataModelDefinition CreateWithExpression() => new()
    {
        Schema = SchemaDefinitionFactory.Create(),
        As = ".output"
    };
}
