namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ProcessTypeDefinitionFactory
{
    internal static ProcessTypeDefinition CreateContainer() => new()
    {
        Container = ContainerProcessDefinitionFactory.Create(),
        Await = true,
        Return = ProcessReturnType.Stdout
    };

    internal static ProcessTypeDefinition CreateShell() => new()
    {
        Shell = ShellProcessDefinitionFactory.Create(),
        Await = true
    };

    internal static ProcessTypeDefinition CreateScript() => new()
    {
        Script = ScriptProcessDefinitionFactory.Create()
    };

    internal static ProcessTypeDefinition CreateWorkflow() => new()
    {
        Workflow = WorkflowProcessDefinitionFactory.Create()
    };
}
