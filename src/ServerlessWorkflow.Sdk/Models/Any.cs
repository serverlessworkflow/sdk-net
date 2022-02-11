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
using Neuroglia;
using Neuroglia.Serialization;
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object of any type
    /// </summary>
    [ProtoContract]
    [DataContract]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.AnyConverter))]
    public class Any
        : ProtoObject
    {

        /// <summary>
        /// Initializes a new <see cref="Any"/>
        /// </summary>
        public Any()
        {

        }

        /// <summary>
        /// Innitializes a new <see cref="Any"/>
        /// </summary>
        /// <param name="properties">An <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="Any"/>'s name/value property mappings</param>
        public Any(IDictionary<string, object> properties)
        {
            foreach (var property in properties)
            {
                this.Set(property.Key, property.Value);
            }
        }

        /// <inheritdoc/>
        [ProtoMember(1)]
        protected new List<ProtoField> Fields
        {
            get => base.Fields;
            set => base.Fields = value;
        }

        /// <summary>
        /// Creates a new <see cref="Any"/> from the specified object
        /// </summary>
        /// <returns>The object to create the new <see cref="Any"/> for</returns>
        public static new Any FromObject(object source)
        {
            return new(source.ToDictionary());
        }

    }

}
