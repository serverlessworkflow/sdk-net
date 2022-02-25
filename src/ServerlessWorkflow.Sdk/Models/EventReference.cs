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
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a reference to an <see cref="EventDefinition"/>
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class EventReference
    {

        /// <summary>
        /// Gets the name of the event to produce
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "triggerEventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("triggerEventRef")]
        [YamlMember(Alias = "triggerEventRef")]
        [ProtoMember(1, IsRequired = true, Name = "triggerEventRef")]
        [DataMember(Order = 1, IsRequired = true, Name = "triggerEventRef")]
        public virtual string ProduceEvent { get; set; } = null!;

        /// <summary>
        /// Gets the name of the event to consume
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "resultEventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("resultEventRef")]
        [YamlMember(Alias = "resultEventRef")]
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string ResultEvent { get; set; } = null!;

        /// <summary>
        /// Gets/sets the data to become the cloud event's payload. 
        /// If string type, an expression which selects parts of the states data output to become the data (payload) of the event referenced by '<see cref="ProduceEvent"/>'. 
        /// If object type, a custom object to become the data (payload) of the event referenced by '<see cref="ProduceEvent"/>'.
        /// </summary>
        [ProtoMember(3, Name = "data")]
        [DataMember(Order = 3, Name = "data")]
        [YamlMember(Alias = "data")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "data"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<DynamicObject, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("data"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<DynamicObject, string>))]
        protected virtual OneOf<DynamicObject, string>? DataValue { get; set; }

        /// <summary>
        /// Gets/sets a custom object to become the data (payload) of the event referenced by '<see cref="ProduceEvent"/>'
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual DynamicObject? Data
        {
            get
            {
                return this.DataValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.DataValue = null;
                else
                    this.DataValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an expression which selects parts of the states data output to become the data (payload) of the event referenced by '<see cref="ProduceEvent"/>'
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual string? DataExpression
        {
            get
            {
                return this.DataValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.DataValue = null;
                else
                    this.DataValue = value;
            }
        }

        /// <summary>
        /// Gets/sets additional extension context attributes to the produced event
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual DynamicObject? ContextAttributes { get; set; }

        /// <summary>
        /// Gets the maximum amount of time to wait for the result event. If not defined it be set to the actionExecutionTimeout
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? ConsumeEventTimeout { get; set; }

        /// <summary>
        /// Gets/sets the reference event's <see cref="Sdk.InvocationMode"/>. Default is <see cref="InvocationMode.Synchronous"/>.
        /// </summary>
        /// <remarks>
        /// Default value of this property is sync, meaning that workflow execution should wait until the function completes (the result event is received).<para></para>
        /// If set to async, workflow execution should just produce the trigger event and should not wait for the result event
        /// </remarks>
        [Newtonsoft.Json.JsonProperty(PropertyName = "invoke")]
        [System.Text.Json.Serialization.JsonPropertyName("invoke")]
        [YamlMember(Alias = "invoke")]
        [ProtoMember(6, Name = "invoke")]
        [DataMember(Order = 6, Name = "invoke")]
        public virtual InvocationMode InvocationMode { get; set; } = InvocationMode.Synchronous;


    }

}