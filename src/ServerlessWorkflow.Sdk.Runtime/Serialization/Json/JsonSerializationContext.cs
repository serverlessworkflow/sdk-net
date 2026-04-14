namespace ServerlessWorkflow.Sdk.Runtime.Serialization.Json;

/// <summary>
/// Represents the source generation context for JSON serialization and deserialization of the Serverless Workflow SDK runtime types.
/// </summary>
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(AuthenticationResult))]
[JsonSerializable(typeof(CloudEvent))]
[JsonSerializable(typeof(CorrelationContext))]
[JsonSerializable(typeof(OAuth2Token))]
[JsonSerializable(typeof(SchemaValidationResult))]
[JsonSerializable(typeof(TaskLifeCycleEvent))]
[JsonSerializable(typeof(TaskRetryAttempt))]
[JsonSerializable(typeof(TaskRun))]
[JsonSerializable(typeof(TaskInstance))]
[JsonSerializable(typeof(WorkflowLifeCycleEvent))]
[JsonSerializable(typeof(WorkflowRun))]
[JsonSerializable(typeof(WorkflowInstance))]
public partial class JsonSerializationContext
    : JsonSerializerContext
{



}
