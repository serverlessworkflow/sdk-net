namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ForTaskDefinitionFactory
{
    internal static ForTaskDefinition Create()
    {
        var tasks = new Map<string, TaskDefinition>();
        tasks.Add(new MapEntry<string, TaskDefinition>("processItem", TaskDefinitionFactory.CreateSetTask()));
        return new()
        {
            For = ForLoopDefinitionFactory.Create(),
            While = "${ .hasMore }",
            Do = tasks
        };
    }
}
