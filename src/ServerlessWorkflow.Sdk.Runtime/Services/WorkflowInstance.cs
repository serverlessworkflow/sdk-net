namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowInstance"/> interface
/// </summary>
/// <param name="definition">The workflow's definition</param>
/// <param name="state">The workflow's state</param>
/// <param name="stateStore">The service used to manage the workflow's state</param>
/// <typeparam name="TState">The type of the workflow's state</typeparam>
public sealed class WorkflowInstance<TState>(WorkflowDefinition definition, TState state, IWorkflowStateStore<TState> stateStore)
    : IWorkflowInstance<TState>
    where TState : class, IWorkflowState, new()
{

    /// <inheritdoc/>
    public WorkflowDefinition Definition => definition;

    /// <inheritdoc/>
    public TState State { get; private set; } = state;

    IWorkflowState IWorkflowInstance.State => State;

    /// <inheritdoc/>
    public Task<ITaskInstance> CreateTaskAsync(TaskDefinition definition, string? path, JsonNode input, JsonObject? context = null, ITaskProcess? parent = null, bool isExtension = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskInstance> GetTasksAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        State = await stateStore.UpdateAsync(State with
        {
            Status = WorkflowInstanceStatus.Running
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        State = await stateStore.UpdateAsync(State with
        {
            Status = WorkflowInstanceStatus.Suspended
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task ResumeAsync(CancellationToken cancellationToken = default)
    {
        State = await stateStore.UpdateAsync(State with
        {
            Status = WorkflowInstanceStatus.Running
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        State = await stateStore.UpdateAsync(State with
        {
            Status = WorkflowInstanceStatus.Faulted,
            Error = error
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task SetResultAsync(JsonNode? result, CancellationToken cancellationToken = default)
    {
        State = await stateStore.UpdateAsync(State with
        {
            Status = WorkflowInstanceStatus.Completed,
            Output = result
        }, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        State = await stateStore.UpdateAsync(State with
        {
            Status = WorkflowInstanceStatus.Cancelled
        }, cancellationToken);
    }

}