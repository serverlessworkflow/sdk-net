namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to execute workflows
/// </summary>
public interface IWorkflowRuntime
{

    /// <summary>
    /// Gets an object used to describe the current runtime environment
    /// </summary>
    RuntimeDescriptor Descriptor { get; }

    /// <summary>
    /// Runs the specified workflow definition with the provided input
    /// </summary>
    /// <param name="workflowDefinition">The definition of the workflow to run</param>
    /// <param name="input">The input to run the workflow with</param>
    /// <param name="options">The options used to configure the workflow process</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowProcess"/></returns>
    Task<IWorkflowProcess> RunAsync(WorkflowDefinition workflowDefinition, JsonObject input, WorkflowProcessOptions? options = null, CancellationToken cancellationToken = default);

}