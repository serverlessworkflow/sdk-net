// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

using System.Text.Json;

namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the <see cref="JsonConverterFactory"/> used to create <see cref="MapEntryJsonConverter"/>
/// </summary>
public class MapEntryJsonConverter
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

/// <summary>
/// Represents the <see cref="JsonConverter"/> used to write and read <see cref="MapEntry{TKey, TValue}"/> instances
/// </summary>
/// <typeparam name="TKey">The type of the <see cref="MapEntry{TKey, TValue}"/> key</typeparam>
/// <typeparam name="TValue">The type of the <see cref="MapEntry{TKey, TValue}"/> value</typeparam>
public class MapEntryJsonConverter<TKey, TValue> : JsonConverter<MapEntry<TKey, TValue>> where TKey : notnull
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
        writer.WriteRawValue(Neuroglia.Serialization.Json.JsonSerializer.Default.SerializeToText(value.Value));
        writer.WriteEndObject();
    }

}