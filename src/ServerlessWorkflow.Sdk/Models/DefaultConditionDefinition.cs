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
        /// Gets/sets <see cref="DefaultConditionDefinition"/>'s name
        /// </summary>
        [ProtoMember(1, Name = "name")]
        [DataMember(Order = 1, Name = "name")]
        [YamlMember(Alias = "name")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public virtual string? Name { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the next transition of the workflow if there is valid matches
        /// </summary>
        [ProtoMember(2, Name = "transition")]
        [DataMember(Order = 2, Name = "transition")]
        [YamlMember(Alias = "transition")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "transition"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("transition"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
        protected virtual OneOf<TransitionDefinition, string>? TransitionValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="StateDefinition"/>'s transition to another <see cref="StateDefinition"/> upon completion
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual TransitionDefinition? Transition
        {
            get
            {
                if (this.TransitionValue?.T1Value == null
                    && !string.IsNullOrWhiteSpace(this.TransitionValue?.T2Value))
                    return new() { NextState = this.TransitionValue.T2Value };
                else
                    return this.TransitionValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.TransitionValue = null;
                else
                    this.TransitionValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the name of the <see cref="StateDefinition"/> to transition to upon completion
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual string? TransitionToStateName
        {
            get
            {
                return this.TransitionValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.TransitionValue = null;
                else
                    this.TransitionValue = value;
            }
        }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the object used to configure the end of the workflow
        /// </summary>
        [ProtoMember(3, Name = "end")]
        [DataMember(Order = 3, Name = "end")]
        [YamlMember(Alias = "end")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "end"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
        [System.Text.Json.Serialization.JsonPropertyName("end"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
        protected virtual OneOf<EndDefinition, bool>? EndValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="StateDefinition"/>'s transition to another <see cref="StateDefinition"/> upon completion
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual EndDefinition? End
        {
            get
            {
                if (this.EndValue?.T1Value == null
                    && this.EndValue != null 
                    && this.EndValue.T2Value)
                    return new() { };
                else
                    return this.EndValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.EndValue = null;
                else
                    this.EndValue = value;
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the <see cref="StateDefinition"/> is the end of a logicial workflow path
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual bool IsEnd
        {
            get
            {
                if (this.EndValue == null)
                    return false;
                else
                    return this.EndValue.T2Value;
            }
            set
            {
                this.EndValue = value;
            }
        }

    }

}