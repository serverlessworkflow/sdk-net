namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a task instance
/// </summary>
public interface ITaskInstance
{

    /// <summary>
    /// Gets the definition of the task instance
    /// </summary>
    TaskDefinition Definition { get; }

    /// <summary>
    /// Gets the current state of the task instance
    /// </summary>
    ITaskState State { get; }

    /// <summary>
    /// Gets the subtasks the task is made out of
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate subtasks</returns>
    IAsyncEnumerable<ITaskInstance> GetSubTasksAsync(CancellationToken cancellationToken = default);

}

/// <summary>
/// Defines the fundamentals of a task instance with a strongly-typed state
/// </summary>
public interface ITaskInstance<TDefinition>
    : ITaskInstance
     where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets the definition of the task instance
    /// </summary>
    new TDefinition Definition { get; }

}