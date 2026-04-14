using ServerlessWorkflow.Sdk.Events.Tasks;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutionContext"/> interface
/// </summary>
/// <typeparam name="TDefinition">The type of the <see cref="TaskDefinition"/> to execute</typeparam>
/// <param name="logger">The service used to perform logging</param>
/// <param name="workflow">The workflow the task to execute belongs to</param>
/// <param name="eventBus">The service used to publish and subscribe to <see cref="ICloudEvent"/>s</param>
/// <param name="tasks">The service used to manage <see cref="ITaskInstance"/>s</param>
/// <param name="definition">The definition of the task to execute</param>
/// <param name="instance">The initial state of the task to execute</param>
/// <param name="arguments">A name/value mapping of the task's arguments, if any</param>
public sealed class TaskExecutionContext<TDefinition>(ILogger<TaskExecutionContext<TDefinition>> logger, IWorkflowExecutionContext workflow, ICloudEventBus eventBus, ITaskStore tasks, TDefinition definition, ITaskInstance instance, JsonObject? arguments)
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
    public ITaskInstance Instance => instance;

    /// <inheritdoc/>
    public JsonObject? Arguments => arguments;

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskInstance> GetSubTasksAsync(CancellationToken cancellationToken = default) => tasks.ListAsync(instance.WorkflowId, instance.Id, cancellationToken);

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
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Starting task with id '{TaskId}'...", instance.Id);
        await instance.StartAsync(cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Started.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new TaskStartedEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                StartedAt = instance.StartedAt!.Value
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' started", instance.Id);
    }

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Suspending task with id '{TaskId}'...", instance.Id);
        await instance.SuspendAsync(cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Suspended.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new TaskSuspendedEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                SuspendedAt = instance.Runs?.LastOrDefault()?.EndedAt ?? DateTimeOffset.Now,
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' suspended", instance.Id);
    }

    /// <inheritdoc/>
    public async Task RetryAsync(Error cause, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Retrying task with id '{TaskId}'...", instance.Id);
        await instance.RetryAsync(cause, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Retrying.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new RetryingTaskEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                RetryingAt = instance.Runs?.LastOrDefault()?.StartedAt ?? DateTimeOffset.Now
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' retried", instance.Id);
    }

    /// <inheritdoc/>
    public async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Faulting task with id '{TaskId}'...", instance.Id);
        await instance.SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Faulted.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new TaskFaultedEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                Error = error,
                FaultedAt = instance.EndedAt!.Value
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' faulted", instance.Id);
    }

    /// <inheritdoc/>
    public async Task SetResultAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        await instance.SetOutputAsync(result, then ?? FlowDirective.Continue, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Completed.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new TaskCompletedEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                CompletedAt = instance.EndedAt!.Value
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' ran to completion", instance.Id);
    }

    /// <inheritdoc/>
    public Task SetContextDataAsync(JsonObject contextData, CancellationToken cancellationToken = default) => workflow.SetContextDataAsync(contextData, cancellationToken);

    /// <inheritdoc/>
    public async Task SkipAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Skipping the execution of the task with id '{TaskId}'...", instance.Id);
        await instance.SkipAsync(result, then ?? FlowDirective.Continue, cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Skipped.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new TaskSkippedEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                SkippedAt = instance.EndedAt!.Value
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("The execution of the task with id '{TaskId}' has been skipped", instance.Id);
    }

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        using var @lock = await asyncLock.LockAsync(cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Faulting task with id '{TaskId}'...", instance.Id);
        await instance.CancelAsync(cancellationToken).ConfigureAwait(false);
        if (workflow.Options.LifecycleEvents.Publish) await eventBus.PublishAsync(new CloudEvent()
        {
            SpecVersion = CloudEvent.DefaultVersion,
            Id = Guid.NewGuid().ToString(),
            Time = DateTimeOffset.Now,
            Source = workflow.Options.LifecycleEvents.Source,
            Type = ServerlessWorkflowSpecificationDefaults.CloudEvents.Task.Cancelled.v1,
            Subject = Instance.Id,
            DataContentType = MediaTypeNames.Application.Json,
            Data = new TaskCancelledEvent()
            {
                Workflow = workflow.Instance.GetQualifiedName(),
                Task = instance.Reference,
                CancelledAt = instance.EndedAt!.Value
            }
        }, cancellationToken).ConfigureAwait(false);
        if (logger.IsEnabled(LogLevel.Information)) logger.LogInformation("Task with id '{TaskId}' faulted", instance.Id);
    }

}