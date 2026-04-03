namespace ServerlessWorkflow.Sdk.Runtime.Serialization.Json;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(RuntimeError))]
[JsonSerializable(typeof(SchemaValidationResult))]
[JsonSerializable(typeof(TaskLifeCycleEvent))]
public partial class JsonSerializationContext
    : JsonSerializerContext
{



}
