namespace ServerlessWorkflow.Sdk.Runtime.Cli.Commands;

internal sealed class RunWorkflowCommand(IWorkflowRuntime workflowRuntime)
    : AsyncCommand<RunWorkflowCommand.Settings>
{

    protected override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        var file = new FileInfo(settings.WorkflowFile);
        if (!file.Exists)         
        {
            AnsiConsole.WriteException(new FileNotFoundException($"Failed to find the specified file '{settings.WorkflowFile}'", settings.WorkflowFile));
            return -1;
        }
        var text = await File.ReadAllTextAsync(file.FullName, cancellationToken).ConfigureAwait(false);
        var definition = file.Extension.ToLowerInvariant() switch
        {
            ".json" => JsonSerializer.Deserialize(text, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowDefinition),
            ".yaml" or ".yml" => YamlSerializer.Deserialize<WorkflowDefinition>(text, Sdk.Serialization.Json.JsonSerializationContext.Default.Options),
            _ => throw new NotSupportedException($"The specified file extension '{file.Extension}' is not supported")
        } ?? throw new NullReferenceException($"Failed to deserialize the workflow definition from the specified file '{file.FullName}'");
        JsonObject? input = null;
        if (string.IsNullOrWhiteSpace(settings.InputFile))
        {
            if (!file.Exists)
            {
                AnsiConsole.WriteException(new FileNotFoundException($"Failed to find the specified file '{settings.WorkflowFile}'", settings.WorkflowFile));
                return -1;
            }
            text = await File.ReadAllTextAsync(file.FullName, cancellationToken).ConfigureAwait(false);
            input = file.Extension.ToLowerInvariant() switch
            {
                ".json" => JsonSerializer.Deserialize(text, Sdk.Serialization.Json.JsonSerializationContext.Default.JsonObject),
                ".yaml" or ".yml" => YamlSerializer.Deserialize<JsonObject>(text, Sdk.Serialization.Json.JsonSerializationContext.Default.Options),
                _ => throw new NotSupportedException($"The specified file extension '{file.Extension}' is not supported")
            } ?? throw new NullReferenceException($"Failed to deserialize the workflow definition from the specified file '{file.FullName}'");
        }
        var process = await workflowRuntime.RunAsync(definition, input, new(), cancellationToken);
        await process.WaitAsync(cancellationToken).ConfigureAwait(false);
        //todo: handle output
        return 1;
    }

    internal sealed class Settings
        : CommandSettings
    {

        [CommandOption("-f|--file")]
        [Description("Path to the workflow definition file (.json, .yaml, .yml)")]
        public required string WorkflowFile { get; init; }

        [CommandOption("-i|--input")]
        [Description("Input data file")]
        public string? InputFile { get; init; }

        [CommandOption("-o|--output")]
        [Description("Write output to file (default: stdout)")]
        public string? Output { get; init; }

    }

}
