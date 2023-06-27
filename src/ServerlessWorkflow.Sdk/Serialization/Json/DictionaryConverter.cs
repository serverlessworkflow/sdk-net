using Neuroglia;

namespace ServerlessWorkflow.Sdk.Serialization;

/// <summary>
/// Represents the <see cref="JsonConverter"/> used to serialize and deserialize <see cref="IDictionary{TKey, TValue}"/> instances, and unwraps their values (as opposed to keeping JsonElement values)
/// </summary>
public class DictionaryConverter
    : JsonConverter<IDictionary<string, object>>
{

    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(IDictionary<string, object>);

    /// <inheritdoc/>
    public override IDictionary<string, object>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var doc = JsonDocument.ParseValue(ref reader);
        var result = Serializer.Json.Deserialize<Dictionary<string, object>>(doc.RootElement.ToString())!;
        return result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value is JsonElement jsonElement ? jsonElement.ToObject() : kvp.Value)!;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, IDictionary<string, object> value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
    }

}

/// <summary>
/// Represents the <see cref="JsonConverter"/> used to serialize and deserialize <see cref="DynamicMapping"/>s
/// </summary>
public class DynamicMappingConverter
    : JsonConverter<DynamicMapping>
{

    /// <inheritdoc/>
    public override DynamicMapping? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var doc = JsonDocument.ParseValue(ref reader);
        var result = Serializer.Json.Deserialize<Dictionary<string, object>>(doc.RootElement.ToString())!;
        return new(result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value is JsonElement jsonElement ? jsonElement.ToObject() : kvp.Value)!);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, DynamicMapping value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Properties, options);
    }

}
