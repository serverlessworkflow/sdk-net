namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowProcess"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="options">The <see cref="IWorkflowProcess"/> options</param>
/// <param name="runtime">The service used to run workflows</param>
/// <param name="instance">The <see cref="IWorkflowInstance"/> being executed</param>
/// <param name="expressions">The <see cref="IRuntimeExpressionEvaluator"/> used to evaluate expressions within the workflow</param>
/// <param name="eventBus">The service used to publish and subscribe to <see cref="CloudEvent"/>s</param>
public sealed class WorkflowProcess(ILogger<WorkflowProcess> logger, WorkflowProcessOptions options, IWorkflowRuntime runtime, IWorkflowInstance instance, IRuntimeExpressionEvaluator expressions, ICloudEventBus eventBus)
    : IWorkflowProcess
{

    readonly AsyncLock asyncLock = new();
    TaskCompletionSource taskCompletionSource = new();

    /// <inheritdoc/>
    public IWorkflowInstance Instance => instance;

    /// <inheritdoc/>
    public IRuntimeExpressionEvaluator Expressions => expressions;

    /// <inheritdoc/>
    public IWorkflowRuntime Runtime => runtime;

    /// <summary>
    /// Starts the workflow instance execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Starting workflow instance with id '{Instance}'...", instance.State.Id);
        await instance.StartAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Started.v1,
            Subject = Instance.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = Instance.GetQualifiedName(),
                Definition = Instance.Definition.GetQualifiedName(),
                StartedAt = Instance.State.StartedAt ?? DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowStartedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Workflow instance with id '{Instance}' started", instance.State.Id);
    }

    /// <inheritdoc/>
    public Task WaitAsync(CancellationToken cancellationToken = default) => taskCompletionSource.Task.WaitAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        if (Instance.State.Status == WorkflowInstanceStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Suspending the execution of the workflow instance with id '{Instance}'...", instance.State.Id);
        await instance.SuspendAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Suspended.v1,
            Subject = Instance.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = Instance.GetQualifiedName(),
                SuspendedAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowSuspendedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the workflow instance with id '{Instance}' has been suspended", instance.State.Id);
        taskCompletionSource.SetResult();
    }

    /// <inheritdoc/>
    public async Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        if (Instance.State.Status != WorkflowInstanceStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        taskCompletionSource = new();
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Resuming the execution of the workflow instance with id '{Instance}'...", instance.State.Id);
        await instance.ResumeAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Resumed.v1,
            Subject = Instance.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = Instance.GetQualifiedName(),
                ResumedAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowResumedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the workflow instance with id '{Instance}' has been resumed", instance.State.Id);
    }

    /// <summary>
    /// Sets the error that has faulted the workflow's execution
    /// </summary>
    /// <param name="error">The error that has faulted the workflow</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(error);
        if (Instance.State.Status != WorkflowInstanceStatus.Running && Instance.State.Status != WorkflowInstanceStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        await Instance.SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Faulted.v1,
            Subject = Instance.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = Instance.GetQualifiedName(),
                Error = error,
                FaultedAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowFaultedEvent)
        }, cancellationToken).ConfigureAwait(false);
        taskCompletionSource.SetException(new RuntimeErrorException(error));
    }

    /// <summary>
    /// Sets the workflow's result
    /// </summary>
    /// <param name="result">The workflow's result, if any</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    public async Task SetResultAsync(JsonNode? result, CancellationToken cancellationToken = default)
    {
        if (Instance.State.Status != WorkflowInstanceStatus.Running && Instance.State.Status != WorkflowInstanceStatus.Suspended) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        await Instance.SetResultAsync(result, cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Completed.v1,
            Subject = Instance.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = Instance.GetQualifiedName(),
                CompletedAt = DateTimeOffset.Now,
                Output = result
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowCompletedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The workflow instance with id '{Instance}' ran to completion", instance.State.Id);
        taskCompletionSource.SetResult();
    }

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        if (Instance.State.Status == WorkflowInstanceStatus.Cancelled) return;
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Cancelling the execution of the workflow instance with id '{Instance}'...", instance.State.Id);
        await Instance.CancelAsync(cancellationToken).ConfigureAwait(false);
        if (options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Workflow.Cancelled.v1,
            Subject = Instance.GetQualifiedName(),
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Name = Instance.GetQualifiedName(),
                CancelledAt = DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowCancelledEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the workflow instance with id '{Instance}' has been cancelled", instance.State.Id);
        taskCompletionSource.SetCanceled(cancellationToken);
    }

}
