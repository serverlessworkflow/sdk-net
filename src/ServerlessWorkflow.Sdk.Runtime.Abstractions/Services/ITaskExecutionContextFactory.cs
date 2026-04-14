namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to create <see cref="ITaskExecutionContext"/>s
/// </summary>
public interface ITaskExecutionContextFactory
{

    /// <summary>
    /// Creates a new <see cref="ITaskExecutionContext"/> implementation for the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="workflow">The <see cref="IWorkflowExecutionContext"/> the <see cref="ITaskExecutionContext"/> belongs to</param>
    /// <param name="definition">The <see cref="TaskDefinition"/> of the <see cref="ITaskInstance"/> to run</param>
    /// <param name="instance">The <see cref="ITaskInstance"/> to run</param>
    /// <param name="arguments">A name/value mapping of the task's arguments, if any</param>
    /// <returns>A new <see cref="ITaskExecutionContext"/></returns>
    ITaskExecutionContext Create(IWorkflowExecutionContext workflow, TaskDefinition definition, ITaskInstance instance, JsonObject? arguments = null);

}
