namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a service used to manage <see cref="ITaskState"/>s
/// </summary>
public interface ITaskStateStore
{

    /// <summary>
    /// Adds a the specified <see cref="ITaskState"/>
    /// </summary>
    /// <param name="state">The <see cref="ITaskState"/> to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The added <see cref="ITaskState"/></returns>
    Task<ITaskState> AddAsync(ITaskState state, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="ITaskState"/> with the specified unique identifier, belonging to the specified workflow
    /// </summary>
    /// <param name="workflowId">The unique identifier of the workflow the task to get the <see cref="ITaskState"/> of belongs to</param>
    /// <param name="taskId">The unique identifier of the task to get the <see cref="ITaskState"/> of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="ITaskState"/> with the specified unique identifier</returns>
    Task<ITaskState> GetAsync(string workflowId, string taskId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified <see cref="ITaskState"/>
    /// </summary>
    /// <param name="state">The <see cref="ITaskState"/> to update</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="ITaskState"/></returns>
    Task<ITaskState> UpdateAsync(ITaskState state, CancellationToken cancellationToken = default);

}
