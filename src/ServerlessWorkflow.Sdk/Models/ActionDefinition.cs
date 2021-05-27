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

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the object used to define a workflow action
    /// </summary>
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
                else
                    return ActionType.Trigger;
            }
        }

        /// <summary>
        /// Gets/sets the unique action definition name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "functionRef")]
        [System.Text.Json.Serialization.JsonPropertyName("functionRef")]
        [YamlMember(Alias = "functionRef")]
        protected virtual JToken FunctionToken { get; set; }

        private FunctionReference _Function;
        /// <summary>
        /// Gets the object used to configure the reference of the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public FunctionReference Function
        {
            get
            {
                if (this._Function == null
                    && this.FunctionToken?.Type == JTokenType.Object)
                    this._Function = this.FunctionToken.ToObject<FunctionReference>();
                return this._Function;
            }
            set
            {
                this._Function = value;
                if (this._Function == null)
                    this.FunctionToken = null;
                else
                    this.FunctionToken = JToken.FromObject(this._Function);
            }
        }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that references a 'trigger' and 'result' reusable event definitions
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "eventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("eventRef")]
        [YamlMember(Alias = "eventRef")]
        protected virtual JToken EventToken { get; set; }

        private EventReference _Event;
        /// <summary>
        /// Gets the object used to configure the reference of the event to produce or consume
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public EventReference Event
        {
            get
            {
                if (this._Event == null
                    && this.EventToken?.Type == JTokenType.Object)
                    this._Event = this.EventToken.ToObject<EventReference>();
                return this._Event;
            }
            set
            {
                this._Event = value;
                if (this._Event == null)
                    this.EventToken = null;
                else
                    this.EventToken = JToken.FromObject(this._Event);
            }
        }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that references a subflow to run
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "subflowRef")]
        [System.Text.Json.Serialization.JsonPropertyName("subflowRef")]
        [YamlMember(Alias = "subflowRef")]
        protected virtual JToken SubflowToken { get; set; }

        private SubflowReference _Subflow;
        /// <summary>
        /// Gets the object used to configure the reference of the subflow to run
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public SubflowReference Subflow
        {
            get
            {
                if (this._Subflow == null
                    && this.SubflowToken != null)
                {
                    if (this.SubflowToken?.Type == JTokenType.Object)
                        this._Subflow = this.SubflowToken.ToObject<SubflowReference>();
                    else if (this.SubflowToken?.Type == JTokenType.String)
                        this._Subflow = SubflowReference.Parse(this.SubflowToken.ToObject<string>());
                }
                return this._Subflow;
            }
            set
            {
                this._Subflow = value;
                if (this._Subflow == null)
                    this.SubflowToken = null;
                else
                    this.SubflowToken = JToken.FromObject(this._Subflow);
            }
        }

        /// <summary>
        /// Gets/sets the time period to wait for function execution to complete
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Gets/sets an object used to define the way to filter the action's data
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "actionDataFilter")]
        [System.Text.Json.Serialization.JsonPropertyName("actionDataFilter")]
        [YamlMember(Alias = "actionDataFilter")]
        public ActionDataFilterDefinition DataFilter { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}