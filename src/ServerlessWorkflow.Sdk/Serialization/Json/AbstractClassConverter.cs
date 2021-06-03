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
using ServerlessWorkflow.Sdk;
using ServerlessWorkflow.Sdk.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Text.Json.Serialization.Converters
{
    /// <summary>
    /// Represents the <see cref="JsonConverter"/> used to convert to/from an abstract class
    /// </summary>
    /// <typeparam name="T">The type of the abstract class to convert to/from</typeparam>
    public class AbstractClassConverter<T>
        : JsonConverter<T>
    {

        /// <summary>
        /// Initializes a new <see cref="AbstractClassConverter{T}"/>
        /// </summary>
        /// <param name="jsonSerializerOptions">The current <see cref="JsonSerializerOptions"/></param>
        public AbstractClassConverter(JsonSerializerOptions jsonSerializerOptions)
        {
            this.JsonSerializerOptions = jsonSerializerOptions;
            DiscriminatorAttribute discriminatorAttribute = typeof(T).GetCustomAttribute<DiscriminatorAttribute>();
            if (discriminatorAttribute == null)
                throw new NullReferenceException($"Failed to find the required '{nameof(DiscriminatorAttribute)}'");
            this.DiscriminatorProperty = typeof(T).GetProperty(discriminatorAttribute.Property, BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance);
            if (this.DiscriminatorProperty == null)
                throw new NullReferenceException($"Failed to find the specified discriminator property '{discriminatorAttribute.Property}' in type '{typeof(T).Name}'");
            this.TypeMappings = new Dictionary<string, Type>();
            foreach (Type derivedType in typeof(StateType).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType == typeof(T)))
            {
                DiscriminatorValueAttribute discriminatorValueAttribute = derivedType.GetCustomAttribute<DiscriminatorValueAttribute>();
                if (discriminatorValueAttribute == null)
                    continue;
                string discriminatorValue = null;
                if (discriminatorValueAttribute.Value.GetType().IsEnum)
                    discriminatorValue = EnumHelper.Stringify(discriminatorValueAttribute.Value, this.DiscriminatorProperty.PropertyType);
                else
                    discriminatorValue = discriminatorValueAttribute.Value.ToString();
                this.TypeMappings.Add(discriminatorValue, derivedType);
            }
        }

        /// <summary>
        /// Gets the current <see cref="JsonSerializerOptions"/>
        /// </summary>
        protected JsonSerializerOptions JsonSerializerOptions { get; }

        /// <summary>
        /// Gets the discriminator <see cref="PropertyInfo"/> of the abstract type to convert
        /// </summary>
        protected PropertyInfo DiscriminatorProperty { get; }

        /// <summary>
        /// Gets an <see cref="Dictionary{TKey, TValue}"/> containing the mappings of the converted type's derived types
        /// </summary>
        protected Dictionary<string, Type> TypeMappings { get; }

        /// <inheritdoc/>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException("Start object token type expected");
            using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
            string discriminatorPropertyName = this.JsonSerializerOptions?.PropertyNamingPolicy == null ? this.DiscriminatorProperty.Name : this.JsonSerializerOptions.PropertyNamingPolicy.ConvertName(this.DiscriminatorProperty.Name);
            if (!jsonDocument.RootElement.TryGetProperty(discriminatorPropertyName, out JsonElement discriminatorProperty))
                throw new JsonException($"Failed to find the required '{this.DiscriminatorProperty.Name}' discriminator property");
            string discriminatorValue = discriminatorProperty.GetString();
            if (!this.TypeMappings.TryGetValue(discriminatorValue, out Type derivedType))
                throw new JsonException($"Failed to find the derived type with the specified discriminator value '{discriminatorValue}'");
            string json = jsonDocument.RootElement.GetRawText();
            return (T)JsonSerializer.Deserialize(json, derivedType, this.JsonSerializerOptions);
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }

    }

}
