namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="EmitTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="executionContextFactory">The service used to create <see cref="ITaskExecutionContext"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="task">The current <see cref="ITaskExecutionContext"/></param>
public sealed class EmitTaskExecutor(IServiceProvider serviceProvider, ILogger<EmitTaskExecutor> logger, ITaskExecutionContextFactory executionContextFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ITaskExecutionContext<EmitTaskDefinition> task)
    : TaskExecutor<EmitTaskDefinition>(serviceProvider, logger, executionContextFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var attributes = Task.Definition.Emit.Event.With.DeepClone().AsObject() ?? new JsonObject();
        if (!attributes.ContainsKey("id")) attributes["id"] = Guid.NewGuid().ToString();
        if (!attributes.ContainsKey("specversion")) attributes["specversion"] = "1.0";
        if (!attributes.ContainsKey("time")) attributes["time"] = DateTimeOffset.Now.ToString("o");
        var result = (await Task.Workflow.Expressions.EvaluateAsync(attributes, Task.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false))?.AsObject();
        //todo: publish event to event bus when available
        await SetResultAsync(result, Task.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}
