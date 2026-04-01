namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class TaskDescriptorFactory
{
    internal static TaskDescriptor Create() => new()
    {
        Name = "test",
        Definition = TaskDefinitionFactory.CreateSetTask(),
        Reference = JsonPointer.Parse("/test"),
        Input = new()
        {
            ["key"] = "value"
        },
        Output = new()
        {
            ["key"] = "value"
        },
        StartedAt = DateTimeDescriptorFactory.Create()
    };
}
