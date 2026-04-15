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

namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents a JSON converter for <see cref="OneOf{T1, T2}"/> types.
/// </summary>
/// <typeparam name="T1">The first possible type.</typeparam>
/// <typeparam name="T2">The second possible type.</typeparam>
public sealed class OneOfJsonConverter<T1, T2>
    : JsonConverter<OneOf<T1, T2>>
{

    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(OneOf<T1, T2>) || typeToConvert == typeof(T1) || typeToConvert == typeof(T2);

    /// <inheritdoc/>
    public override OneOf<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var raw = doc.RootElement.GetRawText();
        if (TryDeserialize<T1>(raw, options, out var t1)) return new OneOf<T1, T2>(t1!);
        if (TryDeserialize<T2>(raw, options, out var t2)) return new OneOf<T1, T2>(t2!);
        throw new JsonException($"Value does not match either {typeof(T1).Name} or {typeof(T2).Name}.");
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, OneOf<T1, T2> value, JsonSerializerOptions options)
    {
        value.Switch
        (
            a =>
            {
                var typeInfo = options.GetTypeInfo(typeof(T1)) ?? throw new JsonException($"Missing JsonTypeInfo for {typeof(T1).FullName}.");
                JsonSerializer.Serialize(writer, a, typeInfo);
            },
            b =>
            {
                var typeInfo = options.GetTypeInfo(typeof(T2)) ?? throw new JsonException($"Missing JsonTypeInfo for {typeof(T2).FullName}.");
                JsonSerializer.Serialize(writer, b, typeInfo);
            }
        );
    }

    static bool TryDeserialize<T>(string json, JsonSerializerOptions options, out T? value)
    {
        try
        {
            var typeInfo = options.GetTypeInfo(typeof(T)) ?? throw new JsonException($"Missing JsonTypeInfo for {typeof(T).FullName}.");
            value = (T?)JsonSerializer.Deserialize(json, typeInfo);
            return value is not null || json == "null";
        }
        catch
        {
            value = default;
            return false;
        }
    }

}