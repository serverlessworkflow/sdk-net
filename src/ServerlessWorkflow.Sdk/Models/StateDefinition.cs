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
    [ProtoContract]
    [ProtoInclude(100, typeof(CallbackStateDefinition))]
    [ProtoInclude(200, typeof(SleepStateDefinition))]
    [ProtoInclude(300, typeof(EventStateDefinition))]
    [ProtoInclude(400, typeof(ForEachStateDefinition))]
    [ProtoInclude(500, typeof(InjectStateDefinition))]
    [ProtoInclude(600, typeof(OperationStateDefinition))]
    [ProtoInclude(700, typeof(ParallelStateDefinition))]
    [ProtoInclude(800, typeof(SwitchStateDefinition))]
    [DataContract]
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
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual StateType Type { get; protected set; }

        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s id
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s id
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="StateDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
        public virtual OneOf<EndDefinition, bool> End { get; set; }

        /// <summary>
        /// Gets/sets the filter to apply to the <see cref="StateDefinition"/>'s input and output data
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "stateDataFilter")]
        [System.Text.Json.Serialization.JsonPropertyName("stateDataFilter")]
        [YamlMember(Alias = "stateDataFilter")]
        [ProtoMember(5, Name = "stateDataFilter")]
        [DataMember(Order = 5, Name = "stateDataFilter")]
        public virtual StateDataFilterDefinition DataFilter { get; set; }

        /// <summary>
        /// Gets/sets the configuration of the <see cref="StateDefinition"/>'s error handling
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "onErrors")]
        [System.Text.Json.Serialization.JsonPropertyName("onErrors")]
        [YamlMember(Alias = "onErrors")]
        [ProtoMember(6, Name = "onErrors")]
        [DataMember(Order = 6, Name = "onErrors")]
        public virtual List<ErrorHandlerDefinition> Errors { get; set; } = new List<ErrorHandlerDefinition>();

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="StateDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [ProtoMember(7)]
        [DataMember(Order = 7)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
        public virtual OneOf<TransitionDefinition, string> Transition { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="StateDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(DataInputSchema))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(DataInputSchema))]
        [YamlMember(Alias = nameof(DataInputSchema))]
        [ProtoMember(8, Name = nameof(DataInputSchema))]
        [DataMember(Order = 8, Name = nameof(DataInputSchema))]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<DataInputSchemaDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<DataInputSchemaDefinition, string>))]
        protected virtual OneOf<DataInputSchemaDefinition, string> DataInputSchemaToken { get; set; }

        private DataInputSchemaDefinition _DataInputSchema;
        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s <see cref="DataInputSchemaDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual DataInputSchemaDefinition DataInputSchema
        {
            get
            {
                if (this._DataInputSchema == null
                    && this.DataInputSchemaToken != null)
                {
                    if (this.DataInputSchemaToken.Value1 == null)
                        this._DataInputSchema = new DataInputSchemaDefinition() { Schema = this.DataInputSchemaToken.Value2 };
                    else
                        this._DataInputSchema = this.DataInputSchemaToken.Value1;
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
                this.DataInputSchemaToken = new(value);
            }
        }

        /// <summary>
        /// Gets/sets the id of the <see cref="StateDefinition"/> used to compensate the <see cref="StateDefinition"/>
        /// </summary>
        [ProtoMember(9)]
        [DataMember(Order = 9)]
        public virtual string CompensatedBy { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the <see cref="StateDefinition"/> is used for compensating another <see cref="StateDefinition"/>
        /// </summary>
        [ProtoMember(10)]
        [DataMember(Order = 10)]
        public virtual bool UsedForCompensation { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="StateDefinition"/>'s metadata
        /// </summary>
        [ProtoMember(11)]
        [DataMember(Order = 11)]
        public virtual Any Metadata { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
