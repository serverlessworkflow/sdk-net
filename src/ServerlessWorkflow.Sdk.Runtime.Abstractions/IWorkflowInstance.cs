namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a workflow instance
/// </summary>
public interface IWorkflowInstance
{

    /// <summary>
    /// Gets the current state of the workflow instance
    /// </summary>
    IWorkflowState State { get; }

    /// <summary>
    /// Creates a new task instance
    /// </summary>
    /// <param name="definition">The definition of the task to create</param>
    /// <param name="path">The path used to reference the task's definition</param>
    /// <param name="input">The task's input data</param>
    /// <param name="context">The task's context data. If not set, the task inherits its parent's context data</param>
    /// <param name="parent">The parent task, if any</param>
    /// <param name="isExtension">Indicates whether or not the task is part of an extension</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The newly created <see cref="ITaskInstance{TState}"/></returns>
    Task<ITaskInstance<ITaskState>> CreateTaskAsync(TaskDefinition definition, string? path, JsonObject input, JsonObject? context = null, ITaskInstance? parent = null, bool isExtension = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the workflow's tasks
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> to asynchronously enumerate the workflow's tasks</returns>
    IAsyncEnumerable<ITaskInstance<ITaskState>> GetTasksAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the subtasks of the specified task
    /// </summary>
    /// <param name="task">The task to enumerate the subtasks of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> to asynchronously enumerate the task's subtasks</returns>
    IAsyncEnumerable<ITaskInstance<ITaskState>> GetTasksAsync(ITaskInstance<ITaskState> task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Continues execution with the specified task definition
    /// </summary>
    /// <param name="task">The task definition to continue with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ContinueWithAsync(TaskDefinition task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task InitializeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ResumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the error that has faulted the workflow's execution
    /// </summary>
    /// <param name="error">The error that has faulted the workflow</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the workflow's result
    /// </summary>
    /// <param name="result">The workflow's result, if any</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetResultAsync(object? result, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}

/// <summary>
/// Defines the fundamentals of a workflow instance with a strongly-typed state
/// </summary>
/// <typeparam name="TState">The type of the workflow's state</typeparam>
public interface IWorkflowInstance<TState>
    : IWorkflowInstance
     where TState : class, IWorkflowState
{

    /// <summary>
    /// Gets the current state of the workflow instance
    /// </summary>
    new TState State { get; }

}