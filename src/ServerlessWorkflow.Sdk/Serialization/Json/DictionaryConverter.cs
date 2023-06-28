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
