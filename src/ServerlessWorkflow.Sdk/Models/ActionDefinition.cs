/*
 * Copyright 2020-Present The Serverless Workflow Specification Authors
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
using System.ComponentModel.DataAnnotations;

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
                    return ActionType.InvokeFunction;
                else
                    return ActionType.PublishEvent;
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
                if(value != null)
                {
                    this._Function = value;
                    this.FunctionToken = JToken.FromObject(value);
                }
            }
        }

        /// <summary>
        /// Gets the reference of the function to invoke
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        public string FunctionReference
        {
            get
            {
                if (this.Function != null)
                    return this.Function.Name;
                else if (this.FunctionReference != null
                    && this.FunctionToken.Type == JTokenType.String)
                    return this.FunctionToken.ToObject<string>();
                else
                    return null;
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
                if (value != null)
                {
                    this._Event = value;
                    this.EventToken = JToken.FromObject(value);
                }
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

    }

    /// <summary>
    /// Represents a reference to an <see cref="EventDefinition"/>
    /// </summary>
    public class EventReference
    {

        /// <summary>
        /// Gets the name of the 'produced' event that triggers the action
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "triggerEventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("triggerEventRef")]
        [YamlMember(Alias = "triggerEventRef")]
        public virtual string TriggerEvent { get; set; }

        /// <summary>
        /// Gets the name of the 'consumed' event that triggers the action
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired, Newtonsoft.Json.JsonProperty(PropertyName = "resultEventRef")]
        [System.Text.Json.Serialization.JsonPropertyName("resultEventRef")]
        [YamlMember(Alias = "resultEventRef")]
        public virtual string ResultEvent { get; set; }

        /// <summary>
        /// Gets/sets the data to become the cloud event's payload. 
        /// If string type, an expression which selects parts of the states data output to become the data (payload) of the event referenced by 'triggerEventRef'. 
        /// If object type, a custom object to become the data (payload) of the event referenced by 'triggerEventRef'.
        /// </summary>
        public virtual JToken Data { get; set; }

        /// <summary>
        /// Gets/sets additional extension context attributes to the produced event
        /// </summary>
        public virtual JObject ContextAttributes { get; set; }

    }

}