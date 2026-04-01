namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class EmitTaskDefinitionFactory
{
    internal static EmitTaskDefinition Create() => new()
    {
        Emit = EventEmissionDefinitionFactory.Create()
    };
}
