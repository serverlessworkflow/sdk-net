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
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Represents an <see cref="INodeDeserializer"/> used to deserialize <see cref="Any"/> instances
    /// </summary>
    public class AnyDeserializer
        : INodeDeserializer
    {

        /// <summary>
        /// Initializes a new <see cref="AnyDeserializer"/>
        /// </summary>
        /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
        public AnyDeserializer(INodeDeserializer inner)
        {
            this.Inner = inner;
        }

        /// <summary>
        /// Gets the inner <see cref="INodeDeserializer"/>
        /// </summary>
        protected INodeDeserializer Inner { get; }

        /// <inheritdoc/>
        public virtual bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
        {
            value = null;
            if (!typeof(Any).IsAssignableFrom(expectedType))
                return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
            value = nestedObjectDeserializer(reader, typeof(Dictionary<string, object>));
            if(value != null)
                value = new Any((Dictionary<string, object>)value);
            return true;
        }

    }

}
