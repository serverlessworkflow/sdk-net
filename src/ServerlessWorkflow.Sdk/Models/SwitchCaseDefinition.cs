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
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the base class for all <see cref="SwitchStateDefinition"/> case implementations
    /// </summary>
    [ProtoContract]
    [DataContract]
    [ProtoInclude(100, typeof(DataCaseDefinition))]
    [ProtoInclude(200, typeof(EventCaseDefinition))]
    public abstract class SwitchCaseDefinition
    {

        /// <summary>
        /// Gets the <see cref="SwitchCaseDefinition"/>'s type
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public ConditionType Type
        {
            get
            {
                if (this.Transition != null)
                    return ConditionType.Transition;
                else
                    return ConditionType.End;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="SwitchCaseDefinition"/>'s name
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="SwitchCaseDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "transition")]
        [System.Text.Json.Serialization.JsonPropertyName("transition")]
        [YamlMember(Alias = "transition")]
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
        protected virtual OneOf<TransitionDefinition, string> TransitionToken { get; set; }

        private TransitionDefinition _Transition;
        /// <summary>
        /// Gets/sets the <see cref="TransitionDefinition"/> that should be performed when the condition is met
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual TransitionDefinition Transition
        {
            get
            {
                if (this._Transition == null
                    && this.TransitionToken != null)
                {
                    if (this.TransitionToken.Value1 == null)
                        this._Transition = new TransitionDefinition() { To = this.TransitionToken.Value2 };
                    else
                        this._Transition = this.TransitionToken.Value1;
                }
                return this._Transition;
            }
            set
            {
                if (value == null)
                {
                    this._Transition = null;
                    this.TransitionToken = null;
                    return;
                }
                this._Transition = value;
                this.TransitionToken = new(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="SwitchCaseDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "end")]
        [System.Text.Json.Serialization.JsonPropertyName("end")]
        [YamlMember(Alias = "end")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
        protected virtual OneOf<EndDefinition, bool> EndToken { get; set; }

        private EndDefinition _End;
        /// <summary>
        /// Gets/sets the <see cref="EndDefinition"/> that should be performed when the condition is met
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual EndDefinition End
        {
            get
            {
                if (this._End == null
                    && this.EndToken != null)
                {
                    if (this.EndToken.Value1 == null
                        && this.EndToken.Value2)
                        this._End = new();
                    else
                        this._End = this.EndToken.Value1;
                }
                return this._End;
            }
            set
            {
                if (value == null)
                {
                    this._End = null;
                    this.EndToken = null;
                    return;
                }
                this._End = value;
                this.EndToken = new(value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
