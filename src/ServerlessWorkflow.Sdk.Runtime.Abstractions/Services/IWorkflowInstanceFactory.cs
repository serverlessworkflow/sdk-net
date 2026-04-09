namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="IWorkflowInstance"/>s
/// </summary>
public interface IWorkflowInstanceFactory
{

    /// <summary>
    /// Creates a new <see cref="IWorkflowInstance"/> based on the specified <see cref="WorkflowDefinition"/> and input
    /// </summary>
    /// <param name="definition">The <see cref="WorkflowDefinition"/> to create the <see cref="IWorkflowInstance"/> from</param>
    /// <param name="input">The input to initialize the <see cref="IWorkflowInstance"/> with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowInstance"/></returns>
    Task<IWorkflowInstance> CreateAsync(WorkflowDefinition definition, JsonObject input, CancellationToken cancellationToken = default);

}
