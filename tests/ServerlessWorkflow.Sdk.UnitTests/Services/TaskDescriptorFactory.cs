namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class TaskDescriptorFactory
{
    internal static TaskDescriptor Create() => new()
    {
        Id = "test-task",
        Name = "test",
        Definition = TaskDefinitionFactory.CreateSetTask(),
        Reference = JsonPointer.Parse("/test"),
        Input = new JsonObject()
        {
            ["key"] = "value"
        },
        Output = new JsonObject()
        {
            ["key"] = "value"
        },
        StartedAt = DateTimeDescriptorFactory.Create()
    };
}
