/*
 * Copyright 2021-Present The Serverless Workflow Specification Authors
 * <p>
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * <p>
 * http://www.apache.org/licenses/LICENSE-2.0
 * <p>
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using Newtonsoft.Json.Linq;
using ServerlessWorkflow.Sdk;
using ServerlessWorkflow.Sdk.Serialization;
using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Converters
{

    /// <summary>
    /// Represents a service used to create <see cref="AbstractClassConverter{T}"/>
    /// </summary>
    public class AbstractClassConverterFactory
        : JsonConverter
    {

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing the mappings of types to their respective <see cref="JsonConverter"/>
        /// </summary>
        private static readonly Dictionary<Type, JsonConverter> Converters = new();

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsClass && objectType.TryGetCustomAttribute(out DiscriminatorAttribute _);
        }

        /// <inheritdoc/>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (!objectType.IsAbstract)
            {
                JObject jObject = JObject.Load(reader);
                object result = Activator.CreateInstance(objectType, true);
                serializer.Populate(jObject.CreateReader(), result);
                return result;
            }
            if (!objectType.TryGetCustomAttribute(out DiscriminatorAttribute discriminatorAttribute))
                throw new NullReferenceException($"Failed to find the required '{nameof(DiscriminatorAttribute)}'");
            if (!Converters.TryGetValue(objectType, out JsonConverter converter))
            {
                Type converterType = typeof(AbstractClassConverter<>).MakeGenericType(objectType);
                converter = (JsonConverter)Activator.CreateInstance(converterType);
                Converters.Add(objectType, converter);
            }
            return converter.ReadJson(reader, objectType, existingValue, serializer);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }

}
