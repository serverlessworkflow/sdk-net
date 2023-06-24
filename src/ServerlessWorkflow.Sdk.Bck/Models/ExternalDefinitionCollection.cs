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
using System.Collections.Generic;
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a <see cref="List{T}"/> that can be loaded from an external definition file
    /// </summary>
    /// <typeparam name="T">The type of elements contained by the <see cref="ExternalDefinitionCollection{T}"/></typeparam>
    public class ExternalDefinitionCollection<T>
        : List<T>
    {

        /// <summary>
        /// Initializes a new <see cref="ExternalDefinitionCollection{T}"/>
        /// </summary>
        public ExternalDefinitionCollection()
            : base()
        {
            this.DefinitionUri = null!;
            this.Loaded = true;
        
        }

        /// <summary>
        /// Initializes a new <see cref="ExternalDefinitionCollection{T}"/>
        /// </summary>
        /// <param name="collection">The collection whose elements are copied into the <see cref="ExternalDefinitionCollection{T}"/></param>
        public ExternalDefinitionCollection(IEnumerable<T> collection)
            : base(collection)
        {
            this.DefinitionUri = null!;
            this.Loaded = true;
        }

        /// <summary>
        /// Initializes a new <see cref="ExternalDefinitionCollection{T}"/>
        /// </summary>
        /// <param name="definitionUri">The <see cref="Uri"/> used to reference the file that defines the elements contained by the <see cref="ExternalDefinitionCollection{T}"/></param>
        public ExternalDefinitionCollection(Uri definitionUri)
            : base()
        {
            this.DefinitionUri = definitionUri;
            this.Loaded = false;
        }

        /// <summary>
        /// Gets the <see cref="Uri"/> used to reference the file that defines the elements contained by the <see cref="ExternalDefinitionCollection{T}"/>
        /// </summary>
        public virtual Uri DefinitionUri { get; private set; }

        /// <summary>
        /// Gets a boolean indicating whether or not the <see cref="ExternalDefinitionCollection{T}"/> has been loaded
        /// </summary>
        public virtual bool Loaded { get; }

    }

}
