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
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Converters
{
    /// <summary>
    /// Represents a <see cref="JsonConverter"/> used to deserialize implementations of the specified abstract class
    /// </summary>
    /// <typeparam name="T">The type of the abstract class to deserialize</typeparam>
    public class AbstractClassConverter<T>
        : JsonConverter
    {

        /// <summary>
        /// Initializes a new <see cref="AbstractClassConverter{T}"/>
        /// </summary>
        public AbstractClassConverter()
        {
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
                this.TypeMappings.Add(discriminatorValue.ToLower(), derivedType);
            }
        }

        /// <summary>
        /// Gets the discriminator <see cref="PropertyInfo"/> of the abstract type to convert
        /// </summary>
        protected PropertyInfo DiscriminatorProperty { get; }

        /// <summary>
        /// Gets an <see cref="Dictionary{TKey, TValue}"/> containing the mappings of the converted type's derived types
        /// </summary>
        protected Dictionary<string, Type> TypeMappings { get; }

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
            if (!objectType.TryGetCustomAttribute(out DiscriminatorAttribute discriminatorAttribute))
                throw new NullReferenceException($"Failed to find the required '{nameof(DiscriminatorAttribute)}'");
            JObject jObject = JObject.Load(reader);
            string discriminatorValue = jObject.Property(this.DiscriminatorProperty.Name, StringComparison.InvariantCultureIgnoreCase).Value.ToString();
            if (!this.TypeMappings.TryGetValue(discriminatorValue.ToLower(), out Type derivedType))
                throw new JsonException($"Failed to find the derived type with the specified discriminator value '{discriminatorValue}'");
            object result = Activator.CreateInstance(derivedType, true);
            serializer.Populate(jObject.CreateReader(), result);
            return result;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }

}
