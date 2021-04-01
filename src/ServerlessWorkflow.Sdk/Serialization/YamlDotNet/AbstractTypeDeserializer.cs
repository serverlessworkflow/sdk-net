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
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{

    /// <summary>
    /// Represents the <see cref="INodeDeserializer"/> used to deserialize abstract classes
    /// </summary>
    public class AbstractTypeDeserializer 
        : INodeDeserializer
    {

        /// <summary>
        /// Initializes a new <see cref="AbstractTypeDeserializer"/>
        /// </summary>
        /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
        public AbstractTypeDeserializer(INodeDeserializer inner)
        {
            this.Inner = inner;
        }

        /// <summary>
        /// Gets the inner <see cref="INodeDeserializer"/>
        /// </summary>
        protected INodeDeserializer Inner { get; }

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey, TValue}"/> containing all known <see cref="AbstractTypeResolver"/>s
        /// </summary>
        protected Dictionary<Type, AbstractTypeResolver> Resolvers { get; } = new Dictionary<Type, AbstractTypeResolver>();

        /// <inheritdoc/>
        public virtual bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
        {
            value = null;
            if (!reader.Accept(out MappingStart mapping))
                return false;
            if (expectedType.IsInterface
                || !expectedType.IsAbstract
                || !expectedType.TryGetCustomAttribute(out DiscriminatorAttribute discriminatorAttribute))
                return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
            if(!this.Resolvers.TryGetValue(expectedType, out AbstractTypeResolver resolver))
            {
                resolver = new AbstractTypeResolver(expectedType);
                this.Resolvers.Add(expectedType, resolver);
            }
            ParsingEventStream stream = ParsingEventStream.Create(reader);
            if (!resolver.TryResolve(stream, out Type concreteType))
                throw new NullReferenceException($"Failed to resolve the concrete type for the abstract type '{expectedType.Name}'");
            stream.Reset();
            return this.Inner.Deserialize(stream, concreteType, nestedObjectDeserializer, out value);
        }

    }

}
