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
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a reference to a <see cref="FunctionDefinition"/>
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class FunctionReference
    {

        /// <summary>
        /// Gets/sets the referenced function's name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string RefName { get; set; } = null!;

        /// <summary>
        /// Gets/sets a <see cref="Any"/> that contains the parameters of the function to invoke
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual Any? Arguments { get; set; }

        /// <summary>
        /// Gets/sets a <see href="https://spec.graphql.org/June2018/#sec-Selection-Sets">GraphQL selection set</see>
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string? SelectionSet { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.RefName;
        }

    }

}