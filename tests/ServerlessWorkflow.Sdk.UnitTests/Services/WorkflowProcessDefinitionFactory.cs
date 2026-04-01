namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class WorkflowProcessDefinitionFactory
{
    internal static WorkflowProcessDefinition Create() => new()
    {
        Namespace = "com.example",
        Name = "my-subworkflow",
        Version = "1.0.0"
    };
}
