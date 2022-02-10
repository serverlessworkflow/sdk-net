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
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s domain-specific workflow identifier
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string Key { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s name
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s description
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s version
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="System.Version"/> of the Serverless Workflow schema to use
        /// </summary>
        [ProtoMember(6)]
        [DataMember(Order = 6)]
        public virtual string SpecVersion { get; set; } = typeof(WorkflowDefinition).Assembly.GetName().Version.ToString(2);

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "dataInputSchema")]
        [System.Text.Json.Serialization.JsonPropertyName("dataInputSchema")]
        [YamlMember(Alias = "dataInputSchema")]
        [ProtoMember(7, Name = "dataInputSchema")]
        [DataMember(Order = 7, Name = "dataInputSchema")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<JSchema, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<JSchema, Uri>))]
        protected virtual OneOf<JSchema, Uri> DataInputSchemaToken { get; set; }

        private JSchema _DataInputSchema;
        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [IgnoreDataMember]
        public virtual JSchema DataInputSchema
        {
            get
            {
                if (this._DataInputSchema == null
                    && this.DataInputSchemaToken != null)
                {
                    if (this.DataInputSchemaToken.T2Value != null)
                        this._DataInputSchema = new ExternalJSchema(this.DataInputSchemaToken.T2Value);
                    else
                        this._DataInputSchema = this.DataInputSchemaToken.T1Value;
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
        /// Gets/sets the <see cref="Uri"/> to the file that defines the <see cref="WorkflowDefinition"/>'s data input <see cref="JSchema"/> 
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [IgnoreDataMember]
        public virtual Uri DataInputSchemaUri
        {
            get
            {
                if (this.DataInputSchema == null
                    || this._DataInputSchema is not ExternalJSchema externalSchema)
                    return null;
                return externalSchema.DefinitionUri;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this._DataInputSchema = new ExternalJSchema(value);
                this.DataInputSchemaToken = new(value);
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
        [ProtoMember(10)]
        [DataMember(Order = 10)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<WorkflowTimeoutDefinition, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<WorkflowTimeoutDefinition, Uri>))]
        public virtual OneOf<WorkflowTimeoutDefinition, Uri> Timeouts { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that defines the <see cref="WorkflowDefinition"/>'s start
        /// </summary>
        [ProtoMember(11)]
        [DataMember(Order = 11)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<StartDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<StartDefinition, string>))]
        public virtual OneOf<StartDefinition, string> Start { get; set; }

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
        public virtual Any Metadata { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/> collection
        /// </summary>
        [ProtoMember(14)]
        [DataMember(Order = 14)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<EventDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<EventDefinition>, Uri>))]
        protected virtual OneOf<List<EventDefinition>, Uri> Events { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="FunctionDefinition"/> collection
        /// </summary>
        [ProtoMember(15)]
        [DataMember(Order = 15)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<FunctionDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<FunctionDefinition>, Uri>))]
        protected virtual OneOf<List<FunctionDefinition>, Uri> Functions { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="RetryStrategyDefinition"/> collection
        /// </summary>
        [ProtoMember(16)]
        [DataMember(Order = 16)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<RetryStrategyDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<RetryStrategyDefinition>, Uri>))]
        protected virtual OneOf<List<RetryStrategyDefinition>, Uri> Retries { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s
        /// </summary>
        [ProtoMember(17)]
        [DataMember(Order = 17)]
        public virtual List<StateDefinition> States { get; set; } = new List<StateDefinition>();

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s constants
        /// </summary>
        [ProtoMember(18)]
        [DataMember(Order = 18)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<Uri, Any>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<Uri, Any>))]
        public virtual OneOf<Uri, Any> Constants { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s secrets
        /// </summary>
        [ProtoMember(19)]
        [DataMember(Order = 19)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<string>, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<string>, Uri>))]
        public virtual OneOf<List<string>, Uri> Secrets { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="AuthenticationDefinition"/>s
        /// </summary>
        [ProtoMember(20)]
        [DataMember(Order = 20)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<AuthenticationDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<AuthenticationDefinition>, Uri>))]
        public virtual OneOf<List<AuthenticationDefinition>, Uri> Auth { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not actions should automatically be retried on unchecked errors. Defaults to false.
        /// </summary>
        [ProtoMember(21)]
        [DataMember(Order = 21)]
        public virtual bool AutoRetries { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="OneOf{T1, T2}"/> that represents the <see cref="WorkflowDefinition"/>'s <see cref="ExtensionDefinition"/>s
        /// </summary>
        [ProtoMember(22)]
        [DataMember(Order = 22)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<List<ExtensionDefinition>, Uri>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<List<ExtensionDefinition>, Uri>))]
        public virtual OneOf<List<ExtensionDefinition>, Uri> Extensions { get; set; }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to get</param>
        /// <returns>The <see cref="StateDefinition"/> with the specified name, if any</returns>
        public virtual StateDefinition GetState(string name)
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
            state = this.GetState(name);
            return state != null;
        }

        /// <summary>
        /// Gets the <see cref="StateDefinition"/> with the specified name
        /// </summary>
        /// <typeparam name="TState">The expected type of the <see cref="StateDefinition"/> with the specified name</typeparam>
        /// <param name="name">The name of the <see cref="StateDefinition"/> to get</param>
        /// <returns>The <see cref="StateDefinition"/> with the specified name, if any</returns>
        public virtual TState GetState<TState>(string name)
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
            state = this.GetState<TState>(name);
            return state != null;
        }

        /// <summary>
        /// Gets the <see cref="EventDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to get</param>
        /// <returns>The <see cref="EventDefinition"/> with the specified name, if any</returns>
        public virtual EventDefinition GetEvent(string name)
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
            e = this.GetEvent(name);
            return e != null;
        }

        /// <summary>
        /// Gets the <see cref="FunctionDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="FunctionDefinition"/> to get</param>
        /// <returns>The <see cref="FunctionDefinition"/> with the specified name, if any</returns>
        public virtual FunctionDefinition GetFunction(string name)
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
            function = this.GetFunction(name);
            return function != null;
        }

        /// <summary>
        /// Gets the <see cref="AuthenticationDefinition"/> with the specified name
        /// </summary>
        /// <param name="name">The name of the <see cref="AuthenticationDefinition"/> to get</param>
        /// <returns>The <see cref="AuthenticationDefinition"/> with the specified name, if any</returns>
        public virtual AuthenticationDefinition GetAuthentication(string name)
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
            authentication = this.GetAuthentication(name);
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
