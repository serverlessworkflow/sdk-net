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
using YamlDotNet.Serialization;
using System;
using System.Linq;

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
        /// Gets the <see cref="ActionDefinition"/>'s type
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public ActionType Type
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
        /// Gets/sets the unique action definition name
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "functionRef")]
        [System.Text.Json.Serialization.JsonPropertyName("functionRef")]
        [YamlMember(Alias = "functionRef")]
        [ProtoMember(2, Name = "functionRef")]
        [DataMember(Order = 2, Name = "functionRef")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<FunctionReference, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<FunctionReference, string>))]
        protected virtual OneOf<FunctionReference, string> FunctionToken { get; set; }

        private FunctionReference _Function;
        /// <summary>
        /// Gets the object used to configure the reference of the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public FunctionReference Function
        {
            get
            {
                if (this._Function == null
                    && this.FunctionToken != null)
                {
                    if (this.FunctionToken.Value1 == null)
                        this._Function = new FunctionReference() { RefName = this.FunctionToken.Value2 };
                    else
                        this._Function = this.FunctionToken.Value1;
                }
                  
                return this._Function;
            }
            set
            {
                this._Function = value;
                if (this._Function == null)
                    this.FunctionToken = null;
                else
                    this.FunctionToken = new(this._Function);
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
        public EventReference Event { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that references a subflow to run
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "subflowRef")]
        [System.Text.Json.Serialization.JsonPropertyName("subflowRef")]
        [YamlMember(Alias = "subflowRef")]
        [ProtoMember(4, Name = "subflowRef")]
        [DataMember(Order = 4, Name = "subflowRef")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<SubflowReference, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<SubflowReference, string>))]
        protected virtual OneOf<SubflowReference, string> SubflowToken { get; set; }

        private SubflowReference _Subflow;
        /// <summary>
        /// Gets the object used to configure the reference of the subflow to run
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public SubflowReference Subflow
        {
            get
            {
                if (this._Subflow == null
                    && this.SubflowToken != null)
                {
                    if (this.SubflowToken.Value1 == null)
                    {
                        var components = this.SubflowToken.Value2.Split(':', StringSplitOptions.RemoveEmptyEntries);
                        var id = components.First();
                        var version = (string)null;
                        if(components.Length > 1)
                        {
                            version = components.Last();
                            id = this.SubflowToken.Value2[..^(version.Length + 1)];
                        }
                        this._Subflow = new() { WorkflowId = id, Version = version };
                    }
                    else
                        this._Subflow = this.SubflowToken.Value1;
                }
                return this._Subflow;
            }
            set
            {
                this._Subflow = value;
                if (this._Subflow == null)
                    this.SubflowToken = null;
                else
                    this.SubflowToken = new(this._Subflow);
            }
        }

        /// <summary>
        /// Gets/sets the time period to wait for function execution to complete
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        public virtual TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets an object used to define the way to filter the action's data
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "actionDataFilter")]
        [System.Text.Json.Serialization.JsonPropertyName("actionDataFilter")]
        [YamlMember(Alias = "actionDataFilter")]
        [ProtoMember(6, Name = "actionDataFilter")]
        [DataMember(Order = 6, Name = "actionDataFilter")]
        public ActionDataFilterDefinition DataFilter { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}