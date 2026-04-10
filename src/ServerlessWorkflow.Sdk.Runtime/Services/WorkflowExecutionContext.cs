namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowExecutionContext"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The options used to configure workflow execution</param>
/// <param name="definition">The <see cref="WorkflowDefinition"/> of the workflow being executed</param>
/// <param name="state">The <see cref="IWorkflowState"/> of the workflow being executed</param>
/// <param name="runtimeExpressionEvaluator">The service used to evaluate runtime expressions</param>
/// <param name="runtime">The <see cref="IWorkflowRuntime"/> in which the workflow is being executed</param>
/// <param name="eventBus">The service used to publish and subscribe to <see cref="ICloudEvent"/>s</param>
public sealed class WorkflowExecutionContext(ILogger<WorkflowExecutionContext> logger, WorkflowExecutionsOptions options, WorkflowDefinition definition, IWorkflowState state, IRuntimeExpressionEvaluator runtimeExpressionEvaluator, IWorkflowRuntime runtime, ICloudEventBus eventBus)
    : IWorkflowExecutionContext
{

    readonly AsyncLock asyncLock = new();

    /// <inheritdoc/>
    public WorkflowDefinition Definition => definition;

    /// <inheritdoc/>
    public IWorkflowState State => state;

    /// <inheritdoc/>
    public IRuntimeExpressionEvaluator Expressions => runtimeExpressionEvaluator;

    /// <inheritdoc/>
    public IWorkflowRuntime Runtime => runtime;

    /// <inheritdoc/>
    public Task ContinueWithAsync(TaskDefinition task, CancellationToken cancellationToken = default) => Task.CompletedTask;

    /// <inheritdoc/>
    public async Task<ITaskState> CreateTaskAsync(TaskDefinition definition, JsonPointer path, JsonNode input, ITaskExecutionContext? parent = null, bool isExtension = false, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskState> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Starting workflow state with id '{state}'...", state.Id);
        await state.StartAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Started.v1,
            Subject = state.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = state.GetQualifiedName(),
                Definition = definition.GetQualifiedName(),
                StartedAt = state.StartedAt ?? DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowStartedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Workflow state with id '{state}' started", state.Id);
    }

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        if (state.Status == WorkflowStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Suspending the execution of the workflow state with id '{state}'...", state.Id);
        await state.SuspendAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Suspended.v1,
            Subject = state.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = state.GetQualifiedName(),
                SuspendedAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowSuspendedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the workflow state with id '{state}' has been suspended", state.Id);
    }

    /// <inheritdoc/>
    public async Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        if (state.Status != WorkflowStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Resuming the execution of the workflow state with id '{state}'...", state.Id);
        await state.ResumeAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Resumed.v1,
            Subject = state.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = state.GetQualifiedName(),
                ResumedAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowResumedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the workflow state with id '{state}' has been resumed", state.Id);

    }

    /// <inheritdoc/>
    public async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(error);
        if (state.Status != WorkflowStatus.Running && state.Status != WorkflowStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        await state.SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Faulted.v1,
            Subject = state.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = state.GetQualifiedName(),
                Error = error,
                FaultedAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowFaultedEvent)
        }, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task SetResultAsync(JsonNode? result, CancellationToken cancellationToken = default)
    {
        if (state.Status != WorkflowStatus.Running && state.Status != WorkflowStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        await state.SetOutputAsync(result, cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Completed.v1,
            Subject = state.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = state.GetQualifiedName(),
                CompletedAt = DateTimeOffset.Now,
                Output = result
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowCompletedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The workflow state with id '{state}' ran to completion", state.Id);
    }

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        if (state.Status == WorkflowStatus.Cancelled) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Cancelling the execution of the workflow state with id '{state}'...", state.Id);
        await state.CancelAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Cancelled.v1,
            Subject = state.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = state.GetQualifiedName(),
                CancelledAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowCancelledEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the workflow state with id '{state}' has been cancelled", state.Id);
    }

}
