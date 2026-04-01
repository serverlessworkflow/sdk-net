namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class TryTaskDefinitionFactory
{
    internal static TryTaskDefinition Create()
    {
        var tryTasks = new Map<string, TaskDefinition>();
        tryTasks.Add(new MapEntry<string, TaskDefinition>("riskyTask", TaskDefinitionFactory.CreateSetTask()));
        return new()
        {
            Try = tryTasks,
            Catch = ErrorCatcherDefinitionFactory.Create()
        };
    }
}
