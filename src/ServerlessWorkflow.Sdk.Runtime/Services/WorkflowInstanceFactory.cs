namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowInstanceFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="stateStore">The service used to manage workflow states</param>
/// <typeparam name="TState">The type of the workflow's state</typeparam>
public sealed class WorkflowInstanceFactory<TState>(IServiceProvider serviceProvider, IWorkflowStateStore<TState> stateStore)
    : IWorkflowInstanceFactory
    where TState : class, IWorkflowState, new()
{

    /// <inheritdoc/>
    public async Task<IWorkflowInstance> CreateAsync(WorkflowDefinition definition, JsonObject input, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(input);
        var state = await stateStore.AddAsync(new()
        {
            Definition = definition.GetReference(),
            Input = input
        }, cancellationToken).ConfigureAwait(false);
        return ActivatorUtilities.CreateInstance<WorkflowInstance<TState>>(serviceProvider, definition, state);
    }

}
