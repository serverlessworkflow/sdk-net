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
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an external definition reference
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ExternalArrayDefinition
        : JArray
    {

        /// <summary>
        /// Initializes a new <see cref="ExternalArrayDefinition"/>
        /// </summary>
        public ExternalArrayDefinition()
            : base()
        {
            this.Loaded = true;
        }

        /// <summary>
        /// Initializes a new <see cref="ExternalArrayDefinition"/>
        /// </summary>
        /// <param name="definitionUri">The <see cref="Uri"/> used to reference the file that defines the element described by the <see cref="ExternalArrayDefinition"/></param>
        public ExternalArrayDefinition(Uri definitionUri)
            : base()
        {
            this.DefinitionUri = definitionUri;
            this.Loaded = false;
        }

        /// <summary>
        /// Gets the <see cref="Uri"/> used to reference the file that defines the element described by the <see cref="ExternalArrayDefinition"/>
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual Uri DefinitionUri { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="ExternalArrayDefinition"/> has been loaded
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual bool Loaded { get; }

    }

}
