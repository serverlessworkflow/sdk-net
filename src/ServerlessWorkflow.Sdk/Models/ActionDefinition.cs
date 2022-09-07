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
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the object used to define a workflow action
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ActionDefinition
    {

        /// <summary>
        /// Gets/sets the unique action definition name
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string? Name { get; set; }

        /// <summary>
        /// Gets the <see cref="ActionDefinition"/>'s type
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public virtual ActionType Type
        {
            get
            {
                if (this.Function != null)
                    return ActionType.Function;
                else if (this.Event != null)
                    return ActionType.Trigger;
                else if (this.Subflow != null)
                    return ActionType.Subflow;
                else
                    throw new InvalidOperationException("Failed to determine the action type");
            }
        }

        /// <summary>
        /// Gets/sets a <see cref="OneOf{T1, T2}"/> that represents the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "functionRef")]
        [System.Text.Json.Serialization.JsonPropertyName("functionRef")]
        [YamlMember(Alias = "functionRef")]
        [ProtoMember(2, Name = "functionRef")]
        [DataMember(Order = 2, Name = "functionRef")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<FunctionReference, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<FunctionReference, string>))]
        protected virtual OneOf<FunctionReference, string>? FunctionValue { get; set; }

        /// <summary>
        /// Gets the object used to configure the reference of the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual FunctionReference? Function
        {
            get
            {
                if (this.FunctionValue?.T1Value == null
                    && !string.IsNullOrWhiteSpace(this.FunctionValue?.T2Value))
                        return new FunctionReference() { RefName = this.FunctionValue.T2Value };
                    else
                        return this.FunctionValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.FunctionValue = null;
                else
                    this.FunctionValue = value;
            }
        }

        /// <summary>
        /// Gets the object used to configure the reference of the event to produce or consume
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "eventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("eventRef")]
        [YamlMember(Alias = "eventRef")]
        [ProtoMember(3, Name = "eventRef")]
        [DataMember(Order = 3, Name = "eventRef")]
        public virtual EventReference? Event { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="OneOf{T1, T2}"/> that references a subflow to run
        /// </summary>
        [YamlMember(Alias = "subFlowRef")]
        [ProtoMember(4, Name = "subFlowRef")]
        [DataMember(Order = 4, Name = "subFlowRef")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "subFlowRef"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<SubflowReference, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("subFlowRef"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<SubflowReference, string>))]
        protected virtual OneOf<SubflowReference, string>? SubflowValue { get; set; }

        /// <summary>
        /// Gets the object used to configure the reference of the subflow to run
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual SubflowReference? Subflow
        {
            get
            {
                if (this.SubflowValue?.T1Value == null
                    && !string.IsNullOrWhiteSpace(this.SubflowValue?.T2Value))
                {
                    var components = this.SubflowValue.T2Value.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    var id = components.First();
                    var version = null as string;
                    if (components.Length > 1)
                    {
                        version = components.Last();
                        id = this.SubflowValue.T2Value[..^(version.Length + 1)];
                    }
                    return new() { WorkflowId = id, Version = version };
                }
                return this.SubflowValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.SubflowValue = null;
                else
                    this.SubflowValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the name of the workflow retry definition to use. If not defined uses the default runtime retry definition
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual string? RetryRef { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing references to defined <see cref="ErrorHandlerDefinition"/>s for which the action should not be retried. Used only when `<see cref="WorkflowDefinition.AutoRetries"/>` is set to `true`
        /// </summary>
        [ProtoMember(6)]
        [DataMember(Order = 6)]
        public virtual List<string>? NonRetryableErrors { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing references to defined <see cref="ErrorHandlerDefinition"/>s for which the action should be retried. Used only when `<see cref="WorkflowDefinition.AutoRetries"/>` is set to `false`
        /// </summary>
        [ProtoMember(7)]
        [DataMember(Order = 7)]
        public virtual List<string>? RetryableErrors { get; set; }

        /// <summary>
        /// Gets/sets an object used to define the way to filter the action's data
        /// </summary>
        [ProtoMember(8)]
        [DataMember(Order = 8)]
        public ActionDataFilterDefinition? ActionDataFilter { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="ActionDefinition"/>'s execution delay configuration
        /// </summary>
        [ProtoMember(9)]
        [DataMember(Order = 9)]
        public virtual ActionExecutionDelayDefinition? Sleep { get; set; }

        /// <summary>
        /// Gets/sets an expression to be evaluated positively as a condition for the <see cref="ActionDefinition"/> to execute.
        /// </summary>
        [ProtoMember(10)]
        [DataMember(Order = 10)]
        public virtual string? Condition { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                return base.ToString();
            else
                return this.Name;
        }

    }

}