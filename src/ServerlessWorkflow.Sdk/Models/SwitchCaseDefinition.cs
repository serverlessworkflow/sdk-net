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
    public abstract class SwitchCaseDefinition
    {

        /// <summary>
        /// Gets the <see cref="SwitchCaseDefinition"/>'s type
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
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
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="SwitchCaseDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(Transition))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(Transition))]
        [YamlMember(Alias = nameof(Transition))]
        protected virtual JToken TransitionToken { get; set; }

        private TransitionDefinition _Transition;
        /// <summary>
        /// Gets/sets the <see cref="TransitionDefinition"/> that should be performed when the condition is met
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual TransitionDefinition Transition
        {
            get
            {
                if (this._Transition == null
                    && this.TransitionToken != null)
                {
                    if (this.TransitionToken.Type == JTokenType.String)
                        this._Transition = new TransitionDefinition() { To = this.TransitionToken.ToString() };
                    else
                        this._Transition = this.TransitionToken.ToObject<TransitionDefinition>();
                }
                return this._Transition;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._Transition = value;
                this.TransitionToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="SwitchCaseDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(End))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(End))]
        [YamlMember(Alias = nameof(End))]
        protected virtual JToken EndToken { get; set; }

        private EndDefinition _End;
        /// <summary>
        /// Gets/sets the <see cref="EndDefinition"/> that should be performed when the condition is met
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual EndDefinition End
        {
            get
            {
                if (this._End == null
                    && this.EndToken != null)
                {
                    if (this.EndToken.Type == JTokenType.Boolean || this.EndToken.Type == JTokenType.String
                        && this.EndToken.ToObject<bool>())
                        this._End = new EndDefinition();
                    else
                        this._End = this.EndToken.ToObject<EndDefinition>();
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
                this.EndToken = JToken.FromObject(value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
