namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class TaskDefinitionFactory
{
    internal static SetTaskDefinition CreateSetTask() => new()
    {
        Set = new()
        {
            ["key"] = "value"
        }
    };
}
