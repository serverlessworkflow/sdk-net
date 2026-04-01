namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WorkflowDescriptorFactory
{
    internal static WorkflowDescriptor Create() => new()
    {
        Id = "test",
        Definition = WorkflowDefinitionFactory.Create(),
        Input = new()
        {
            ["key"] = "value"
        },
        StartedAt = DateTimeDescriptorFactory.Create()
    };
}