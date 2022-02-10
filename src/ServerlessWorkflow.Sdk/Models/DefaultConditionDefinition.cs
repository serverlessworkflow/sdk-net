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

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to define the transition of the workflow if there is no matching data conditions or event timeout is reached
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class DefaultConditionDefinition
    {

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the next transition of the workflow if there is valid matches
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
        public virtual OneOf<TransitionDefinition, string> Transition { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the object used to configure the end of the workflow
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
        public virtual OneOf<EndDefinition, bool> End { get; set; }

    }

}