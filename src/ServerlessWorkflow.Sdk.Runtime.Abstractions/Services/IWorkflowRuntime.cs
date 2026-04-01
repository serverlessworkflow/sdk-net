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

}