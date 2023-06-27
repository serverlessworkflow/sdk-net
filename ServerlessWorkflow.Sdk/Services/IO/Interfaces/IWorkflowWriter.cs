namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Defines the fundamentals of a service used to write <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowWriter
{

    /// <summary>
    /// Writes the specified <see cref="WorkflowDefinition"/> to a <see cref="Stream"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to write</param>
    /// <param name="stream">The <see cref="Stream"/> to read the <see cref="WorkflowDefinition"/> from</param>
    /// <param name="format">The format of the <see cref="WorkflowDefinition"/> to read. Defaults to '<see cref="WorkflowDefinitionFormat.Yaml"/>'</param>
    /// <returns>A new <see cref="WorkflowDefinition"/></returns>
    void Write(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml);

}
