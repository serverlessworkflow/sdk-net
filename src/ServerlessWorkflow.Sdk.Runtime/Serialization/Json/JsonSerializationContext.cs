namespace ServerlessWorkflow.Sdk.Runtime.Serialization.Json;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(Error))]
[JsonSerializable(typeof(RetryAttempt))]
[JsonSerializable(typeof(TaskInstance))]
[JsonSerializable(typeof(TaskRun))]
[JsonSerializable(typeof(TaskLifeCycleEvent))]
public partial class JsonSerializationContext
    : JsonSerializerContext
{



}
