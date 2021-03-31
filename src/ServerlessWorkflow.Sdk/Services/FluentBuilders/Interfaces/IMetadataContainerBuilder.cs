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
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Defines the fundamentals of a service used to build metadata containers
    /// </summary>
    /// <typeparam name="TContainer">The type of the <see cref="IMetadataContainerBuilder{TContainer}"/></typeparam>
    public interface IMetadataContainerBuilder<TContainer>
        where TContainer : class, IMetadataContainerBuilder<TContainer>
    {

        /// <summary>
        /// Gets the container's metadata
        /// </summary>
        JObject Metadata { get; }

        /// <summary>
        /// Adds the specified metadata
        /// </summary>
        /// <param name="key">The metadata key</param>
        /// <param name="value">The metadata value</param>
        /// <returns>The configured container</returns>
        TContainer WithMetadata(string key, object value);

        /// <summary>
        /// Adds the specified metadata
        /// </summary>
        /// <param name="metadata">An <see cref="IDictionary{TKey, TValue}"/> representing the container's metadata</param>
        /// <returns>The configured container</returns>
        TContainer WithMetadata(IDictionary<string, object> metadata);

    }

}
