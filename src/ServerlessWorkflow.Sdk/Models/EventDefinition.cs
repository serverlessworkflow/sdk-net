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
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to define events and their correlations
    /// </summary>
    public class EventDefinition
    {

        /// <summary>
        /// Gets/sets the Unique event name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the cloud event source
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Source { get; set; }

        /// <summary>
        /// Gets/sets the cloud event type
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// Gets/sets a value that defines the CloudEvent as either '<see cref="EventKind.Consumed"/>' or '<see cref="EventKind.Produced"/>' by the workflow. Default is '<see cref="EventKind.Consumed"/>'.
        /// </summary>
        public virtual EventKind Kind { get; set; } = EventKind.Consumed;

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the <see cref="EventCorrelationDefinition"/>s used to define the way the cloud event is correlated
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "correlation"), MinLength(1)]
        [System.Text.Json.Serialization.JsonPropertyName("correlation")]
        [YamlMember(Alias = "correlation")]
        public virtual List<EventCorrelationDefinition> Correlations { get; set; } = new List<EventCorrelationDefinition>();

        /// <summary>
        /// Gets/sets the <see cref="EventDefinition"/>'s metadata
        /// </summary>
        public virtual JObject Metadata { get; set; } = new JObject();

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
