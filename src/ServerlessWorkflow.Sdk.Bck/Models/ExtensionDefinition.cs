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
using System;
using System.ComponentModel.DataAnnotations;
namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents the definition of a Serverless Workflow extension
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ExtensionDefinition
    {

        /// <summary>
        /// Gets/sets the extension's unique id
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [DataMember(Order = 1, IsRequired = true)]
        [ProtoMember(1)]
        public virtual string ExtensionId { get; set; } = null!;

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> to a resource containing the workflow extension definition (json or yaml)
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [DataMember(Order = 2, IsRequired = true)]
        [ProtoMember(2)]
        public virtual Uri Resource { get; set; } = null!;

    }

}
