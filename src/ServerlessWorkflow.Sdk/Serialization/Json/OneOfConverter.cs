namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the service used to convert <see cref="OneOf{T1, T2}"/>
/// </summary>
/// <typeparam name="T1">The first type alternative</typeparam>
/// <typeparam name="T2">The second type alternative</typeparam>
public class OneOfConverter<T1, T2>
    : JsonConverter<OneOf<T1, T2>>
{

    /// <inheritdoc/>
    public override OneOf<T1, T2>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var doc = JsonDocument.ParseValue(ref reader);
        try
        {
            var value = Serializer.Json.Deserialize<T1>(doc.RootElement);
            if (value == null) return null;
            return new(value);
        }
        catch
        {
            var value = Serializer.Json.Deserialize<T2>(doc.RootElement);
            if (value == null) return null;
            return new(value);
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, OneOf<T1, T2> value, JsonSerializerOptions options)
    {
        string? json;
        if (value.T1Value != null) json = Serializer.Json.Serialize(value.T1Value);
        else json = Serializer.Json.Serialize(value.T2Value);
        writer.WriteRawValue(json);
    }

}