namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="ITaskStateStore"/> interface
/// </summary>
public sealed class InMemoryTaskStateStore
    : ITaskStateStore
{

    readonly ConcurrentDictionary<string, ITaskState> tasks = [];

    /// <inheritdoc/>
    public Task<ITaskState> AddAsync(ITaskState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        tasks[GetCacheKey(state.WorkflowId, state.Id)] = state;
        return Task.FromResult(state);
    }

    /// <inheritdoc/>
    public Task<ITaskState> GetAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return tasks.TryGetValue(GetCacheKey(workflowId, taskId), out var state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Task with id '{taskId}' not found in workflow '{workflowId}'");
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskState> ListAsync(string workflowId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        return tasks.Values.Where(t => t.WorkflowId == workflowId).ToAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskState> ListAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return tasks.Values.Where(t => t.WorkflowId == workflowId && t.ParentId == taskId).ToAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<ITaskState> UpdateAsync(ITaskState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        tasks[GetCacheKey(state.WorkflowId, state.Id)] = state;
        return Task.FromResult(state);
    }

    static string GetCacheKey(string workflowId, string taskId) => $"{workflowId}:{taskId}";

}
