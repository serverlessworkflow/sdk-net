namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Defines the fundamentals of a service used to resolve the external definitions referenced by a <see cref="WorkflowDefinition"/>
/// </summary>
public interface IWorkflowExternalDefinitionResolver
{

    /// <summary>
    /// Loads the external definitions referenced by the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to load the external references of</param>
    /// <param name="options">The options used to configure how to read external definitions</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The loaded <see cref="WorkflowDefinition"/></returns>
    Task<WorkflowDefinition> LoadExternalDefinitionsAsync(WorkflowDefinition workflow, WorkflowReaderOptions options, CancellationToken cancellationToken = default);

}
