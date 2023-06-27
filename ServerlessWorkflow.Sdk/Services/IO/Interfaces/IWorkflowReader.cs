namespace ServerlessWorkflow.Sdk.Services.IO;

/// <summary>
/// Defines the fundamentals of a service used to read <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowReader
{

    /// <summary>
    /// Reads a <see cref="WorkflowDefinition"/> from the specified <see cref="Stream"/>
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to read the <see cref="WorkflowDefinition"/> from</param>
    /// <param name="options">The <see cref="WorkflowReaderOptions"/> to use</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="WorkflowDefinition"/></returns>
    Task<WorkflowDefinition> ReadAsync(Stream stream, WorkflowReaderOptions? options = null, CancellationToken cancellationToken = default);

}
