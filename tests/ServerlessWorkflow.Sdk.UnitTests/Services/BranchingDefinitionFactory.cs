namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class BranchingDefinitionFactory
{
    internal static BranchingDefinition Create()
    {
        var branches = new Map<string, TaskDefinition>
        {
            new MapEntry<string, TaskDefinition>("branch1", TaskDefinitionFactory.CreateSetTask())
        };
        return new()
        {
            Branches = branches,
            Compete = true
        };
    }
}
