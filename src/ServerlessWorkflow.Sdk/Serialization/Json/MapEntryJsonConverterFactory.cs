namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the <see cref="JsonConverterFactory"/> used to create <see cref="MapEntryJsonConverterFactory"/>
/// </summary>
public class MapEntryJsonConverterFactory
    : JsonConverterFactory
{

    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(MapEntry<,>);

    /// <inheritdoc/>
    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var keyType = typeToConvert.GetGenericArguments()[0];
        var valueType = typeToConvert.GetGenericArguments()[1];
        var converterType = typeof(MapEntryJsonConverter<,>).MakeGenericType(keyType, valueType);
        return (JsonConverter)Activator.CreateInstance(converterType)!;
    }

}
