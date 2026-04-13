namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of the context of a workflow's execution
/// </summary>
public interface IWorkflowExecutionContext
{

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> of the current workflow
    /// </summary>
    WorkflowDefinition Definition { get; }

    /// <summary>
    /// Gets the current <see cref="IWorkflowState"/>
    /// </summary>
    IWorkflowState State { get; }

    /// <summary>
    /// Gets the current <see cref="IRuntimeExpressionEvaluator"/>
    /// </summary>
    IRuntimeExpressionEvaluator Expressions { get; }

    /// <summary>
    /// Gets the service used to run the workflow
    /// </summary>
    IWorkflowRuntime Runtime { get; }

    /// <summary>
    /// Gets the options used to configure the workflow's execution
    /// </summary>
    WorkflowExecutionsOptions Options { get; }

    /// <summary>
    /// Continues execution with the provided <see cref="TaskDefinition"/>
    /// </summary>
    /// <param name="task">The <see cref="TaskDefinition"/> to continue with</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ContinueWithAsync(TaskDefinition task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new <see cref="ITaskState"/>
    /// </summary>
    /// <param name="definition">The <see cref="TaskDefinition"/> of the <see cref="ITaskState"/> to create</param>
    /// <param name="path">The path used to reference the <see cref="TaskDefinition"/> of the <see cref="ITaskState"/> to create</param>
    /// <param name="input">The input data, if any</param>
    /// <param name="parent">The parent of the <see cref="ITaskState"/> to create, if any</param>
    /// <param name="isExtension">Indicates whether or not the task is part of an extension</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="ITaskState"/></returns>
    Task<ITaskState> CreateTaskAsync(TaskDefinition definition, JsonPointer path, JsonNode input, ITaskExecutionContext? parent = null, bool isExtension = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the workflow's tasks
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> to asynchronously enumerate the tasks the workflow owns</returns>
    IAsyncEnumerable<ITaskState> GetTasksAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ResumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Publishes the specified <see cref="ICloudEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="ICloudEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the error that has faulted the workflow's execution
    /// </summary>
    /// <param name="error">The <see cref="Error"/> that has faulted the workflow</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the workflow's result
    /// </summary>
    /// <param name="result">The workflow's result, if any</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetResultAsync(JsonNode? result, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the workflow's context data
    /// </summary>
    /// <param name="contextData">The workflow's context data</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetContextDataAsync(JsonObject contextData, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}