namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WorkflowDefinitionMetadataFactory
{
    internal static WorkflowDefinitionMetadata Create() => new()
    {
        Dsl = "1.0.0",
        Namespace = "com.example",
        Name = "my-workflow",
        Version = "1.0.0",
        Title = "My Workflow",
        Summary = "A sample workflow definition",
        Tags = new EquatableDictionary<string, string>(
            new Dictionary<string, string>
            {
                ["environment"] = "production",
                ["team"] = "platform"
            })
    };
}
