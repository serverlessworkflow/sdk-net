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
using Newtonsoft.Json.Schema;
using ProtoBuf;
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the definition of a Serverless Workflow
    /// </summary>
    [DataContract]
    [ProtoContract]
    public class WorkflowDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s unique identifier
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string? Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s domain-specific workflow identifier
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string? Key { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s name
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string Name { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s description
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual string? Description { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s version
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual string Version { get; set; } = null!;

        /// <summary>
        /// Gets/sets the <see cref="System.Version"/> of the Serverless Workflow schema to use
        /// </summary>
        [ProtoMember(6)]
        [DataMember(Order = 6)]
        public virtual string SpecVersion { get; set; } = typeof(WorkflowDefinition).Assembly.GetName().Version!.ToString(2);

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/>
        /// </summary>
        [ProtoMember(7, Name = "dataInputSchema")]
        [DataMember(Order = 7, Name = "dataInputSchema")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "dataInputSchema"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<JSchema, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("dataInputSchema"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<JSchema, Uri>))]
        protected virtual OneOf<JSchema, Uri>? DataInputSchemaValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s data input schema
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual JSchema? DataInputSchema
        {
            get
            {
                return this.DataInputSchemaValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.DataInputSchemaValue = null;
                else
                    this.DataInputSchemaValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at the <see cref="WorkflowDefinition"/>'s input data schema
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? DataInputSchemaUri
        {
            get
            {
                return this.DataInputSchemaValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.DataInputSchemaValue = null;
                else
                    this.DataInputSchemaValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the language the <see cref="WorkflowDefinition"/>'s expressions are expressed in
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "expressionLang")]
        [System.Text.Json.Serialization.JsonPropertyName("expressionLang")]
        [YamlMember(Alias = "expressionLang")]
        [Required]
        [ProtoMember(8, Name = "expressionLang")]
        [DataMember(Order = 8, Name = "expressionLang")]
        public virtual string ExpressionLanguage { get; set; } = "jq";

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s annotations
        /// </summary>
        [ProtoMember(9)]
        [DataMember(Order = 9)]
        public virtual List<string> Annotations { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s execution timeouts
        /// </summary>
        [ProtoMember(10, Name = "timeouts")]
        [DataMember(Order = 10, Name = "timeouts")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "timeouts"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<WorkflowTimeoutDefinition, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("timeouts"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<WorkflowTimeoutDefinition, Uri>))]
        protected virtual OneOf<WorkflowTimeoutDefinition, Uri>? TimeoutsValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s execution timeouts
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual WorkflowTimeoutDefinition? Timeouts
        {
            get
            {
                return this.TimeoutsValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.TimeoutsValue = null;
                else
                    this.TimeoutsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at the <see cref="WorkflowDefinition"/>'s <see cref="WorkflowTimeoutDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? TimeoutsUri
        {
            get
            {
                return this.TimeoutsValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.TimeoutsValue = null;
                else
                    this.TimeoutsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that defines the <see cref="WorkflowDefinition"/>'s start
        /// </summary>
        [ProtoMember(11, Name = "start")]
        [DataMember(Order = 11, Name = "start")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "start"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<StartDefinition, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("start"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<StartDefinition, string>))]
        protected virtual OneOf<StartDefinition, string>? StartValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s <see cref="StartDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual StartDefinition? Start
        {
            get
            {
                return this.StartValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.StartValue = null;
                else
                    this.StartValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the name of the <see cref="WorkflowDefinition"/>'s start <see cref="StateDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual string? StartStateName
        {
            get
            {
                return this.StartValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.StartValue = null;
                else
                    this.StartValue = value;
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to keep instances of the <see cref="WorkflowDefinition"/> active even if there are no active execution paths. Instance can be terminated via 'terminate end definition' or reaching defined 'execTimeout'
        /// </summary>
        [ProtoMember(12)]
        [DataMember(Order = 12)]
        public virtual bool KeepActive { get; set; } = false;

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s metadata
        /// </summary>
        [ProtoMember(13)]
        [DataMember(Order = 13)]
        public virtual Any? Metadata { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/> collection
        /// </summary>
        [ProtoMember(14, Name = "events")]
        [DataMember(Order = 14, Name = "events")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "events"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<EventDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("events"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<EventDefinition>, Uri>))]
        protected virtual OneOf<List<EventDefinition>, Uri>? EventsValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual List<EventDefinition>? Events
        {
            get
            {
                return this.EventsValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.EventsValue = null;
                else
                    this.EventsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? EventsUri
        {
            get
            {
                return this.EventsValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.EventsValue = null;
                else
                    this.EventsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/> collection
        /// </summary>
        [ProtoMember(15, Name = "functions")]
        [DataMember(Order = 15, Name = "functions")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "functions"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<FunctionDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("functions"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<FunctionDefinition>, Uri>))]
        protected virtual OneOf<List<FunctionDefinition>, Uri>? FunctionsValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual List<FunctionDefinition>? Functions
        {
            get
            {
                return this.FunctionsValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.FunctionsValue = null;
                else
                    this.FunctionsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? FunctionsUri
        {
            get
            {
                return this.FunctionsValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.FunctionsValue = null;
                else
                    this.FunctionsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="RetryDefinition"/> collection
        /// </summary>
        [ProtoMember(16, Name = "retries")]
        [DataMember(Order = 16, Name = "retries")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "retries"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<RetryDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("retries"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<RetryDefinition>, Uri>))]
        protected virtual OneOf<List<RetryDefinition>, Uri>? RetriesValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="RetryDefinition"/>s
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual List<RetryDefinition>? Retries
        {
            get
            {
                return this.RetriesValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.RetriesValue = null;
                else
                    this.RetriesValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="RetryDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? RetriesUri
        {
            get
            {
                return this.RetriesValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.RetriesValue = null;
                else
                    this.RetriesValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s
        /// </summary>
        [ProtoMember(17)]
        [DataMember(Order = 17)]
        public virtual List<StateDefinition> States { get; set; } = new List<StateDefinition>();

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        [ProtoMember(18, Name = "constants")]
        [DataMember(Order = 18, Name = "constants")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "constants"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<Any, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("constants"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<Any, Uri>))]
        protected virtual OneOf<Any, Uri>? ConstantsValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Any? Constants
        {
            get
            {
                return this.ConstantsValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.ConstantsValue = null;
                else
                    this.ConstantsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? ConstantsUri
        {
            get
            {
                return this.ConstantsValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.ConstantsValue = null;
                else
                    this.ConstantsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [ProtoMember(19, Name = "secrets")]
        [DataMember(Order = 19, Name = "secrets")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "secrets"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<string>, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("secrets"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<string>, Uri>))]
        protected virtual OneOf<List<string>, Uri>? SecretsValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual List<string>? Secrets
        {
            get
            {
                return this.SecretsValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.SecretsValue = null;
                else
                    this.SecretsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? SecretsUri
        {
            get
            {
                return this.SecretsValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.SecretsValue = null;
                else
                    this.SecretsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s
        /// </summary>
        [ProtoMember(20, Name = "auth")]
        [DataMember(Order = 20, Name = "auth")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "auth"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<AuthenticationDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("auth"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<AuthenticationDefinition>, Uri>))]
        protected virtual OneOf<List<AuthenticationDefinition>, Uri>? AuthValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual List<AuthenticationDefinition>? Auth
        {
            get
            {
                return this.AuthValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.AuthValue = null;
                else
                    this.AuthValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? AuthUri
        {
            get
            {
                return this.AuthValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.AuthValue = null;
                else
                    this.AuthValue = value;
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not actions should automatically be retried on unchecked errors. Defaults to false.
        /// </summary>
        [ProtoMember(21)]
        [DataMember(Order = 21)]
        public virtual bool AutoRetries { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="ExtensionDefinition"/>s
        /// </summary>
        [ProtoMember(22, Name = "extensions")]
        [DataMember(Order = 22, Name = "extensions")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "extensions"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<ExtensionDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonPropertyName("extensions"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<ExtensionDefinition>, Uri>))]
        protected virtual OneOf<List<ExtensionDefinition>, Uri>? ExtensionsValue { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="ExtensionDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual List<ExtensionDefinition>? Extensions
        {
            get
            {
                return this.ExtensionsValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.ExtensionsValue = null;
                else
                    this.ExtensionsValue = value;
            }
        }

        /// <summary>
        /// Gets/sets an <see cref="Uri"/> pointing at a file containing the <see cref="WorkflowDefinition"/>'s <see cref="ExtensionDefinition"/> collection
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual Uri? ExtensionsUri
        {
            get
            {
                return this.ExtensionsValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.ExtensionsValue = null;
                else
                    this.ExtensionsValue = value;
            }
        }

        /// <summary>
        /// Gets the start <see cref="StateDefinition"/>
        /// </summary>
        /// <returns>The <see cref="StateDefinition"/> the <see cref="WorkflowDefinition"/> starts with</returns>
        public virtual StateDefinition GetStartState()
        {
            var stateName = this.StartStateName;
            if (this.Start != null)
                stateName = this.Start.StateName;
            if (string.IsNullOrWhiteSpace(stateName))
                return this.States.First();
            if (!this.TryGetState(stateName, out var state))
                throw new NullReferenceException($"Failed to find a state definition with name '{state}'");
            return state;
        }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to get</param>
        /// <returns>The <see cref="StateDefinition"/> with the specified name, if any</returns>
        public virtual StateDefinition? GetState(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.States?.FirstOrDefault(s => s.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to retrieve</param>
        /// <param name="state">The <see cref="StateDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="StateDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetState(string name, out StateDefinition state)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            state = this.GetState(name)!;
            return state != null;
        }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <typeparam name="TState">The expected type of the <see cref="StateDefinition"/> with the specified name</typeparam>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to get</param>
        /// <returns>The <see cref="StateDefinition"/> with the specified name, if any</returns>
        public virtual TState? GetState<TState>(string name)
            where TState : StateDefinition
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.GetState(name) as TState;
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <typeparam name="TState">The expected type of the <see cref="StateDefinition"/> with the specified name</typeparam>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to retrieve</param>
        /// <param name="state">The <see cref="StateDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="StateDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetState<TState>(string name, out TState state)
            where TState : StateDefinition
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            state = this.GetState<TState>(name)!;
            return state != null;
        }

        /// <summary>
        /// Gets the <see cref="EventDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to get</param>
        /// <returns>The <see cref="EventDefinition"/> with the specified name, if any</returns>
        public virtual EventDefinition? GetEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.Events?.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="EventDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to retrieve</param>
        /// <param name="e">The <see cref="EventDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="EventDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetEvent(string name, out EventDefinition e)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            e = this.GetEvent(name)!;
            return e != null;
        }

        /// <summary>
        /// Gets the <see cref="FunctionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="FunctionDefinition"/> to get</param>
        /// <returns>The <see cref="FunctionDefinition"/> with the specified name, if any</returns>
        public virtual FunctionDefinition? GetFunction(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.Functions?.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="FunctionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="FunctionDefinition"/> to retrieve</param>
        /// <param name="function">The <see cref="FunctionDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="FunctionDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetFunction(string name, out FunctionDefinition function)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            function = this.GetFunction(name)!;
            return function != null;
        }

        /// <summary>
        /// Gets the <see cref="AuthenticationDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to get</param>
        /// <returns>The <see cref="AuthenticationDefinition"/> with the specified name, if any</returns>
        public virtual AuthenticationDefinition? GetAuthentication(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            return this.Auth?.FirstOrDefault(e => e.Name == name);
        }

        /// <summary>
        /// Attempts to retrieve the <see cref="AuthenticationDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to retrieve</param>
        /// <param name="authentication">The <see cref="AuthenticationDefinition"/> with the specified name, if any</param>
        /// <returns>A boolean indicating whether or not a <see cref="AuthenticationDefinition"/> with the specified name could be found</returns>
        public virtual bool TryGetAuthentication(string name, out AuthenticationDefinition authentication)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            authentication = this.GetAuthentication(name)!;
            return authentication != null;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{ this.Id} {this.Version}";
        }

        /// <summary>
        /// Creates a new <see cref="IWorkflowBuilder"/> used to build a new <see cref="WorkflowDefinition"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="WorkflowDefinition"/> to create</param>
        /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to create</param>
        /// <param name="version">The version of the <see cref="WorkflowDefinition"/> to create</param>
        /// <returns>A new <see cref="IWorkflowBuilder"/></returns>
        public static IWorkflowBuilder Create(string id, string name, string version)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentNullException(nameof(version));
            return new WorkflowBuilder()
                .WithId(id)
                .WithName(name)
                .WithVersion(version);
        }

    }

}
