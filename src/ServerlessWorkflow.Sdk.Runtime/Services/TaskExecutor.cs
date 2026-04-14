namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutor"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/>.</param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>es</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provider <see cref="ISchemaHandler"/>s</param>
/// <param name="task">The <see cref="ITaskExecutionContext"/> to run</param>
public abstract class TaskExecutor<TDefinition>(IServiceProvider serviceProvider, ILogger logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<TDefinition> task)
    : ITaskExecutor<TDefinition>
     where TDefinition : TaskDefinition
{

    readonly JsonNode descriptor = JsonSerializer.SerializeToNode(task.GetDescriptor(), Sdk.Serialization.Json.JsonSerializationContext.Default.TaskDescriptor)!;
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
    /// Gets the service used to create <see cref="ITaskExecutionContextFactory"/>s
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
    /// Gets the <see cref="TaskExecutor{TDefinition}"/>'s <see cref="System.Diagnostics.Stopwatch"/>, used to clock the <see cref="ITaskInstance"/>'s execution
    /// </summary>
    protected Stopwatch Stopwatch { get; } = new();

    /// <summary>
    /// Gets a key/definition mapping of the extensions, if any, that apply to the task to run
    /// </summary>
    protected IEnumerable<KeyValuePair<string, ExtensionDefinition>>? Extensions => Task.Workflow.Definition.Use?.Extensions?.Where(ex => ex.Value.Extend == "all" || ex.Value.Extend == Task.Definition.Type);

    /// <inheritdoc/>
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (Task.Instance.Status != null && !Task.Instance.IsOperative) return;
        try
        {
            await InitializeCoreAsync(cancellationToken).ConfigureAwait(false);
            Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Initialized));
        }
        catch (HttpRequestException ex)
        {
            Logger.LogError("An error occurred while initializing the task '{task}': {ex}", Task.Instance.Reference, ex);
            await ((ITaskExecutor)this).SetErrorAsync(new Error()
            {
                Type = ErrorType.Communication,
                Title = ErrorTitle.Communication,
                Status = ex.StatusCode.HasValue ? (ushort)ex.StatusCode : (ushort)ErrorStatus.Communication,
                Detail = ex.Message,
                Instance = new(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Logger.LogError("An error occurred while initializing the task '{task}': {ex}", Task.Instance.Reference, ex);
            await ((ITaskExecutor)this).SetErrorAsync(new Error()
            {
                Type = ErrorType.Runtime,
                Title = ErrorTitle.Runtime,
                Status = ErrorStatus.Runtime,
                Detail = ex.Message,
                Instance = new(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Initializes the <see cref="ITaskInstance"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task InitializeCoreAsync(CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        if (Task.Instance.Status != null && !Task.Instance.IsOperative) return;
        CancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        var expressionEvaluationArguments = GetExpressionEvaluationArguments();
        var timeout = await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.Timeout, Task.Instance.Input, expressionEvaluationArguments, cancellationToken).ConfigureAwait(false);
        if (timeout is not null) CancellationTokenSource.CancelAfter(timeout.ToTimeSpan());
        try
        {
            if (!string.IsNullOrWhiteSpace(Task.Definition.If) && !await Task.Workflow.Expressions.EvaluateConditionAsync(Task.Definition.If, Task.Instance.Input, expressionEvaluationArguments, cancellationToken).ConfigureAwait(false))
            {
                await SkipAsync(Task.Instance.Input, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                if (Task.Definition.Input?.Schema is not null)
                {
                    var schemaFormat = Task.Definition.Input.Schema!.Format ?? SchemaFormat.Json;
                    var schemaHandler = SchemaHandlerProvider.GetHandler(schemaFormat) ?? throw new ArgumentNullException($"Failed to find an handler that supports the specified schema format '{schemaFormat}'");
                    var validationResult = await schemaHandler.ValidateAsync(Task.Instance.Input, Task.Definition.Input.Schema!, cancellationToken).ConfigureAwait(false);
                    if (!validationResult.IsValid)
                    {
                        await SetErrorAsync(new Error()
                        {
                            Type = ErrorType.Validation,
                            Status = ErrorStatus.Validation,
                            Title = ErrorTitle.Validation,
                            Instance = new($"{Task.Instance.Reference}/input", UriKind.RelativeOrAbsolute),
                            Detail = $"Failed to validate the task's input:\n{string.Join('\n', validationResult.Errors?.Select(e => $"- {e.Key}:\n  • {string.Join("\n  • ", e.Value)}") ?? [])}"
                        }, cancellationToken).ConfigureAwait(false);
                        return;
                    }
                }
                await Task.StartAsync(CancellationTokenSource.Token).ConfigureAwait(false);
                Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Running));
                Stopwatch.Start();
                await BeforeExecuteAsync(cancellationToken).ConfigureAwait(false); //todo: act upon last directive
                await ExecuteCoreAsync(CancellationTokenSource.Token).ConfigureAwait(false);
            }
            await TaskCompletionSource.Task;
        }
        catch (OperationCanceledException) when (timeout is not null && !cancellationToken.IsCancellationRequested)
        {
            Logger.LogError("The task '{task}' timed out after {timeout} milliseconds", Task.Instance.Reference, timeout.TotalMilliseconds);
            await SetErrorAsync(new Error()
            {
                Status = (int)HttpStatusCode.RequestTimeout,
                Type = ErrorType.Timeout,
                Title = ErrorTitle.Timeout,
                Detail = $"The task '{Task.Instance.Reference}' timed out after {timeout.TotalMilliseconds } milliseconds"
            }, default).ConfigureAwait(false);
        }
        catch (OperationCanceledException) { }
        catch (HttpRequestException ex)
        {
            Logger.LogError("An error occurred while executing the task '{task}': {ex}", Task.Instance.Reference, ex);
            await SetErrorAsync(new Error()
            {
                Type = ErrorType.Communication,
                Title = ErrorTitle.Communication,
                Status = ex.StatusCode.HasValue ? (ushort)ex.StatusCode : (ushort)ErrorStatus.Communication,
                Detail = ex.Message,
                Instance = new(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Logger.LogError("An error occurred while executing the task '{task}': {ex}", Task.Instance.Reference, ex);
            await SetErrorAsync(new Error()
            {
                Type = ErrorType.Runtime,
                Title = ErrorTitle.Runtime,
                Status = ErrorStatus.Runtime,
                Detail = ex.Message,
                Instance = new(Task.Instance.Reference.ToString(), UriKind.RelativeOrAbsolute)
            }, cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Executes code before the task executes, typically extensions, if any
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual async Task BeforeExecuteAsync(CancellationToken cancellationToken)
    {
        if (Task.Instance.IsExtension || Extensions is null) return;
        var input = Task.Instance.Input;
        foreach (var extension in Extensions.Where(ex => ex.Value.Before != null).Reverse())
        {
            var taskDefinition = new DoTaskDefinition()
            {
                Do = extension.Value.Before!
            };
            var task = await Task.Workflow.CreateTaskAsync(taskDefinition, JsonPointer.Create("before", extension.Key), input, Task, true, cancellationToken).ConfigureAwait(false);
            var executor = await CreateTaskExecutorAsync(task, taskDefinition, Task.Workflow.Instance.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
            await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            if (executor.Task.Instance.Next == FlowDirective.Exit)
            {
                await SetResultAsync(executor.Task.Instance.Output, executor.Task.Definition.Then, cancellationToken).ConfigureAwait(false);
                return;
            }
            input = executor.Task.Instance.Output ?? new JsonObject();
            Executors.Remove(executor);
        }
    }

    /// <summary>
    /// Executes the <see cref="ITaskInstance"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task ExecuteCoreAsync(CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <summary>
    /// Executes code after the task executes, typically extensions, if any
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual async Task AfterExecuteAsync(CancellationToken cancellationToken)
    {
        if (Task.Instance.IsExtension || Extensions == null) return;
        var output = Task.Instance.Output ?? new JsonObject();
        foreach (var extension in Extensions.Where(ex => ex.Value.After != null).Reverse())
        {
            var taskDefinition = new DoTaskDefinition()
            {
                Do = extension.Value.After!
            };
            var task = await Task.Workflow.CreateTaskAsync(taskDefinition, JsonPointer.Create("after", extension.Key), output, Task, true, cancellationToken).ConfigureAwait(false);
            var executor = await CreateTaskExecutorAsync(task, taskDefinition, Task.Workflow.Instance.ContextData, Task.Arguments, cancellationToken).ConfigureAwait(false);
            await executor.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            if (executor.Task.Instance.Next == FlowDirective.Exit) break;
            output = executor.Task.Instance.Output ?? new JsonObject();
            Executors.Remove(executor);
            await executor.DisposeAsync().ConfigureAwait(false);
        }
    }

    /// <inheritdoc/>
    public async Task SuspendAsync(CancellationToken cancellationToken = default)
    {
        foreach (var executor in Executors)
        {
            await executor.SuspendAsync(cancellationToken).ConfigureAwait(false);
            Executors.Remove(executor);
        }
        Stopwatch.Stop();
        await SuspendCoreAsync(cancellationToken).ConfigureAwait(false);
        await Task.SuspendAsync(cancellationToken).ConfigureAwait(false);
        Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Suspended));
        if (!TaskCompletionSource.Task.IsCompleted) TaskCompletionSource.SetResult();
        CancellationTokenSource?.Cancel();
    }

    /// <summary>
    /// Suspends the <see cref="ITaskInstance"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task SuspendCoreAsync(CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public async Task RetryAsync(Error cause, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(cause);
        Stopwatch.Stop();
        await Task.RetryAsync(cause, cancellationToken).ConfigureAwait(false);
        await RetryCoreAsync(cause, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retries to run the <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="cause">The <see cref="Sdk.Models.Error"/> that caused the retry attempt</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task RetryCoreAsync(Error cause, CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public async Task SetErrorAsync(Error error, CancellationToken cancellationToken = default)
    {
        Stopwatch.Stop();
        await SetErrorCoreAsync(error, cancellationToken).ConfigureAwait(false);
        await Task.SetErrorAsync(error, cancellationToken).ConfigureAwait(false);
        Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Faulted));
        Subject.OnError(new RuntimeErrorException(error));
        if (!TaskCompletionSource.Task.IsCompleted) TaskCompletionSource.SetResult();
        if (CancellationTokenSource != null) await CancellationTokenSource.CancelAsync().ConfigureAwait(false);
    }

    /// <summary>
    /// Faults the handled <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="error"><see cref="Sdk.Models.Error"/> to set</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task SetErrorCoreAsync(Error error, CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public async Task SetResultAsync(JsonNode? result = null, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        if (Task.Instance.Status != TaskStatus.Running) return;
        Stopwatch.Stop();
        if (string.IsNullOrWhiteSpace(then)) then = FlowDirective.Continue;
        var output = result ?? new JsonObject();
        var arguments = GetExpressionEvaluationArguments() ?? [];
        arguments[RuntimeExpressions.Arguments.Output] = output.DeepClone();
        if (Task.Definition.Output?.As is not null)  output = (await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.Output.As, output, arguments, cancellationToken).ConfigureAwait(false))?.AsObject();
        if (Task.Definition.Export?.As is not null)
        {
            var context = await Task.Workflow.Expressions.EvaluateAsync(Task.Definition.Export.As, output ?? new JsonObject(), arguments, cancellationToken).ConfigureAwait(false);
            if (context is JsonObject jsonObject) await Task.SetContextDataAsync(jsonObject, cancellationToken).ConfigureAwait(false);
        }
        await AfterExecuteAsync(cancellationToken).ConfigureAwait(false);
        await SetResultCoreAsync(output, then, cancellationToken).ConfigureAwait(false);
        await Task.SetResultAsync(output, then, cancellationToken).ConfigureAwait(false);
        Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Completed));
        Subject.OnCompleted();
        if (!TaskCompletionSource.Task.IsCompleted) TaskCompletionSource.SetResult();
    }

    /// <summary>
    /// Sets the <see cref="ITaskInstance"/>'s result and transitions to '<see cref="TaskStatus.Completed"/>'.
    /// </summary>
    /// <param name="result">The <see cref="ITaskInstance"/>'s result, if any</param>
    /// <param name="then">The <see cref="FlowDirective"/> to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task SetResultCoreAsync(JsonNode? result, string then, CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public async Task CancelAsync(CancellationToken cancellationToken = default)
    {
        foreach (var executor in Executors)
        {
            await executor.CancelAsync(cancellationToken).ConfigureAwait(false);
            Executors.Remove(executor);
        }
        Stopwatch.Stop();
        await Task.CancelAsync(cancellationToken).ConfigureAwait(false);
        await DoCancelAsync(cancellationToken).ConfigureAwait(false);
        Subject.OnNext(new TaskLifeCycleEvent(TaskLifeCycleEventType.Cancelled));
        if (!TaskCompletionSource.Task.IsCompleted) TaskCompletionSource.SetCanceled(cancellationToken);
        CancellationTokenSource?.Cancel();
    }

    /// <summary>
    /// Cancels the <see cref="ITaskInstance"/>
    /// </summary>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    protected virtual Task DoCancelAsync(CancellationToken cancellationToken) => System.Threading.Tasks.Task.CompletedTask;

    /// <inheritdoc/>
    public virtual async Task SkipAsync(JsonNode? result, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default)
    {
        if (Task.Instance.Status != null) return;
        Stopwatch.Stop();
        if (string.IsNullOrWhiteSpace(then)) then = FlowDirective.Continue;
        var output = result;
        await Task.SkipAsync(output, then, cancellationToken).ConfigureAwait(false);
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
        var parameters = Task.Workflow.GetExpressionEvaluationArguments();
        if (Task.Arguments?.Count > 0) foreach (var (key, value) in Task.Arguments) parameters.TryAdd(key, value?.DeepClone());
        parameters[RuntimeExpressions.Arguments.Context] = Task.Workflow.Instance.ContextData.DeepClone();
        parameters[RuntimeExpressions.Arguments.Task] = descriptor.DeepClone();
        parameters[RuntimeExpressions.Arguments.Input] = Task.Instance.Input.DeepClone();
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
        var process = ExecutionContextFactory.Create(Task.Workflow, definition, instance, arguments);
        var executor = ExecutorFactory.Create(process);
        await executor.InitializeAsync(cancellationToken).ConfigureAwait(false);
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
            try { await executor.DisposeAsync().ConfigureAwait(false); }
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
        await DisposeAsync(disposing: true).ConfigureAwait(false);
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
