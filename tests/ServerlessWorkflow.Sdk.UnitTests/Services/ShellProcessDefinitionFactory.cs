using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class ShellProcessDefinitionFactory
{
    internal static ShellProcessDefinition Create() => new()
    {
        Command = "echo",
        Arguments = ["Hello", "World"],
        Environment = new EquatableDictionary<string, string>(
            new Dictionary<string, string>
            {
                ["PATH"] = "/usr/bin"
            })
    };
}
