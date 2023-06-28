// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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

        static JsonSerializerOptions? _DefaultIndentedOptions;
        /// <summary>
        /// Gets/sets the default indented <see cref="JsonSerializerOptions"/>
        /// </summary>
        public static JsonSerializerOptions DefaultIndentedOptions
        {
            get
            {
                if (_DefaultIndentedOptions != null) return _DefaultIndentedOptions;
                _DefaultIndentedOptions = new JsonSerializerOptions();
                DefaultOptionsConfiguration?.Invoke(_DefaultIndentedOptions);
                _DefaultIndentedOptions.WriteIndented = true;
                return _DefaultIndentedOptions;
            }
        }

        /// <summary>
        /// Serializes the specified object to JSON
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="graph">The object to serialized</param>
        /// <param name="indented">A boolean indicating whether or not to indent the output</param>
        /// <returns>The JSON of the serialized object</returns>
        public static string Serialize<T>(T graph, bool indented = false) => indented ?
            JsonSerializer.Serialize(graph, DefaultIndentedOptions) :
            JsonSerializer.Serialize(graph, DefaultOptions);

        /// <summary>
        /// Serializes the specified object to JSON
        /// </summary>
        /// <param name="graph">The object to serialized</param>
        /// <param name="inputType">The type of object to serialize</param>
        /// <param name="indented">A boolean indicating whether or not to indent the output</param>
        /// <returns>The JSON of the serialized object</returns>
        public static string Serialize(object graph, Type inputType, bool indented = false) => indented ?
            JsonSerializer.Serialize(graph, inputType, DefaultIndentedOptions) :
            JsonSerializer.Serialize(graph, inputType, DefaultOptions);

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
        /// Serializes the specified object into a new <see cref="JsonDocument"/>
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="graph">The object to serialize</param>
        /// <returns>A new <see cref="JsonDocument"/></returns>
        public static JsonDocument? SerializeToDocument<T>(T graph) => JsonSerializer.SerializeToDocument(graph, DefaultOptions);

        /// <summary>
        /// Serializes an object to the specified <see cref="Stream"/>
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        /// <param name="stream">The <see cref="Stream"/> to serialize the object to</param>
        /// <param name="graph">The object to serialize</param>
        /// <param name="indented">A boolean indicating whether or not to indent the output</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        public static Task SerializeAsync<T>(Stream stream, T graph, bool indented = false, CancellationToken cancellationToken = default) => indented ?
            JsonSerializer.SerializeAsync(stream, graph, DefaultIndentedOptions, cancellationToken) :
            JsonSerializer.SerializeAsync(stream, graph, DefaultOptions, cancellationToken);

        /// <summary>
        /// Serializes an object to the specified <see cref="Stream"/>
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to serialize the object to</param>
        /// <param name="graph">The object to serialize</param>
        /// <param name="inputType">The type of the object to serialize</param>
        /// <param name="indented">A boolean indicating whether or not to indent the output</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new awaitable <see cref="Task"/></returns>
        public static Task SerializeAsync(Stream stream, object graph, Type inputType, bool indented = false, CancellationToken cancellationToken = default) => indented ?
            JsonSerializer.SerializeAsync(stream, graph, inputType, DefaultIndentedOptions, cancellationToken) :
            JsonSerializer.SerializeAsync(stream, graph, inputType, DefaultOptions, cancellationToken);

        /// <summary>
        /// Deserializes the specified JSON input
        /// </summary>
        /// <param name="json">The JSON input to deserialize</param>
        /// <param name="returnType">The type to deserialize the JSON into</param>
        /// <returns>An object that results from the specified JSON input's deserialization</returns>
        public static object? Deserialize(string json, Type returnType) => JsonSerializer.Deserialize(json, returnType, DefaultOptions);

        /// <summary>
        /// Deserializes the specified <see cref="JsonElement"/>
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified <see cref="JsonElement"/> into</typeparam>
        /// <param name="element">The <see cref="JsonElement"/> to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(JsonElement element) => JsonSerializer.Deserialize<T>(element, DefaultOptions);

        /// <summary>
        /// Deserializes the specified <see cref="JsonDocument"/>
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified <see cref="JsonDocument"/> into</typeparam>
        /// <param name="document">The <see cref="JsonDocument"/> to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(JsonDocument document) => JsonSerializer.Deserialize<T>(document, DefaultOptions);

        /// <summary>
        /// Deserializes the specified JSON input
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified JSON into</typeparam>
        /// <param name="json">The JSON input to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, DefaultOptions);

        /// <summary>
        /// Deserializes the specified <see cref="JsonNode"/>
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified <see cref="JsonNode"/> into</typeparam>
        /// <param name="node">The <see cref="JsonNode"/> to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(JsonNode node) => JsonSerializer.Deserialize<T>(node, DefaultOptions);

        /// <summary>
        /// Deserializes the specified JSON input
        /// </summary>
        /// <typeparam name="T">The type to deserialize the specified JSON into</typeparam>
        /// <param name="buffer">The JSON input to deserialize</param>
        /// <returns>The deserialized value</returns>
        public static T? Deserialize<T>(ReadOnlySpan<byte> buffer) => JsonSerializer.Deserialize<T>(buffer, DefaultOptions);

        /// <summary>
        /// Deserializes the specified <see cref="Stream"/> as a new <see cref="IAsyncEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T">The expected type of elements to enumerate</typeparam>
        /// <param name="stream">The <see cref="Stream"/> to deserialize</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
        /// <returns>A new <see cref="IAsyncEnumerable{T}"/></returns>
        public static IAsyncEnumerable<T?> DeserializeAsyncEnumerable<T>(Stream stream, CancellationToken cancellationToken = default) => JsonSerializer.DeserializeAsyncEnumerable<T>(stream, DefaultOptions, cancellationToken);

        /// <summary>
        /// Converts the specified YAML input into JSON
        /// </summary>
        /// <param name="yaml">The YAML input to convert</param>
        /// <param name="indented">A boolean indicating whether or not to indent the output</param>
        /// <returns>The YAML input converted into JSON</returns>
        public static string ConvertFromYaml(string yaml, bool indented = false)
        {
            if (string.IsNullOrWhiteSpace(yaml)) return null!;
            var graph = Yaml.Deserialize<object>(yaml);
            return Serialize(graph, indented);
        }

        /// <summary>
        /// Converts the specified JSON input into YAML
        /// </summary>
        /// <param name="json">The JSON input to convert</param>
        /// <returns>The JSON input converted into YAML</returns>
        public static string ConvertToYaml(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) return null!;
            var graph = Deserialize<object>(json);
            return Yaml.Serialize(graph);
        }

    }

}
