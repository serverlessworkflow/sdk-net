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
using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization
{
    /// <summary>
    /// Represents an <see cref="INodeDeserializer"/> used to deserialize <see cref="JArray"/>s
    /// </summary>
    public class JArrayDeserializer
        : INodeDeserializer
    {

        /// <summary>
        /// Initializes a new <see cref="JArrayDeserializer"/>
        /// </summary>
        /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
        public JArrayDeserializer(INodeDeserializer inner)
        {
            this.Inner = inner;
        }

        /// <summary>
        /// Gets the inner <see cref="INodeDeserializer"/>
        /// </summary>
        protected INodeDeserializer Inner { get; }

        /// <inheritdoc/>
        public virtual bool Deserialize(IParser reader, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
        {
            if (!typeof(JToken).IsAssignableFrom(expectedType))
                return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
            try
            {
                if (!this.Inner.Deserialize(reader, typeof(object[]), nestedObjectDeserializer, out value))
                    return false;
                value = JToken.FromObject(value);
                return true;
            }
            catch
            {
                value = null;
                return false;
            } 
        }

    }

}
