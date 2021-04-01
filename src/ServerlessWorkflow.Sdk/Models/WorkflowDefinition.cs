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
using ServerlessWorkflow.Sdk.Services.FluentBuilders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the definition of a Serverless Workflow
    /// </summary>
    public class WorkflowDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s unique identifier
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s name
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s version
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        public virtual string Version { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="System.Version"/> of the Serverless Workflow schema to use
        /// </summary>
        public virtual string SchemaVersion { get; set; } = "0.6";

        /// <summary>
        /// Gets/sets the language the <see cref="WorkflowDefinition"/>'s expressions are expressed in
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "expressionLang")]
        [System.Text.Json.Serialization.JsonPropertyName("expressionLang")]
        [YamlMember(Alias = "expressionLang")]
        [Required]
        public virtual string ExpressionLanguage { get; set; } = "jq";

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s annotations
        /// </summary>
        public virtual List<string> Annotations { get; set; } = new List<string>();

        /// <summary>
        /// Gets/sets the object used to configure the <see cref="WorkflowDefinition"/>'s execution timeout
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "execTimeout")]
        [System.Text.Json.Serialization.JsonPropertyName("execTimeout")]
        [YamlMember(Alias = "execTimeout")]
        public virtual ExecutionTimeoutDefinition ExecutionTimeout { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that defines the <see cref="WorkflowDefinition"/>'s start
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = nameof(Start))]
        [System.Text.Json.Serialization.JsonPropertyName(nameof(Start))]
        [YamlMember(Alias = nameof(Start))]
        protected virtual JToken StartToken { get; set; }

        private StartDefinition _Start;
        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s <see cref="StartDefinition"/>
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual StartDefinition Start
        {
            get
            {
                if (this._Start == null
                    && this.StartToken != null)
                {
                    if (this.StartToken.Type == JTokenType.String)
                        this._Start = new StartDefinition() { StateName = this.StartToken.ToObject<string>() };
                    else
                        this._Start = this.StartToken.ToObject<StartDefinition>();
                }
                return this._Start;
            }
            set
            {
                if (value == null)
                {
                    this._Start = null;
                    this.StartToken = null;
                    return;
                }
                this._Start = value;
                this.StartToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not to keep instances of the <see cref="WorkflowDefinition"/> active event if there are no active execution paths. Instance can be terminated via 'terminate end definition' or reaching defined 'execTimeout'
        /// </summary>
        public virtual bool KeepActive { get; set; } = false;

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s metadata
        /// </summary>
        public virtual JObject Metadata { get; set; } = new JObject();

        /// <summary>
        /// Gets/sets an <see cref="List{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        public virtual List<EventDefinition> Events { get; set; } = new List<EventDefinition>();

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="EventDefinition"/>s
        /// </summary>
        public virtual List<FunctionDefinition> Functions { get; set; } = new List<FunctionDefinition>();

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="RetryStrategyDefinition"/>s
        /// </summary>
        public virtual List<RetryStrategyDefinition> Retries { get; set; } = new List<RetryStrategyDefinition>();

        /// <summary>
        /// Gets/sets an <see cref="IEnumerable{T}"/> containing the <see cref="WorkflowDefinition"/>'s <see cref="StateDefinition"/>s
        /// </summary>
        public virtual List<StateDefinition> States { get; set; } = new List<StateDefinition>();

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
