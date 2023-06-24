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
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a workflow state that performs an action, then waits for the callback event that denotes completion of the action
    /// </summary>
    [DiscriminatorValue(StateType.Callback)]
    [ProtoContract]
    [DataContract]
    public class CallbackStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="CallbackStateDefinition"/>
        /// </summary>
        public CallbackStateDefinition()
            : base(StateType.Callback)
        {

        }

        /// <summary>
        /// Gets/sets the action to be executed
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual ActionDefinition? Action { get; set; }

        /// <summary>
        /// Gets/sets a reference to the callback event to await
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "eventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("eventRef")]
        [YamlMember(Alias = "eventRef")]
        [ProtoMember(2, Name = "eventRef")]
        [DataMember(Order = 2, Name = "eventRef")]
        public virtual string? Event { get; set; }

        /// <summary>
        /// Gets/sets the time period to wait for incoming events
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601NullableTimeSpanConverter))]
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets the callback event data filter definition
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual EventDataFilterDefinition EventDataFilter { get; set; } = new EventDataFilterDefinition();

    }

}
