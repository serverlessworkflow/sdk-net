namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="RaiseTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class RaiseTaskExecutor(IServiceProvider serviceProvider, ILogger<RaiseTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<RaiseTaskDefinition> task)
    : TaskExecutor<RaiseTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var errorDefinition = Task.Definition.Raise.Error.Match<ErrorDefinition?>(
            error => error,
            reference =>
            {
                if (Task.Workflow.Definition.Use?.Errors?.TryGetValue(reference, out var referencedError) == true && referencedError != null) return referencedError;
                throw new NullReferenceException($"Failed to find the referenced error definition '{reference}'");
            }
        ) ?? throw new NullReferenceException("The error to raise must be defined (or referenced)");
        var error = new RuntimeError()
        {
            Status = (ushort)errorDefinition.Status,
            Type = new(errorDefinition.Type, UriKind.RelativeOrAbsolute),
            Title = errorDefinition.Title,
            Detail = errorDefinition.Detail,
            Instance = new Uri(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
        };
        await SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
    }

}
