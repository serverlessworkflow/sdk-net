namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="RunTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="containerRuntime">The service used to manage <see cref="IContainer"/>s</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class RunTaskExecutor(IServiceProvider serviceProvider, ILogger<RunTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IContainerRuntime containerRuntime, ITaskExecutionContext<RunTaskDefinition> task)
    : TaskExecutor<RunTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    IContainer? container;

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        if (Task.Definition.Run.Container != null) await ExecuteContainerProcessAsync(cancellationToken).ConfigureAwait(false);
        else if (Task.Definition.Run.Shell != null) await ExecuteShellProcessAsync(cancellationToken).ConfigureAwait(false);
        else throw new NotSupportedException("The specified process type is not supported");
    }

    async Task ExecuteContainerProcessAsync(CancellationToken cancellationToken)
    {
        var processDefinition = Task.Definition.Run.Container!;
        container = await containerRuntime.CreateAsync(processDefinition, cancellationToken).ConfigureAwait(false);
        try
        {
            await container.StartAsync(cancellationToken).ConfigureAwait(false);
            if (Task.Definition.Run.Await == false)
            {
                await SetResultAsync(new JsonObject(), Task.Definition.Then, cancellationToken).ConfigureAwait(false);
                return;
            }
            await container.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
            var standardOutput = container.StandardOutput == null ? null : (await container.StandardOutput.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
            var result = new JsonObject { ["output"] = standardOutput };
            await SetResultAsync(result, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while executing the container process: {ex}", ex);
            var message = ex.Message;
            try { if (container.StandardError != null) message = await container.StandardError.ReadToEndAsync(cancellationToken).ConfigureAwait(false); } catch { }
            await SetErrorAsync(RuntimeError.Runtime(new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute), message), cancellationToken).ConfigureAwait(false);
        }
    }

    async Task ExecuteShellProcessAsync(CancellationToken cancellationToken)
    {
        var processDefinition = Task.Definition.Run.Shell!;
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
        var process = Process.Start(startInfo) ?? throw new NullReferenceException($"Failed to create the shell process defined at '{Task.Instance.State.Reference}'");
        try
        {
            if (Task.Definition.Run.Await == false)
            {
                await SetResultAsync([], Task.Definition.Then, cancellationToken).ConfigureAwait(false);
                return;
            }
            await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
            var rawOutput = (await process.StandardOutput.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
            var errorMessage = (await process.StandardError.ReadToEndAsync(cancellationToken).ConfigureAwait(false)).Trim();
            if (process.ExitCode == 0) await SetResultAsync(new JsonObject { ["output"] = rawOutput }, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            else await SetErrorAsync(RuntimeError.Runtime(new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute), errorMessage), cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            process.Dispose();
        }
    }

    /// <inheritdoc/>
    protected override Task SuspendCoreAsync(CancellationToken cancellationToken) => container?.StopAsync(cancellationToken) ?? System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    protected override Task DoCancelAsync(CancellationToken cancellationToken) => container?.StopAsync(cancellationToken) ?? System.Threading.Tasks.Task.CompletedTask;

}
