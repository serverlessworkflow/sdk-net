/*
 * Copyright 2020-Present The Serverless Workflow Specification Authors
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
using ServerlessWorkflow.Sdk.Serialization;
using System;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents a workflow state that waits for a certain amount of time before transitioning to a next state
    /// </summary>
    [DiscriminatorValue(StateType.Delay)]
    public class DelayStateDefinition
       : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="DelayStateDefinition"/>
        /// </summary>
        public DelayStateDefinition()
            : base(StateType.Delay)
        {

        }

        /// <summary>
        /// Gets/sets the amount of time to delay when in this state
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "timeDelay"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("timeDelay"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        [YamlMember(Alias = "timeDelay")]
        public virtual TimeSpan Delay { get; set; }

    }

}
