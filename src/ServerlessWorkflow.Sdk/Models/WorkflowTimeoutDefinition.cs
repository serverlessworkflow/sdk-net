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
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents the object used to configure a <see cref="WorkflowDefinition"/>'s <see href="https://github.com/serverlessworkflow/specification/blob/main/specification.md#Workflow-Timeouts">timeout</see>
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class WorkflowTimeoutDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s execution timeout
        /// </summary>
        [YamlMember(Alias = "workflowExecTimeout")]
        [ProtoMember(1, Name = "workflowExecTimeout")]
        [DataMember(Order = 1, Name = "workflowExecTimeout")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "workflowExecTimeout"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<WorkflowExecutionTimeoutDefinition, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("workflowExecTimeout"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<WorkflowExecutionTimeoutDefinition, string>))]
        protected virtual OneOf<WorkflowExecutionTimeoutDefinition, string>? WorkflowExecutionTimeoutValue { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="WorkflowDefinition"/>'s execution timeout
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual WorkflowExecutionTimeoutDefinition? WorkflowExecutionTimeout
        {
            get
            {
                return this.WorkflowExecutionTimeoutValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.WorkflowExecutionTimeoutValue = null;
                else
                    this.WorkflowExecutionTimeoutValue = value;
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
        public virtual TimeSpan? WorkflowExecutionTimeoutDuration
        {
            get
            {
                if (this.WorkflowExecutionTimeoutValue?.T1Value == null
                    && !string.IsNullOrWhiteSpace(this.WorkflowExecutionTimeoutValue?.T2Value))
                    return Iso8601TimeSpan.Parse(this.WorkflowExecutionTimeoutValue?.T2Value);
                else
                    return this.WorkflowExecutionTimeoutValue?.T1Value?.Duration;
            }
            set
            {
                if (value == null)
                    this.WorkflowExecutionTimeoutValue = null;
                else
                    this.WorkflowExecutionTimeoutValue = Iso8601TimeSpan.Format(value.Value);
            }
        }

        /// <summary>
        /// Gets/sets the duration after which to timeout states by default
        /// </summary>
        [YamlMember(Alias = "stateExecTimeout")]
        [ProtoMember(2, Name = "stateExecTimeout")]
        [DataMember(Order = 2, Name = "stateExecTimeout")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "stateExecTimeout"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("stateExecTimeout"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public TimeSpan? StateExecutionTimeout { get; set; }

        /// <summary>
        /// Gets/sets the duration after which to timeout actions by default
        /// </summary>
        [YamlMember(Alias = "actionExecTimeout")]
        [ProtoMember(2, Name = "actionExecTimeout")]
        [DataMember(Order = 2, Name = "actionExecTimeout")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "actionExecTimeout"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("actionExecTimeout"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public TimeSpan? ActionExecutionTimeout { get; set; }

        /// <summary>
        /// Gets/sets the duration after which to timeout branches by default
        /// </summary>
        [YamlMember(Alias = "branchExecTimeout")]
        [ProtoMember(2, Name = "branchExecTimeout")]
        [DataMember(Order = 2, Name = "branchExecTimeout")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "branchExecTimeout"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("branchExecTimeout"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public TimeSpan? BranchExecutionTimeout { get; set; }

        /// <summary>
        /// Gets/sets the duration after which to timeout events by default
        /// </summary>
        [YamlMember(Alias = "eventTimeout")]
        [ProtoMember(2, Name = "eventTimeout")]
        [DataMember(Order = 2, Name = "eventTimeout")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "eventTimeout"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonPropertyName("eventTimeout"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public TimeSpan? EventTimeout { get; set; }

    }

}
