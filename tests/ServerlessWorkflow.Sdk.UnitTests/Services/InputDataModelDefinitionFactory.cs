namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class InputDataModelDefinitionFactory
{
    internal static InputDataModelDefinition Create() => new()
    {
        Schema = SchemaDefinitionFactory.Create(),
        From = ".input"
    };
}
