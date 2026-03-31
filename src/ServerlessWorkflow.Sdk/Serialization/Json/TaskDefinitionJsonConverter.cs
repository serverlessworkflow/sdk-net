namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents a JSON converter for <see cref="TaskDefinition"/> objects, responsible for serializing and deserializing instances of <see cref="TaskDefinition"/> and its derived types to and from JSON format.
/// </summary>
public sealed class TaskDefinitionJsonConverter
    : JsonConverter<TaskDefinition>
{

    /// <inheritdoc/>
    public override TaskDefinition? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var namingPolicy = options.PropertyNamingPolicy ?? JsonNamingPolicy.CamelCase;
        using var document = JsonDocument.ParseValue(ref reader);
        var root = document.RootElement;
        if (root.ValueKind != JsonValueKind.Object) throw new JsonException($"Expected a JSON object to deserialize a {nameof(TaskDefinition)}.");
        if (root.TryGetProperty(namingPolicy.ConvertName(nameof(SetTaskDefinition.Set)), out var _)) return JsonSerializer.Deserialize(root, JsonSerializationContext.Default.SetTaskDefinition);
        throw new NotSupportedException($"Failed to determine the specific type of {nameof(TaskDefinition)} to deserialize.");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TaskDefinition value, JsonSerializerOptions options)
    {
        var json = value switch
        {
            SetTaskDefinition setTaskDefinition => JsonSerializer.Serialize(setTaskDefinition, JsonSerializationContext.Default.SetTaskDefinition),
            _ => throw new NotSupportedException($"The type {value.GetType().FullName} is not supported for JSON serialization.")
        };
        writer.WriteRawValue(json);
    }

}
