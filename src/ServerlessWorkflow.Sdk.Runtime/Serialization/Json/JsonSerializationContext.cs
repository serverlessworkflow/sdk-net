namespace ServerlessWorkflow.Sdk.Runtime.Serialization.Json;

/// <summary>
/// Represents the source generation context for JSON serialization and deserialization of the Serverless Workflow SDK runtime types.
/// </summary>
[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(AuthenticationResult))]
[JsonSerializable(typeof(CloudEvent))]
[JsonSerializable(typeof(OAuth2Token))]
[JsonSerializable(typeof(SchemaValidationResult))]
[JsonSerializable(typeof(TaskLifeCycleEvent))]
public partial class JsonSerializationContext
    : JsonSerializerContext
{



}
