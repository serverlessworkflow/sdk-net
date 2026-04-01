namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class DoTaskDefinitionFactory
{
    internal static DoTaskDefinition Create()
    {
        var tasks = new Map<string, TaskDefinition>();
        tasks.Add(new MapEntry<string, TaskDefinition>("setValues", TaskDefinitionFactory.CreateSetTask()));
        return new()
        {
            Do = tasks
        };
    }
}
