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
    /// Represents an object used to define a reusable function
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class FunctionDefinition
    {

        /// <summary>
        /// Gets/sets a unique function name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Name { get; set; } = null!;

        /// <summary>
        /// Gets/sets the operation. If type '<see cref="FunctionType.Rest"/>', combination of the function/service OpenAPI definition URI and the operationID of the operation that needs to be invoked, separated by a '#'. 
        /// If type is `<see cref="FunctionType.Expression"/>` defines the workflow expression.
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string Operation { get; set; } = null!;

        /// <summary>
        /// Gets/sets the type of the defined function. Defaults to '<see cref="FunctionType.Rest"/>'
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual FunctionType Type { get; set; } = FunctionType.Rest;

        /// <summary>
        /// Gets/sets the reference to the <see cref="AuthenticationDefinition"/> to use when invoking the function. Ignored when <see cref="Type"/> has been set to <see cref="FunctionType.Expression"/>
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual string? AuthRef { get; set; }

        /// <summary>
        /// Gets/sets the function's metadata
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual Any? Metadata { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
