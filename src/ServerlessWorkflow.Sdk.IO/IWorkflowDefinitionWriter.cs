namespace ServerlessWorkflow.Sdk.IO;

/// <summary>
/// Defines the fundamentals of a service used to write <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowDefinitionWriter
{

    /// <summary>
    /// Writes the specified <see cref="WorkflowDefinition"/> to a <see cref="Stream"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to write</param>
    /// <param name="stream">The <see cref="Stream"/> to read the <see cref="WorkflowDefinition"/> from</param>
    /// <param name="format">The format of the <see cref="WorkflowDefinition"/> to read. Defaults to '<see cref="WorkflowDefinitionFormat.Yaml"/>'</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task WriteAsync(WorkflowDefinition workflow, Stream stream, string format = WorkflowDefinitionFormat.Yaml, CancellationToken cancellationToken = default);

}