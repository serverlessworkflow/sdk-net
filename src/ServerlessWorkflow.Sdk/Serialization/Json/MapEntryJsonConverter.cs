namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the <see cref="JsonConverter"/> used to write and read <see cref="MapEntry{TKey, TValue}"/> instances
/// </summary>
/// <typeparam name="TKey">The type of the <see cref="MapEntry{TKey, TValue}"/> key</typeparam>
/// <typeparam name="TValue">The type of the <see cref="MapEntry{TKey, TValue}"/> value</typeparam>
public class MapEntryJsonConverter<TKey, TValue>
    : JsonConverter<MapEntry<TKey, TValue>> where TKey : notnull
{

    /// <inheritdoc/>
    public override MapEntry<TKey, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException("Expected StartObject token");
        var kvp = JsonSerializer.Deserialize<IDictionary<TKey, TValue>>(ref reader, options)?.FirstOrDefault();
        if (kvp.HasValue) return new(kvp.Value.Key, kvp.Value.Value);
        else return null;
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, MapEntry<TKey, TValue>? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }
        writer.WriteStartObject();
        writer.WritePropertyName(value.Key.ToString()!);
        JsonSerializer.Serialize(writer, value.Value, options);
        writer.WriteEndObject();
    }

}
