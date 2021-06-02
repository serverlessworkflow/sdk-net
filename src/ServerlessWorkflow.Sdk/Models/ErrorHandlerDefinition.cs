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
    /// Represents the definition of a workflow error handler
    /// </summary>
    public class ErrorHandlerDefinition
    {

        /// <summary>
        /// Gets/sets a domain-specific error name, or '*' to indicate all possible errors. If other handlers are declared, the <see cref="ErrorHandlerDefinition"/> will only be considered on errors that have NOT been handled by any other.
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        public virtual string Error { get; set; }

        /// <summary>
        /// Gets/sets the error code. Can be used in addition to the name to help runtimes resolve to technical errors/exceptions. Should not be defined if error is set to '*'.
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Gets/sets a reference to the <see cref="RetryStrategyDefinition"/> to use 
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "retryRef")]
        [System.Text.Json.Serialization.JsonPropertyName("retryRef")]
        [YamlMember(Alias = "retryRef")]
        public virtual string Retry { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "transition")]
        [System.Text.Json.Serialization.JsonPropertyName("transition")]
        [YamlMember(Alias = "transition")]
        protected virtual JToken TransitionToken { get; set; }

        private TransitionDefinition _Transition;
        /// <summary>
        /// Gets/sets the object used to configre the transition to execute on error.
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
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "end")]
        [System.Text.Json.Serialization.JsonPropertyName("end")]
        [YamlMember(Alias = "end")]
        protected virtual JToken EndToken { get; set; }

        private EndDefinition _End;
        /// <summary>
        /// Gets/sets the object used to configre the way the workflow ends on error
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
            return $"{this.Error}{(string.IsNullOrWhiteSpace(this.Code) ? string.Empty : $" (code: '{this.Code}')")}";
        }

    }

}
