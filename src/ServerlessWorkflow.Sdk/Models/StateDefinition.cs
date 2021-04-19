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
using ServerlessWorkflow.Sdk.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a <see href="https://github.com/serverlessworkflow/specification/blob/master/specification.md#State-Definition">serverless workflow state definition</see>
    /// </summary>
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.AbstractClassConverterFactory))]
    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.AbstractClassConverterFactory))]
    [Discriminator(nameof(Type))]
    public abstract class StateDefinition
    {

        /// <summary>
        /// Initializes a new <see cref="StateDefinition"/>
        /// </summary>
        /// <param name="type">The <see cref="StateDefinition"/>'s type</param>
        protected StateDefinition(StateType type)
        {
            this.Type = type;
        }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/>'s type
        /// </summary>
        [YamlMember]
        public virtual StateType Type { get; protected set; }

        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s id
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="StateDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(End))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(End))]
        [YamlMember(Alias = nameof(End))]
        protected virtual JToken EndToken { get; set; }

        private EndDefinition _End;
        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s <see cref="EndDefinition"/>
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

        /// <summary>
        /// Gets/sets the filter to apply to the <see cref="StateDefinition"/>'s input and output data
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "stateDataFilter")]
        [System.Text.Json.Serialization.JsonPropertyName("stateDataFilter")]
        [YamlMember(Alias = "stateDataFilter")]
        public virtual StateDataFilterDefinition DataFilter { get; set; }

        /// <summary>
        /// Gets/sets the configuration of the <see cref="StateDefinition"/>'s error handling
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "onErrors")]
        [System.Text.Json.Serialization.JsonPropertyName("onErrors")]
        [YamlMember(Alias = "onErrors")]
        public virtual List<ErrorHandlerDefinition> Errors { get; set; } = new List<ErrorHandlerDefinition>();

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="StateDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(Transition))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(Transition))]
        [YamlMember(Alias = nameof(Transition))]
        protected virtual JToken TransitionToken { get; set; }

        private TransitionDefinition _Transition;
        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s <see cref="TransitionDefinition"/>
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
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="StateDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(DataInputSchema))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(DataInputSchema))]
        [YamlMember(Alias = nameof(DataInputSchema))]
        protected virtual JToken DataInputSchemaToken { get; set; }

        private DataInputSchemaDefinition _DataInputSchema;
        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s <see cref="DataInputSchemaDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual DataInputSchemaDefinition DataInputSchema
        {
            get
            {
                if (this._DataInputSchema == null
                    && this.DataInputSchemaToken != null)
                {
                    if (this.DataInputSchemaToken.Type == JTokenType.String)
                        this._DataInputSchema = new DataInputSchemaDefinition() { Schema = this.DataInputSchemaToken.Value<string>() };
                    else
                        this._DataInputSchema = this.EndToken.ToObject<DataInputSchemaDefinition>();
                }
                return this._DataInputSchema;
            }
            set
            {
                if (value == null)
                {
                    this._DataInputSchema = null;
                    this.DataInputSchemaToken = null;
                    return;
                }
                this._DataInputSchema = value;
                this.DataInputSchemaToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the id of the <see cref="StateDefinition"/> used to compensate the <see cref="StateDefinition"/>
        /// </summary>
        public virtual string CompensatedBy { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the <see cref="StateDefinition"/> is used for compensating another <see cref="StateDefinition"/>
        /// </summary>
        public virtual bool UseForCompensation { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s metadata
        /// </summary>
        public virtual JObject Metadata { get; set; } = new JObject();

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
