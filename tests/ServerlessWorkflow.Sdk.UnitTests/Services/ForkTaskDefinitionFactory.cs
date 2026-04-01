namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ForkTaskDefinitionFactory
{
    internal static ForkTaskDefinition Create() => new()
    {
        Fork = BranchingDefinitionFactory.Create()
    };
}
