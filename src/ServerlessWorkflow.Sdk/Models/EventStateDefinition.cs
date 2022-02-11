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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a workflow state that awaits one or more events and perform actions when they are received
    /// </summary>
    [DiscriminatorValue(StateType.Event)]
    [ProtoContract]
    [DataContract]
    public class EventStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="EventStateDefinition"/>
        /// </summary>
        public EventStateDefinition()
            : base(StateType.Event)
        {

        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the <see cref="EventStateDefinition"/> awaits one or all of defined events.
        /// If 'true', consuming one of the defined events causes its associated actions to be performed. If 'false', all of the defined events must be consumed in order for actions to be performed. Defaults to 'true'.
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual bool Exclusive { get; set; } = true;

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="EventStateDefinition"/>'s triggers and actions
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "onEvents")]
        [System.Text.Json.Serialization.JsonPropertyName("onEvents")]
        [YamlMember(Alias = "onEvents")]
        [ProtoMember(2, Name = "onEvents")]
        [DataMember(Order = 2, Name = "onEvents")]
        public virtual List<EventStateTriggerDefinition> Triggers { get; set; } = new List<EventStateTriggerDefinition>();

        /// <summary>
        /// Gets/sets the duration to wait for incoming events
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets the <see cref="EventStateTriggerDefinition"/> with the specified id
        /// </summary>
        /// <param name="id">The id of the <see cref="EventStateTriggerDefinition"/> to get</param>
        /// <returns>The <see cref="EventStateTriggerDefinition"/> with the specified id</returns>
        public virtual EventStateTriggerDefinition GetTrigger(int id)
        {
            return this.Triggers.ElementAt(id);
        }

        /// <summary>
        /// Attempts to get the <see cref="EventStateTriggerDefinition"/> with the specified id
        /// </summary>
        /// <param name="id">The name of the <see cref="EventStateTriggerDefinition"/> to get</param>
        /// <param name="trigger">The <see cref="EventStateTriggerDefinition"/> with the specified id</param>
        /// <returns>A boolean indicating whether or not a <see cref="EventStateTriggerDefinition"/> with the specified id could be found</returns>
        public virtual bool TryGetTrigger(int id, out EventStateTriggerDefinition trigger)
        {
            trigger = null!;
            try
            {
                trigger = this.GetTrigger(id);
            }
            catch
            {
                return false;
            }
            return trigger != null;
        }

    }

}
