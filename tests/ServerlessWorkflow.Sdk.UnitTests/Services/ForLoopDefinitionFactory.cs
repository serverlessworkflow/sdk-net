namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ForLoopDefinitionFactory
{
    internal static ForLoopDefinition Create() => new()
    {
        Each = "item",
        In = "${ .items }",
        At = "index"
    };
}
