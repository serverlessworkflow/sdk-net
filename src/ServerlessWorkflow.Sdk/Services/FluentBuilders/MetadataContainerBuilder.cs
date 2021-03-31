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
using System.Collections.Generic;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Represents the base class for all <see cref="IMetadataContainerBuilder{TContainer}"/>
    /// </summary>
    /// <typeparam name="TContainer">The type of the <see cref="IMetadataContainerBuilder{TContainer}"/></typeparam>
    public abstract class MetadataContainerBuilder<TContainer>
        : IMetadataContainerBuilder<TContainer>
        where TContainer : class, IMetadataContainerBuilder<TContainer>
    {

        /// <inheritdoc/>
        public abstract JObject Metadata { get; }

        /// <inheritdoc/>
        public virtual TContainer WithMetadata(string key, object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));
            JToken token = null;
            if (value != null)
                token = JToken.FromObject(value);
            this.Metadata.Add(key, token);
            return (TContainer)(object)this;
        }

        /// <inheritdoc/>
        public virtual TContainer WithMetadata(IDictionary<string, object> metadata)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            foreach (KeyValuePair<string, object> kvp in metadata)
            {
                this.WithMetadata(kvp.Key, kvp.Value);
            }
            return (TContainer)(object)this;
        }

    }

}
