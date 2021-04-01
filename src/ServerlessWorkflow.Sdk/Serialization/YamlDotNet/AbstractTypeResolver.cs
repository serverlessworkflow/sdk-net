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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Represents the service used to resolve abstract type implementations
    /// </summary>
    public class AbstractTypeResolver
    {

        /// <summary>
        /// Initializes a new <see cref="AbstractTypeResolver"/>
        /// </summary>
        /// <param name="abstractType">The abstract type to resolve the implementation type for</param>
        public AbstractTypeResolver(Type abstractType)
        {
            DiscriminatorAttribute discriminatorAttribute = abstractType.GetCustomAttribute<DiscriminatorAttribute>();
            if (discriminatorAttribute == null)
                throw new NullReferenceException($"Failed to find the required '{nameof(DiscriminatorAttribute)}'");
            this.DiscriminatorProperty = abstractType.GetProperty(discriminatorAttribute.Property, BindingFlags.Default | BindingFlags.Public | BindingFlags.Instance);
            if (this.DiscriminatorProperty == null)
                throw new NullReferenceException($"Failed to find the specified discriminator property '{discriminatorAttribute.Property}' in type '{abstractType.Name}'");
            this.TypeMappings = new Dictionary<string, Type>();
            foreach (Type derivedType in typeof(StateType).Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType == abstractType))
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
        /// Gets the abstract type to resolve the implementation type for
        /// </summary>
        protected Type AbstractType { get; }

        /// <summary>
        /// Gets the discriminator <see cref="PropertyInfo"/> of the abstract type to convert
        /// </summary>
        protected PropertyInfo DiscriminatorProperty { get; }

        /// <summary>
        /// Gets an <see cref="Dictionary{TKey, TValue}"/> containing the mappings of the converted type's derived types
        /// </summary>
        protected Dictionary<string, Type> TypeMappings { get; }

        /// <summary>
        /// Attempts to resolve the abstract type's implementation based on the specified <see cref="ParsingEventStream"/>
        /// </summary>
        /// <param name="stream">The <see cref="ParsingEventStream"/> to use to resolve the implementation type</param>
        /// <param name="implementationType">The resulting implementation type</param>
        /// <returns>A boolean indicating whether or not the implementation type could be resolved thanks to the specified <see cref="ParsingEventStream"/></returns>
        public bool TryResolve(ParsingEventStream stream, out Type implementationType)
        {
            implementationType = null;
            if (stream.TryFindMappingEntry(scalar =>
            {
                return string.Equals(scalar.Value, this.DiscriminatorProperty.Name, StringComparison.InvariantCultureIgnoreCase);
            }, out Scalar key, out ParsingEvent value))
            {
                if (value is Scalar valueScalar)
                    return this.TypeMappings.TryGetValue(valueScalar.Value.ToLower(), out implementationType);
            }
            return false;
        }


    }

}
