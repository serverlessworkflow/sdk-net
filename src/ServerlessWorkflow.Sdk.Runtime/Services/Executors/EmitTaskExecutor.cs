namespace ServerlessWorkflow.Sdk.Runtime.Services.Executors;

/// <summary>
/// Represents an <see cref="ITaskExecutor"/> implementation used to execute <see cref="EmitTaskDefinition"/>s
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="taskProcessFactory">The service used to create <see cref="ITaskProcess"/>s</param>
/// <param name="executorFactory">The service used to create <see cref="ITaskExecutor"/>s</param>
/// <param name="schemaHandlerProvider">The service used to provide <see cref="ISchemaHandler"/> implementations</param>
/// <param name="cloudEventBus">The service used to publish and subscribe to <see cref="ICloudEvent"/>s</param>
/// <param name="task">The current <see cref="ITaskProcess"/></param>
public sealed class EmitTaskExecutor(IServiceProvider serviceProvider, ILogger<EmitTaskExecutor> logger, ITaskProcessFactory taskProcessFactory, ITaskExecutorFactory executorFactory, ISchemaHandlerProvider schemaHandlerProvider, ICloudEventBus cloudEventBus, ITaskProcess<EmitTaskDefinition> task)
    : TaskExecutor<EmitTaskDefinition>(serviceProvider, logger, taskProcessFactory, executorFactory, schemaHandlerProvider, task)
{

    /// <inheritdoc/>
    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var attributes = Task.Instance.Definition.Emit.Event.With.DeepClone().AsObject() ?? new JsonObject();
        if (!attributes.ContainsKey(CloudEventAttributes.Id)) attributes[CloudEventAttributes.Id] = Guid.NewGuid().ToString();
        if (!attributes.ContainsKey(CloudEventAttributes.SpecVersion)) attributes[CloudEventAttributes.SpecVersion] = CloudEvent.DefaultVersion;
        if (!attributes.ContainsKey(CloudEventAttributes.Time)) attributes[CloudEventAttributes.Time] = DateTimeOffset.Now.ToString("o");
        var result = await Task.Workflow.Expressions.EvaluateAsync(attributes, Task.Instance.State.Input, GetExpressionEvaluationArguments(), cancellationToken).ConfigureAwait(false);
        var cloudEvent = JsonSerializer.Deserialize(result, Serialization.Json.JsonSerializationContext.Default.CloudEvent);
        await cloudEventBus.PublishAsync(cloudEvent!, cancellationToken).ConfigureAwait(false);
        await SetResultAsync(result, Task.Instance.Definition.Then, cancellationToken).ConfigureAwait(false);
    }

}
