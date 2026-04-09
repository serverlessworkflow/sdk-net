namespace ServerlessWorkflow.Sdk.Runtime.Cli.Commands;

internal sealed class RunWorkflowCommand
    : AsyncCommand<RunWorkflowCommand.Settings>
{

    protected override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        return 1;
    }

    internal sealed class Settings
        : CommandSettings
    {

        [CommandArgument(0, "<workflow>")]
        [Description("Path to the workflow definition file (.json, .yaml, .yml)")]
        public required string WorkflowFile { get; init; }

        [CommandOption("-i|--input")]
        [Description("Input data (file path or inline JSON)")]
        public string? Input { get; init; }

        [CommandOption("-o|--output")]
        [Description("Write output to file (default: stdout)")]
        public string? Output { get; init; }

    }

}
