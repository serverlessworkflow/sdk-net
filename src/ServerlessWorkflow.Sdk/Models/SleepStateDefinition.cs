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
    /// Represents a workflow state that waits for a certain amount of time before transitioning to a next state
    /// </summary>
    [DiscriminatorValue(StateType.Sleep)]
    [ProtoContract]
    [DataContract]
    public class SleepStateDefinition
       : StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="SleepStateDefinition"/>
        /// </summary>
        public SleepStateDefinition()
            : base(StateType.Sleep)
        {

        }

        /// <summary>
        /// Gets/sets the amount of time to delay when in this state
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "timeDelay"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("timeDelay"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        [YamlMember(Alias = "timeDelay")]
        [ProtoMember(1, Name = "timeDelay")]
        [DataMember(Order = 1, Name = "timeDelay")]
        public virtual TimeSpan Delay { get; set; }

    }

}
