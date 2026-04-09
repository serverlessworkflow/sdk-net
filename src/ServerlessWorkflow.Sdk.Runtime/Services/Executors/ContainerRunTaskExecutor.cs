namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute container <see cref="RunTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="containerRuntime">The service used to manage <see cref="IContainer"/>s</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class ContainerRunTaskExecutor(IServiceProvider serviceProvider, ILogger<ContainerRunTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, 
    ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, IContainerRuntime containerRuntime, ITaskExecutionContext<RunTaskDefinition> task)
    : TaskExecutor<RunTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    IContainer? container;

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
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

    /// <inheritdoc/>
    protected override Task SuspendCoreAsync(CancellationToken cancellationToken) => container?.StopAsync(cancellationToken) ?? System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    protected override Task DoCancelAsync(CancellationToken cancellationToken) => container?.StopAsync(cancellationToken) ?? System.Threading.Tasks.Task.CompletedTask;

}
