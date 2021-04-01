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
using YamlDotNet.Serialization;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a reference to an <see cref="EventDefinition"/>
    /// </summary>
    public class EventReference
    {

        /// <summary>
        /// Gets the name of the 'produced' event that triggers the action
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "triggerEventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("triggerEventRef")]
        [YamlMember(Alias = "triggerEventRef")]
        public virtual string TriggerEvent { get; set; }

        /// <summary>
        /// Gets the name of the 'consumed' event that triggers the action
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "resultEventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("resultEventRef")]
        [YamlMember(Alias = "resultEventRef")]
        public virtual string ResultEvent { get; set; }

        /// <summary>
        /// Gets/sets the data to become the cloud event's payload. 
        /// If string type, an expression which selects parts of the states data output to become the data (payload) of the event referenced by 'triggerEventRef'. 
        /// If object type, a custom object to become the data (payload) of the event referenced by 'triggerEventRef'.
        /// </summary>
        public virtual JToken Data { get; set; }

        /// <summary>
        /// Gets/sets additional extension context attributes to the produced event
        /// </summary>
        public virtual JObject ContextAttributes { get; set; } = new JObject();

    }

}