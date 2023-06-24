using System.Text.Json.Serialization.Metadata;

namespace ServerlessWorkflow.Sdk.Serialization;

/// <summary>
/// Provides functionality to serialize/deserialize objects to/from JSON and YAML
/// </summary>
public static partial class Serializer
{

    /// <summary>
    /// Provides functionality to serialize/deserialize objects to/from JSON
    /// </summary>
    public static class Json
    {

        /// <summary>
        /// Gets/sets an <see cref="Action{T}"/> used to configure the <see cref="JsonSerializerOptions"/> used by default
        /// </summary>
        public static Action<JsonSerializerOptions>? DefaultOptionsConfiguration { get; set; } = (options) =>
        {
            options.TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { JsonTypeInfoModifiers.IncludeNonPublicProperties() }
            };
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            options.Converters.Add(new DictionaryConverter());
        };

        static JsonSerializerOptions? _DefaultOptions;
        /// <summary>
        /// Gets/sets the default <see cref="JsonSerializerOptions"/>
        /// </summary>
        public static JsonSerializerOptions DefaultOptions
        {
            get
            {
                if (_DefaultOptions != null) return _DefaultOptions;
                _DefaultOptions = new JsonSerializerOptions();
                DefaultOptionsConfiguration?.Invoke(_DefaultOptions);
                return _DefaultOptions;
            }
        }

        /// <summary>
        /// Serializes the specified object to JSON
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="graph">The object to serialized</param>
        /// <returns>The JSON of the serialized object</returns>
        public static string Serialize<T>(T graph) => JsonSerializer.Serialize(graph, DefaultOptions);

        /// <summary>
        /// Serializes the specified object into a new <see cref="JsonNode"/>
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="graph">The object to serialize</param>
        /// <returns>A new <see cref="JsonNode"/></returns>
        public static JsonNode? SerializeToNode<T>(T graph) => JsonSerializer.SerializeToNode(graph, DefaultOptions);

        /// <summary>
        /// Serializes the specified object into a new <see cref="JsonElement"/>
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="graph">The object to serialize</param>
        /// <returns>A new <see cref="JsonElement"/></returns>
        public static JsonElement? SerializeToElement<T>(T graph) => JsonSerializer.SerializeToElement(graph, DefaultOptions);

        /// <summary>
        /// Deserializes the specified JSON input
        /// </summary>
        /// <param name="json">The JSON input to deserialize</param>
        /// <param name="expectedType">The type to deserialize the specified JSON into</param>
        /// <returns>The deserialized value</returns>
        public static object? Deserialize(string json, Type expectedType) => JsonSerializer.Deserialize(json, expectedType, DefaultOptions);

        /// <summary>
        /// Deserializes the specified JSON input
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified JSON into</typeparam>
        /// <param name="json">The JSON input to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, DefaultOptions);

        /// <summary>
        /// Deserializes the specified JSON input
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified JSON into</typeparam>
        /// <param name="elem">The JSON input to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(JsonElement elem) => JsonSerializer.Deserialize<T>(elem, DefaultOptions);

        /// <summary>
        /// Deserializes the specified <see cref="JsonNode"/>
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified <see cref="JsonNode"/> into</typeparam>
        /// <param name="node">The <see cref="JsonNode"/> to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(JsonNode node) => node.Deserialize<T>(DefaultOptions);

    }

}
