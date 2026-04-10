namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutionContext"/> interface
/// </summary>
/// <typeparam name="TDefinition">The type of the <see cref="TaskDefinition"/> to execute</typeparam>
/// <param name="workflow">The workflow the task to execute belongs to</param>
/// <param name="definition">The definition of the task to execute</param>
/// <param name="state">The initial state of the task to execute</param>
/// <param name="arguments">A name/value mapping of the task's arguments, if any</param>
public sealed class TaskExecutionContext<TDefinition>(IWorkflowExecutionContext workflow, TDefinition definition, ITaskState state, JsonObject? arguments)
    : ITaskExecutionContext<TDefinition>
    where TDefinition : TaskDefinition
{

    readonly AsyncLock asyncLock = new();

    /// <inheritdoc/>
    public IWorkflowExecutionContext Workflow => workflow;

    /// <inheritdoc/>
    public TDefinition Definition => definition;

    TaskDefinition ITaskExecutionContext.Definition => Definition;

    /// <inheritdoc/>
    public ITaskState State => state;

    /// <inheritdoc/>
    public JsonObject? Arguments => arguments;

    /// <inheritdoc/>
    public Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskState> GetSubTasksAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<IObservable<IStreamedCloudEvent>> StreamAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<ICorrelationContext> CorrelateAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task RetryAsync(Error cause, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetContextDataAsync(JsonObject context, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetResultAsync(JsonNode? result, string? then = "continue", CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SkipAsync(JsonNode? result, string? then = "continue", CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task CancelAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}