namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the <see cref="JsonConverter{T}"/> used to convert <see cref="TimeSpan"/>s from and to ISO 8601 durations
/// </summary>
public class Iso8601TimeSpanConverter
    : JsonConverter<TimeSpan>
{

    /// <inheritdoc/>
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var iso8601Input = reader.GetString();
        if (string.IsNullOrWhiteSpace(iso8601Input)) return TimeSpan.Zero;
        return Iso8601TimeSpan.Parse(iso8601Input);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Iso8601TimeSpan.Format(value));
    }

}

/// <summary>
/// Represents the <see cref="JsonConverter{T}"/> used to convert <see cref="TimeSpan"/>s from and to ISO 8601 durations
/// </summary>
public class Iso8601NullableTimeSpanConverter
    : JsonConverter<TimeSpan?>
{

    /// <inheritdoc/>
    public override TimeSpan? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var iso8601Input = reader.GetString();
        if (string.IsNullOrWhiteSpace(iso8601Input)) return null;
        return Iso8601TimeSpan.Parse(iso8601Input);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeSpan? value, JsonSerializerOptions options)
    {
        if (value.HasValue) writer.WriteStringValue(Iso8601TimeSpan.Format(value.Value));
    }

}