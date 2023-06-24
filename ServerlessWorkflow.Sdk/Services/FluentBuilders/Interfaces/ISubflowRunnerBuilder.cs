namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build workflow runners
/// </summary>
public interface ISubflowRunnerBuilder
{

    /// <summary>
    /// Runs the specified workflow
    /// </summary>
    /// <param name="workflowId">The workflow to run</param>
    void RunSubflow(string workflowId);

}
