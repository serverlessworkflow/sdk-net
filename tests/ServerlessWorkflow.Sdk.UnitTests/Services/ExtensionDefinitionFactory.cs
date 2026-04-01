namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ExtensionDefinitionFactory
{
    internal static ExtensionDefinition Create()
    {
        var beforeTasks = new Map<string, TaskDefinition>
        {
            new MapEntry<string, TaskDefinition>("log", TaskDefinitionFactory.CreateSetTask())
        };
        return new()
        {
            Extend = "call",
            When = "${ .call == \"http\" }",
            Before = beforeTasks
        };
    }
}
