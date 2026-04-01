namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutor"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/>.</param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provider <see cref="ISchemaHandler"/>s</param>
/// <param name="task">The <see cref="ITaskExecutionContext"/> in which to run the <see cref="ITaskExecutor"/></param>
public abstract class TaskExecutor<TDefinition>(IServiceProvider serviceProvider, ILogger logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<TDefinition> task)
    : ITaskExecutor<TDefinition>
     where TDefinition : TaskDefinition
{

    bool disposed;

    /// <summary>
    /// Gets the current <see cref="IServiceProvider"/>
    /// </summary>
    protected IServiceProvider ServiceProvider { get; } = serviceProvider;

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Gets the service used to create <see cref="ITaskExecutionContext"/>s
    /// </summary>
    protected ITaskExecutionContextFactory ExecutionContextFactory { get; } = executionContextFactory;

    /// <summary>
    /// Gets the service used to create <see cref="ITaskExecutor"/>s
    /// </summary>
    protected ITaskExecutorFactory ExecutorFactory { get; } = executorFactory;

    /// <summary>
    /// Gets the service used to provide <see cref="ISchemaHandler"/>s
    /// </summary>
    protected ISchemaHandlerProvider SchemaHandlerProvider { get; } = schemaHandlerProvider;

    /// <inheritdoc/>
    public ITaskExecutionContext<TDefinition> Task { get; } = task;

    ITaskExecutionContext ITaskExecutor.Task => Task;

    /// <summary>
    /// Gets the <see cref="ISubject{T}"/> used to stream <see cref="ITaskLifeCycleEvent"/>s
    /// </summary>
    protected Subject<ITaskLifeCycleEvent> Subject { get; } = new();

    /// <summary>
    /// Gets a <see cref="ConcurrentHashSet{T}"/> containing all child <see cref="ITaskExecutor"/>s
    /// </summary>
    protected ConcurrentHashSet<ITaskExecutor> Executors { get; } = [];

    /// <summary>
    /// Gets the <see cref="ITaskExecutor"/>'s <see cref="System.Threading.Tasks.TaskCompletionSource"/>
    /// </summary>
    protected TaskCompletionSource TaskCompletionSource { get; } = new();

    /// <summary>
    /// Gets the <see cref="ITaskExecutor"/>'s <see cref="System.Threading.CancellationTokenSource"/>
    /// </summary>
    protected CancellationTokenSource? CancellationTokenSource { get; set; }

    /// <summary>
    /// Gets the object used to asynchronously lock the <see cref="TaskExecutor{TDefinition}"/>
    /// </summary>
    protected AsyncLock Lock { get; } = new();

    /// <summary>
    /// Gets the <see cref="TaskExecutor{TDefinition}"/>'s <see cref="System.Diagnostics.Stopwatch"/>, used to clock the <see cref="TaskInstance"/>'s execution
    /// </summary>
    protected Stopwatch Stopwatch { get; } = new();

    /// <summary>
    /// Gets a key/definition mapping of the extensions, if any, that apply to the task to run
    /// </summary>
    protected IEnumerable<KeyValuePair<string, ExtensionDefinition>>? Extensions => Task.Workflow.Definition.Use?.Extensions?.Where(ex => ex.Value.Extend == "all" || ex.Value.Extend == Task.Definition.Type);

    /// <inheritdoc/>
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (Task.Instance.State.Status != null && !Task.Instance.State.IsOperative) return;
        try
        {
            await InitializeCoreAsync(cancellationToken);
            await Task.Instance.InitializeAsync(cancellationToken);
            Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Initialized));
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError("An error occurred while initializing the task '{task}': {ex}", Task.Instance.State.Reference, ex);
            await ((ITaskExecutor)this).SetErrorAsync(new()
            {
                Type = ErrorType.Communication,
                Title = ErrorTitle.Communication,
                Status = ex.StatusCode.HasValue ? (ushort)ex.StatusCode : (ushort)ErrorStatus.Communication,
                Detail = ex.Message,
                Instance = new(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError("An error occurred while initializing the task '{task}': {ex}", Task.Instance.State.Reference, ex);
            await ((ITaskExecutor)this).SetErrorAsync(new()
            {
                Type = ErrorType.Runtime,
                Title = ErrorTitle.Runtime,
                Status = ErrorStatus.Runtime,
                Detail = ex.Message,
                Instance = new(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken);
        }
    }

    /// <summary>
    /// Initializes the <see cref="ITaskInstance"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task.Task"/></returns>
    protected virtual Task InitializeCoreAsync(CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (Task.Instance.State.Status != null && !Task.Instance.State.IsOperative) return;
        CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var expressionEvaluationArguments = GetExpressionEvaluationArguments();
        var timeout = await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.Timeout, Task.Input, expressionEvaluationArguments, cancellationToken);
        if (timeout is not null) CancellationTokenSource.CancelAfter(timeout.ToTimeSpan());
        try
        {
            if (!string.IsNullOrWhiteSpace(Task.Definition.If) && !(await Task.Workflow.Expressions.EvaluateConditionAsync(Task.Definition.If, Task.Input, expressionEvaluationArguments, cancellationToken)))
            {
                await SkipAsync(Task.Input, Task.Definition.Then, cancellationToken);
            }
            else
            {
                if (Task.Definition.Input?.Schema != null)
                {
                    var schemaHandler = SchemaHandlerProvider.GetHandler(Task.Definition.Input.Schema.Format) ?? throw new ArgumentNullException($"Failed to find an handler that supports the specified schema format '{Task.Definition.Input.Schema.Format}'");
                    var validationResult = await schemaHandler.ValidateAsync(Task.Input, Task.Definition.Input.Schema, cancellationToken);
                    if (!validationResult.IsSuccess())
                    {
                        await SetErrorAsync(new()
                        {
                            Type = ErrorType.Validation,
                            Status = ErrorStatus.Validation,
                            Title = ErrorTitle.Validation,
                            Instance = new($"{Task.Instance.State.Reference}/input", UriKind.RelativeOrAbsolute),
                            Detail = $"Failed to validate the task's input:\n{string.Join('\n', validationResult.Errors?.FirstOrDefault()?.Errors?.Select(e => $"- {e.Key}: {e.Value.First()}") ?? [])}"
                        }, cancellationToken);
                        return;
                    }
                }
                await Task.Instance.StartAsync(CancellationTokenSource.Token);
                Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Running));
                Stopwatch.Start();
                await BeforeExecuteAsync(cancellationToken); //todo: act upon last directive
                await ExecuteCoreAsync(CancellationTokenSource.Token);
            }
            await TaskCompletionSource.Task;
        }
        catch (OperationCanceledException) when (timeout is not null && !cancellationToken.IsCancellationRequested)
        {
            Logger.LogError("The task '{task}' timed out after {timeout} milliseconds", Task.Instance.State.Reference, timeout.TotalMilliseconds);
            await SetErrorAsync(new Error()
            {
                Status = (int)HttpStatusCode.RequestTimeout,
                Type = ErrorType.Timeout,
                Title = ErrorTitle.Timeout,
                Detail = $"The task '{Task.Instance.State.Reference}' timed out after {timeout.TotalMilliseconds } milliseconds"
            }, default);
        }
        catch (OperationCanceledException) { }
        catch (HttpRequestException ex)
        {
            Logger.LogError("An error occurred while executing the task '{task}': {ex}", Task.Instance.State.Reference, ex);
            await SetErrorAsync(new()
            {
                Type = ErrorType.Communication,
                Title = ErrorTitle.Communication,
                Status = ex.StatusCode.HasValue ? (ushort)ex.StatusCode : (ushort)ErrorStatus.Communication,
                Detail = ex.Message,
                Instance = new(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError("An error occurred while executing the task '{task}': {ex}", Task.Instance.State.Reference, ex);
            await SetErrorAsync(new()
            {
                Type = ErrorType.Runtime,
                Title = ErrorTitle.Runtime,
                Status = ErrorStatus.Runtime,
                Detail = ex.Message,
                Instance = new(Task.Instance.State.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken);
        }
    }

    /// <summary>
    /// Executes code before the task executes, typically extensions, if any
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual async Task BeforeExecuteAsync(CancellationToken cancellationToken)
    {
        if (Task.Instance.State.IsExtension || Extensions is null) return;
        var input = Task.Input;
        foreach (var extension in Extensions.Where(ex => ex.Value.Before != null).Reverse())
        {
            var taskDefinition = new DoTaskDefinition()
            {
                Do = extension.Value.Before!
            };
            var task = await Task.Workflow.Instance.CreateTaskAsync(taskDefinition, $"before/{extension.Key}", input, null, Task.Instance, true, cancellationToken);
            var executor = await CreateTaskExecutorAsync(task, taskDefinition, Task.ContextData, Task.Arguments, cancellationToken);
            await executor.ExecuteAsync(cancellationToken);
            if (executor.Task.Instance.State.Next == FlowDirective.Exit)
            {
                await SetResultAsync(executor.Task.Output, executor.Task.Definition.Then, cancellationToken);
                return;
            }
            input = executor.Task.Output ?? [];
            Executors.Remove(executor);
        }
    }

    /// <summary>
    /// Executes the <see cref="ITaskInstance"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task.Task"/></returns>
    protected virtual Task ExecuteCoreAsync(CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task RetryAsync(Error cause, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task SetResultAsync(object? result = null, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task CancelAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task SkipAsync(object? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        if (Task.Instance.State.Status != null) return;
        Stopwatch.Stop();
        if (string.IsNullOrWhiteSpace(then)) then = FlowDirective.Continue;
        var output = result;
        await Task.Instance.SkipAsync(output, then, cancellationToken);
        Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Skipped));
        Subject.OnCompleted();
        if (!TaskCompletionSource.Task.IsCompleted) TaskCompletionSource.SetResult();
    }

    /// <inheritdoc/>
    public virtual IDisposable Subscribe(IObserver<ITaskLifeCycleEvent> observer) => Subject.Subscribe(observer);

    /// <summary>
    /// Gets a new <see cref="JsonObject"/>, if any, containing the runtime expression evaluation arguments for the <see cref="ITaskInstance"/> to run
    /// </summary>
    /// <returns>A new <see cref="JsonObject"/>, if any, containing the runtime expression evaluation arguments for the <see cref="ITaskInstance"/> to run</returns>
    protected virtual JsonObject? GetExpressionEvaluationArguments()
    {
        var parameters = Task.Arguments.DeepClone().AsObject()!;
        parameters[RuntimeExpressions.Arguments.Runtime] = JsonSerializer.SerializeToNode(Task.Workflow.Runtime.Descriptor, Sdk.Serialization.Json.JsonSerializationContext.Default.RuntimeDescriptor);
        parameters[RuntimeExpressions.Arguments.Context] = Task.ContextData;
        parameters[RuntimeExpressions.Arguments.Workflow] = JsonSerializer.SerializeToNode(Task.Workflow.GetDescriptor(), Sdk.Serialization.Json.JsonSerializationContext.Default.WorkflowDescriptor);
        parameters[RuntimeExpressions.Arguments.Task] = JsonSerializer.SerializeToNode(Task.GetDescriptor(), Sdk.Serialization.Json.JsonSerializationContext.Default.TaskDescriptor);
        parameters[RuntimeExpressions.Arguments.Input] = Task.Input;
        return parameters;
    }

    /// <summary>
    /// Creates a new <see cref="ITaskExecutor"/> for the specified <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="instance">The <see cref="ITaskInstance"/> to create a new <see cref="ITaskExecutor"/> for</param>
    /// <param name="definition">The <see cref="TaskDefinition"/> of the <see cref="ITaskInstance"/> to execute</param>
    /// <param name="contextData">The current context data</param>
    /// <param name="arguments">A name/value mapping of the task's arguments, if any</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="ITaskExecutor"/></returns>
    protected virtual async Task<ITaskExecutor> CreateTaskExecutorAsync(ITaskInstance instance, TaskDefinition definition, JsonObject contextData, JsonObject? arguments = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(instance);
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(contextData);
        var context = ExecutionContextFactory.Create(Task.Workflow, instance, definition, contextData, arguments);
        var executor = ExecutorFactory.Create(ServiceProvider, context);
        await executor.InitializeAsync(cancellationToken);
        Executors.Add(executor);
        return executor;
    }

    /// <summary>
    /// Disposes of the <see cref="TaskExecutor{TDefinition}"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="TaskExecutor{TDefinition}"/> is being disposed of</param>
    /// <returns>A new awaitable <see cref="ValueTask"/></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposed) return;
        foreach (var executor in Executors)
        {
            try { await executor.DisposeAsync(); }
            catch { }
        }
        Subject.Dispose();
        Executors.Clear();
        CancellationTokenSource?.Dispose();
        disposed = true;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of the <see cref="TaskExecutor{TDefinition}"/>
    /// </summary>
    /// <param name="disposing">A boolean indicating whether or not the <see cref="TaskExecutor{TDefinition}"/> is being disposed of</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposed) return;
        foreach (var executor in Executors)
        {
            try { executor.Dispose(); }
            catch { }
        }
        Subject.Dispose();
        Executors.Clear();
        CancellationTokenSource?.Dispose();
        disposed = true;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

}
