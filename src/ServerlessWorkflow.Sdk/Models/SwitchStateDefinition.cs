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
using ServerlessWorkflow.Sdk.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a workflow state that can be seen as a workflow gateway: they can direct transitions of a workflow based on certain conditions
    /// </summary>
    [DiscriminatorValue(StateType.Switch)]
    public class SwitchStateDefinition
        : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="SwitchStateDefinition"/>
        /// </summary>
        public SwitchStateDefinition()
            : base(StateType.Switch)
        {

        }

        /// <summary>
        /// Gets the <see cref="SwitchStateDefinition"/>'s type
        /// </summary>
        public virtual SwitchStateType SwitchType
        {
            get
            {
                if (this.DataConditions != null)
                    return SwitchStateType.Data;
                else
                    return SwitchStateType.Event;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of <see cref="DataCaseDefinition"/>s between which to switch. Assigning the property sets the <see cref="SwitchStateDefinition"/>'s <see cref="SwitchType"/> to <see cref="SwitchStateType.Data"/>.
        /// </summary>
        public virtual List<DataCaseDefinition> DataConditions { get; set; } = new List<DataCaseDefinition>();

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> of <see cref="EventCaseDefinition"/>s between which to switch. Assigning the property sets the <see cref="SwitchStateDefinition"/>'s <see cref="SwitchType"/> to <see cref="SwitchStateType.Event"/>.
        /// </summary>
        public virtual List<EventCaseDefinition> EventConditions { get; set; } = new List<EventCaseDefinition>();

        /// <summary>
        /// Gets/sets the duration to wait for incoming events
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? EventTimeout { get; set; }

        /// <summary>
        /// Gets/sets an object used to configure the <see cref="SwitchStateDefinition"/>'s default condition, in case none of the specified conditions were met
        /// </summary>
        public virtual DefaultConditionDefinition DefaultCondition { get; set; }

    }

}
