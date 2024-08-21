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

using ServerlessWorkflow.Sdk.Models;
using System.Collections.Concurrent;
using System.Text.Json;

namespace ServerlessWorkflow.Sdk.Serialization.Json;

/// <summary>
/// Represents the <see cref="JsonConverterFactory"/> used to serialize/deserialize to/from <see cref="IOneOf"/> instances
/// </summary>
public class OneOfConverter 
    : JsonConverterFactory
{

    static readonly ConcurrentDictionary<Type, JsonConverter> ConverterCache = new();

    /// <inheritdoc/>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType) return false;
        var genericType = typeToConvert.GetGenericTypeDefinition();
        return genericType == typeof(OneOf<,>);
    }

    /// <inheritdoc/>
    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return ConverterCache.GetOrAdd(typeToConvert, (type) =>
        {
            var typeArgs = type.GetGenericArguments();
            var converterType = typeof(OneOfConverterInner<,>).MakeGenericType(typeArgs);
            return (JsonConverter?)Activator.CreateInstance(converterType)!;
        });
    }

    class OneOfConverterInner<T1, T2> : JsonConverter<OneOf<T1, T2>>
    {

        /// <inheritdoc/>
        public override OneOf<T1, T2>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null) return null;
            var document = JsonDocument.ParseValue(ref reader);
            var rootElement = document.RootElement;
            try
            {
                var value1 = JsonSerializer.Deserialize<T1>(rootElement.GetRawText(), options);
                if (value1 != null) return new OneOf<T1, T2>(value1);
            }
            catch (JsonException) { }
            try
            {
                var value2 = JsonSerializer.Deserialize<T2>(rootElement.GetRawText(), options);
                if (value2 != null) return new OneOf<T1, T2>(value2);
            }
            catch (JsonException) { throw new JsonException($"Cannot deserialize {rootElement.GetRawText()} as either {typeof(T1).Name} or {typeof(T2).Name}"); }
            throw new JsonException("Unexpected error during deserialization.");
        }

        public override void Write(Utf8JsonWriter writer, OneOf<T1, T2> value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
                return;
            }
            switch (value.TypeIndex)
            {
                case 1:
                    JsonSerializer.Serialize(writer, value.T1Value, options);
                    break;
                case 2:
                    JsonSerializer.Serialize(writer, value.T2Value, options);
                    break;
                default:
                    throw new JsonException("Invalid index value.");
            }
        }

    }

}

