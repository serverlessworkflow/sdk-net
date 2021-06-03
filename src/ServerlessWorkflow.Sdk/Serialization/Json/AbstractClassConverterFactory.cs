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
using ServerlessWorkflow.Sdk.Serialization;
using System.Collections.Generic;
using System.Reflection;

namespace System.Text.Json.Serialization.Converters
{

    /// <summary>
    /// Represents the <see cref="JsonConverterFactory"/> used to create <see cref="AbstractClassConverter{T}"/>
    /// </summary>
    public class AbstractClassConverterFactory
        : JsonConverterFactory
    {

        private static readonly Dictionary<Type, JsonConverter> _Converters = new();
        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing the mappings of types to their respective <see cref="JsonConverter"/>
        /// </summary>
        protected static Dictionary<Type, JsonConverter> Converters
        {
            get
            {
                return _Converters;
            }
        }

        /// <summary>
        /// Initializes a new <see cref="AbstractClassConverterFactory"/>
        /// </summary>
        /// <param name="jsonSerializerOptions">The current <see cref="System.Text.Json.JsonSerializerOptions"/></param>
        public AbstractClassConverterFactory(JsonSerializerOptions jsonSerializerOptions)
        {
            this.JsonSerializerOptions = jsonSerializerOptions;
        }

        /// <summary>
        /// Gets the current <see cref="JsonSerializerOptions"/>
        /// </summary>
        protected JsonSerializerOptions JsonSerializerOptions { get; }

        /// <inheritdoc/>
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsClass && typeToConvert.IsAbstract && typeToConvert.IsDefined(typeof(DiscriminatorAttribute));
        }

        /// <inheritdoc/>
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (!Converters.TryGetValue(typeToConvert, out JsonConverter converter))
            {
                Type converterType = typeof(AbstractClassConverter<>).MakeGenericType(typeToConvert);
                converter = (JsonConverter)Activator.CreateInstance(converterType, this.JsonSerializerOptions);
                Converters.Add(typeToConvert, converter);
            }
            return converter;
        }

    }

}
