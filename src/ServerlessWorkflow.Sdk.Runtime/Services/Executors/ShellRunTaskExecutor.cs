namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute shell <see cref="RunTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="taskProcessFactory">The service used to create <see cref="ITaskProcess"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskProcess"/></param>
public sealed class ShellRunTaskExecutor(IServiceProvider serviceProvider, ILogger<ShellRunTaskExecutor> logger, ITaskProcessFactory taskProcessFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskProcess<RunTaskDefinition> task)
    : TaskExecutor<RunTaskDefinition>(serviceProvider, logger, taskProcessFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var processDefinition = Task.Instance.Definition.Run.Shell!;
        var fileInfo = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "/bin/bash" : "cmd.exe";
        var shellArgs = new List<string>();
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) shellArgs.Add("-c");
        else shellArgs.Add("/c");
        shellArgs.Add(processDefinition.Command);
        if (processDefinition.Arguments != null) shellArgs.AddRange(processDefinition.Arguments);
        var startInfo = new ProcessStartInfo(fileInfo, shellArgs)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };
        if (processDefinition.Environment != null) foreach (var kvp in processDefinition.Environment) startInfo.EnvironmentVariables[kvp.Key] = kvp.Value;
        var process = Process.Start(startInfo) ?? throw new NullReferenceException($"Failed to create the shell process defined at '{Task.Instance.State.Reference}'");
        try
        {
            if (Task.Instance.Definition.Run.Await == false)
            {
                await SetResultAsync(new JsonObject(), Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
                return;
            }
            await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
            var rawOutput = (await process.StandardOutput.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
            var errorMessage = (await process.StandardError.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
            if (process.ExitCode == 0) await SetResultAsync(new JsonObject { ["output"] = rawOutput }, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
            else await SetErrorAsync(Error.Runtime(new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute), errorMessage), cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            process.Dispose();
        }
    }

}
