namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class RunTaskDefinitionFactory
{
    internal static RunTaskDefinition CreateContainer() => new()
    {
        Run = ProcessTypeDefinitionFactory.CreateContainer()
    };

    internal static RunTaskDefinition CreateShell() => new()
    {
        Run = ProcessTypeDefinitionFactory.CreateShell()
    };

    internal static RunTaskDefinition CreateScript() => new()
    {
        Run = ProcessTypeDefinitionFactory.CreateScript()
    };

    internal static RunTaskDefinition CreateWorkflow() => new()
    {
        Run = ProcessTypeDefinitionFactory.CreateWorkflow()
    };
}
