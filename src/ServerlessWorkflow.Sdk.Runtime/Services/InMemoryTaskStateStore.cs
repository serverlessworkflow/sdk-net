namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="ITaskStore"/> interface
/// </summary>
public sealed class InMemoryTaskStateStore
    : ITaskStore
{

    readonly ConcurrentDictionary<string, ITaskInstance> tasks = [];

    /// <inheritdoc/>
    public Task<ITaskInstance> AddAsync(ITaskInstance instance, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(instance);
        tasks[GetCacheKey(instance.WorkflowId, instance.Id)] = instance;
        return Task.FromResult(instance);
    }

    /// <inheritdoc/>
    public Task<ITaskInstance> GetAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return tasks.TryGetValue(GetCacheKey(workflowId, taskId), out var state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Task with id '{taskId}' not found in workflow '{workflowId}'");
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskInstance> ListAsync(string workflowId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        return tasks.Values.Where(t => t.WorkflowId == workflowId).ToAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskInstance> ListAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return tasks.Values.Where(t => t.WorkflowId == workflowId && t.ParentId == taskId).ToAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<ITaskInstance> UpdateAsync(ITaskInstance instance, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(instance);
        tasks[GetCacheKey(instance.WorkflowId, instance.Id)] = instance;
        return Task.FromResult(instance);
    }

    static string GetCacheKey(string workflowId, string taskId) => $"{workflowId}:{taskId}";

}
