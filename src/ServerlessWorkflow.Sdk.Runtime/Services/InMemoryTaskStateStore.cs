using Microsoft.Extensions.Caching.Memory;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="ITaskStateStore"/> interface
/// </summary>
/// <param name="cache">The <see cref="IMemoryCache"/> instance used to store task states</param>
public sealed class InMemoryTaskStateStore(IMemoryCache cache)
    : ITaskStateStore
{

    /// <inheritdoc/>
    public Task<ITaskState> AddAsync(ITaskState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        return Task.FromResult(cache.Set(GetCacheKey(state.WorkflowId, state.Id), state));
    }

    /// <inheritdoc/>
    public Task<ITaskState> GetAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return cache.TryGetValue(GetCacheKey(workflowId, taskId), out ITaskState? state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Task with id '{taskId}' not found in workflow '{workflowId}'");
    }

    /// <inheritdoc/>
    public Task<ITaskState> UpdateAsync(ITaskState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        return Task.FromResult(cache.Set(GetCacheKey(state.WorkflowId, state.Id), state));
    }

    static string GetCacheKey(string workflowId, string taskId) => $"{workflowId}:{taskId}";

}
