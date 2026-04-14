using Microsoft.Extensions.Caching.Memory;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="IWorkflowStore"/> interface
/// </summary>
/// <param name="cache">The <see cref="IMemoryCache"/> instance used to store workflow states</param>
public sealed class InMemoryWorkflowStore(IMemoryCache cache)
    : IWorkflowStore
{

    /// <inheritdoc/>
    public Task<IWorkflowInstance> AddAsync(WorkflowDefinition definition, JsonObject? input = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        var state = new WorkflowInstance()
        {
            Definition = definition.GetReference(),
            Input = input
        };
        cache.Set(state.Id, state);
        return Task.FromResult((IWorkflowInstance)state);
    }

    /// <inheritdoc/>
    public Task<IWorkflowInstance> GetAsync(string workflowId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        return cache.TryGetValue(workflowId, out IWorkflowInstance? state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Workflow with id '{workflowId}' not found");
    }

    /// <inheritdoc/>
    public Task<IWorkflowInstance> UpdateAsync(IWorkflowInstance state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        return Task.FromResult(cache.Set(state.Id, state));
    }

}
