namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a service used to manage <see cref="IWorkflowState"/>s
/// </summary>
public interface IWorkflowStateStore
{

    /// <summary>
    /// Adds a the specified <see cref="IWorkflowState"/>
    /// </summary>
    /// <param name="definition">The definition of the workflow to add</param>
    /// <param name="input">The input of the workflow to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The added <see cref="IWorkflowState"/></returns>
    Task<IWorkflowState> AddAsync(WorkflowDefinition definition, JsonObject? input = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="IWorkflowState"/> with the specified unique identifier, belonging to the specified workflow
    /// </summary>
    /// <param name="id">The unique identifier of the workflow to get the state of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="IWorkflowState"/> with the specified unique identifier</returns>
    Task<IWorkflowState> GetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified <see cref="IWorkflowState"/>
    /// </summary>
    /// <param name="state">The <see cref="IWorkflowState"/> to update</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="IWorkflowState"/></returns>
    Task<IWorkflowState> UpdateAsync(IWorkflowState state, CancellationToken cancellationToken = default);

}