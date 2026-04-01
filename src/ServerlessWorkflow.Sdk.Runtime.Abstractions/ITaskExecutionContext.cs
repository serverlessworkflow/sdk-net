namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of the context of a task's execution
/// </summary>
public interface ITaskExecutionContext
{

    /// <summary>
    /// Gets the workflow the task to execute belongs to
    /// </summary>
    IWorkflowExecutionContext Workflow { get; }

    /// <summary>
    /// Gets the <see cref="TaskDefinition"/> of the task to execute
    /// </summary>
    TaskDefinition Definition { get; }

    /// <summary>
    /// Gets the task instance being executed
    /// </summary>
    ITaskInstance Instance { get; }

    /// <summary>
    /// Gets/sets the task's input data
    /// </summary>
    JsonObject Input { get; }

    /// <summary>
    /// Gets/sets the task's context data, if any
    /// </summary>
    JsonObject ContextData { get; }

    /// <summary>
    /// Gets/sets the task's arguments
    /// </summary>
    JsonObject Arguments { get; }

    /// <summary>
    /// Gets/sets the task's output data, if any, in case the task ran to completion
    /// </summary>
    JsonObject? Output { get; }

}

/// <summary>
/// Defines the fundamentals of the context of a task's execution
/// </summary>
/// <typeparam name="TDefinition">The type of task to run</typeparam>
public interface ITaskExecutionContext<TDefinition>
    : ITaskExecutionContext
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets the <see cref="TaskDefinition"/> of the task to execute
    /// </summary>
    new TDefinition Definition { get; }

}
