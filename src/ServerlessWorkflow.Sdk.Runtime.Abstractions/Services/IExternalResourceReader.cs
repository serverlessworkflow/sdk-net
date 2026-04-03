namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to read external resources
/// </summary>
public interface IExternalResourceReader
{

    /// <summary>
    /// Reads the specified external resource
    /// </summary>
    /// <param name="resource">The reference to the external resource to get</param>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/>, if any, in the context of which to read the specified resource</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A <see cref="Stream"/> used to read the external resource's contents</returns>
    Task<Stream> ReadAsync(ExternalResourceDefinition resource, WorkflowDefinition? workflow = null, CancellationToken cancellationToken = default);

}