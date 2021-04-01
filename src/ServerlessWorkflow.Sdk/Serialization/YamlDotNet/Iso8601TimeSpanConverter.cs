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
using System;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.Converters
{

    /// <summary>
    /// Represents an <see cref="INodeDeserializer"/> used to deserialize ISO8601 <see cref="TimeSpan"/>s
    /// </summary>
    public class Iso8601TimeSpanConverter
        : INodeDeserializer
    {

        /// <summary>
        /// Initializes a new <see cref="Iso8601TimeSpanConverter"/>
        /// </summary>
        /// <param name="inner">The inner <see cref="INodeDeserializer"/></param>
        public Iso8601TimeSpanConverter(INodeDeserializer inner)
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
            if (expectedType != typeof(TimeSpan)
                && expectedType != typeof(TimeSpan?))
                return this.Inner.Deserialize(reader, expectedType, nestedObjectDeserializer, out value);
            if (!this.Inner.Deserialize(reader, typeof(string), nestedObjectDeserializer, out value))
                return false;
            value = Iso8601TimeSpan.Parse((string)value);
            return true;
        }

    }

}
