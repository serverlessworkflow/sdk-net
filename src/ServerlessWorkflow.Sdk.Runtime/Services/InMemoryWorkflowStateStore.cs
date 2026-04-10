using Microsoft.Extensions.Caching.Memory;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="IWorkflowStateStore"/> interface
/// </summary>
/// <param name="cache">The <see cref="IMemoryCache"/> instance used to store workflow states</param>
public sealed class InMemoryWorkflowStateStore(IMemoryCache cache)
    : IWorkflowStateStore
{

    /// <inheritdoc/>
    public Task<IWorkflowState> AddAsync(WorkflowDefinition definition, JsonObject? input = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        var state = new WorkflowState()
        {
            Definition = definition.GetReference(),
            Input = input
        };
        cache.Set(state.Id, state);
        return Task.FromResult((IWorkflowState)state);
    }

    /// <inheritdoc/>
    public Task<IWorkflowState> GetAsync(string workflowId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        return cache.TryGetValue(workflowId, out IWorkflowState? state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Workflow with id '{workflowId}' not found");
    }

    /// <inheritdoc/>
    public Task<IWorkflowState> UpdateAsync(IWorkflowState state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        return Task.FromResult(cache.Set(state.Id, state));
    }

}
