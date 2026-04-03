using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ScriptProcessDefinitionFactory
{
    internal static ScriptProcessDefinition Create() => new()
    {
        Language = "javascript",
        Code = "console.log('Hello, World!')"
    };
}
