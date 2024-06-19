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

using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace ServerlessWorkflow.Sdk.Serialization.Yaml;

/// <summary>
/// Represents the service used to serialize/deserialize <see cref="MapEntry{TKey, TValue}"/> values to/from YAML
/// </summary>
/// <param name="serializerFactory">A function used to create a new <see cref="ISerializer"/></param>
/// <param name="deserializerFactory">A function used to create a new <see cref="IDeserializer"/></param>
public class MapEntryYamlConverter(Func<ISerializer> serializerFactory, Func<IDeserializer> deserializerFactory)
    : IYamlTypeConverter
{

    /// <inheritdoc/>
    public virtual bool Accepts(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(MapEntry<,>);

    /// <inheritdoc/>
    public virtual object? ReadYaml(IParser parser, Type type) => this.CreateGenericConverter(type).ReadYaml(parser, type);

    /// <inheritdoc/>
    public virtual void WriteYaml(IEmitter emitter, object? value, Type type) => this.CreateGenericConverter(type).WriteYaml(emitter, value, type);

    /// <summary>
    /// Creates a new generic <see cref="MapEntryConverter{TKey, TValue}"/>
    /// </summary>
    /// <param name="type">The type to <see cref="MapEntry{TKey, TValue}"/> type to create a new <see cref="MapEntryConverter{TKey, TValue}"/> for</param>
    /// <returns></returns>
    protected virtual IYamlTypeConverter CreateGenericConverter(Type type)
    {
        var typeArguments = type.GetGenericArguments();
        var converterType = typeof(MapEntryConverter<,>).MakeGenericType(typeArguments);
        return (IYamlTypeConverter)Activator.CreateInstance(converterType, serializerFactory(), deserializerFactory())!;
    }

    class MapEntryConverter<TKey, TValue> (ISerializer serializer, IDeserializer deserializer)
        : IYamlTypeConverter 
        where TKey : notnull
    {

        /// <inheritdoc/>
        public bool Accepts(Type type) => type == typeof(MapEntry<TKey, TValue>);

        /// <inheritdoc/>
        public virtual object ReadYaml(IParser parser, Type type) 
        {
            parser.Consume<MappingStart>();
            var key = deserializer.Deserialize<TKey>(parser);
            var value = deserializer.Deserialize<TValue>(parser);
            parser.Consume<MappingEnd>();
            return new MapEntry<TKey, TValue>(key, value);
        }

        /// <inheritdoc/>
        public virtual void WriteYaml(IEmitter emitter, object? value, Type type)
        {
            if (value == null)
            {
                emitter.Emit(new Scalar(null, null, string.Empty));
                return;
            }
            var entry = (MapEntry<TKey, TValue>)value;
            emitter.Emit(new MappingStart(null, null, true, MappingStyle.Block));
            var yaml = serializer.Serialize(entry.Key).Trim();
            using var keyStream = new MemoryStream(Encoding.UTF8.GetBytes(yaml));
            using var keyStreamReader = new StreamReader(keyStream);
            var parser = new Parser(keyStreamReader);
            while (parser.MoveNext())
            {
                if (parser.Current == null || parser.Current is DocumentEnd) break;
                if (parser.Current is StreamStart || parser.Current is DocumentStart) continue;
                emitter.Emit(parser.Current);
            }
            yaml = serializer.Serialize(entry.Value).Trim();
            using var valueStream = new MemoryStream(Encoding.UTF8.GetBytes(yaml));
            using var valueStreamReader = new StreamReader(valueStream);
            parser = new Parser(valueStreamReader);
            while (parser.MoveNext())
            {
                if (parser.Current == null || parser.Current is DocumentEnd) break;
                if (parser.Current is StreamStart || parser.Current is DocumentStart) continue;
                emitter.Emit(parser.Current);
            }
            emitter.Emit(new MappingEnd());
        }

    }

}