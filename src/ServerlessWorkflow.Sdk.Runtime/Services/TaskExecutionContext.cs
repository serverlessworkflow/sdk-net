namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutionContext"/> interface
/// </summary>
/// <typeparam name="TDefinition">The type of the <see cref="TaskDefinition"/> to execute</typeparam>
/// <param name="logger">The service used to perform logging</param>
/// <param name="workflow">The workflow the task to execute belongs to</param>
/// <param name="eventBus">The service used to publish and subscribe to <see cref="ICloudEvent"/>s</param>
/// <param name="tasks">The service used to manage <see cref="ITaskState"/>s</param>
/// <param name="definition">The definition of the task to execute</param>
/// <param name="state">The initial state of the task to execute</param>
/// <param name="arguments">A name/value mapping of the task's arguments, if any</param>
public sealed class TaskExecutionContext<TDefinition>(ILogger<TaskExecutionContext<TDefinition>> logger, IWorkflowExecutionContext workflow, ICloudEventBus eventBus, ITaskStateStore tasks, TDefinition definition, ITaskState state, JsonObject? arguments)
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
    public IAsyncEnumerable<ITaskState> GetSubTasksAsync(CancellationToken cancellationToken = default) => tasks.ListAsync(state.WorkflowId, state.Id, cancellationToken);

    /// <inheritdoc/>
    public Task<IObservable<IStreamedCloudEvent>> StreamAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException(); //todo: implement
    }

    /// <inheritdoc/>
    public Task<ICorrelationContext> CorrelateAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException(); //todo: implement
    }

    /// <inheritdoc/>
    public Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default) => eventBus.PublishAsync(e, cancellationToken);

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Starting task with id '{TaskId}'...", state.Id);
        await state.StartAsync(cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Started.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                StartedAt = state.StartedAt!.Value
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.TaskStartedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' started", state.Id);
    }

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Suspending task with id '{TaskId}'...", state.Id);
        await state.SuspendAsync(cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Suspended.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                SuspendedAt = state.Runs?.LastOrDefault()?.EndedAt ?? DateTimeOffset.Now,
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.TaskSuspendedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' suspended", state.Id);
    }

    /// <inheritdoc/>
    public async Task RetryAsync(Error cause, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Retrying task with id '{TaskId}'...", state.Id);
        await state.RetryAsync(cause, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Retrying.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                RetryingAt = state.Runs?.LastOrDefault()?.StartedAt ?? DateTimeOffset.Now
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.RetryingTaskEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' retried", state.Id);
    }

    /// <inheritdoc/>
    public async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Faulting task with id '{TaskId}'...", state.Id);
        await state.SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Faulted.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                Error = error,
                FaultedAt = state.EndedAt!.Value
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.TaskFaultedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' faulted", state.Id);
    }

    /// <inheritdoc/>
    public async Task SetResultAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        await state.SetOutputAsync(result, then ?? FlowDirective.Continue, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Completed.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                CompletedAt = state.EndedAt!.Value
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.TaskCompletedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' ran to completion", state.Id);
    }

    /// <inheritdoc/>
    public Task SetContextDataAsync(JsonObject contextData, CancellationToken cancellationToken = default) => workflow.SetContextDataAsync(contextData, cancellationToken);

    /// <inheritdoc/>
    public async Task SkipAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Skipping the execution of the task with id '{TaskId}'...", state.Id);
        await state.SkipAsync(result, then ?? FlowDirective.Continue, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Skipped.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                CompletedAt = state.EndedAt!.Value
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.TaskCompletedEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the task with id '{TaskId}' has been skipped", state.Id);
    }

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Faulting task with id '{TaskId}'...", state.Id);
        await state.CancelAsync(cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Cancelled.v1,
            Subject = State.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = JsonSerializer.SerializeToNode(new()
            {
                Workflow = workflow.State.GetQualifiedName(),
                Task = state.Reference,
                CancelledAt = state.EndedAt!.Value
            }, Sdk.Serialization.Json.JsonSerializationContext.Default.TaskCancelledEvent)
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' faulted", state.Id);
    }

}