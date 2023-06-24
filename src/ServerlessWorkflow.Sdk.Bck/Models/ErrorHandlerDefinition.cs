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
using System.ComponentModel.DataAnnotations;
using YamlDotNet.Serialization;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents the definition of a workflow error handler
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ErrorHandlerDefinition
    {

        /// <summary>
        /// Gets/sets a domain-specific error name, or '*' to indicate all possible errors. If other handlers are declared, the <see cref="ErrorHandlerDefinition"/> will only be considered on errors that have NOT been handled by any other.
        /// </summary>
        [Newtonsoft.Json.JsonRequired]
        [Required]
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Error { get; set; } = null!;

        /// <summary>
        /// Gets/sets the error code. Can be used in addition to the name to help runtimes resolve to technical errors/exceptions. Should not be defined if error is set to '*'.
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string? Code { get; set; }

        /// <summary>
        /// Gets/sets a reference to the <see cref="RetryDefinition"/> to use 
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "retryRef")]
        [System.Text.Json.Serialization.JsonPropertyName("retryRef")]
        [YamlMember(Alias = "retryRef")]
        [ProtoMember(3, Name = "retryRef")]
        [DataMember(Order = 3, Name = "retryRef")]
        public virtual string? Retry { get; set; } = null!;

        /// <summary>
        /// Gets/sets the object that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [ProtoMember(4, Name = "transition")]
        [DataMember(Order = 4, Name = "transition")]
        [YamlMember(Alias = "transition")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "transition"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("transition"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
        protected virtual OneOf<TransitionDefinition, string>? TransitionValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual TransitionDefinition? Transition
        {
            get
            {
                if (this.TransitionValue?.T1Value == null
                    && !string.IsNullOrWhiteSpace(this.TransitionValue?.T2Value))
                    return new() { NextState = this.TransitionValue.T2Value };
                else
                    return this.TransitionValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.TransitionValue = null;
                else
                    this.TransitionValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the name of the state definition to transition to upon completion
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual string? TransitionToStateName
        {
            get
            {
                return this.TransitionValue?.T2Value;
            }
            set
            {
                if (value == null)
                    this.TransitionValue = null;
                else
                    this.TransitionValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the object that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [ProtoMember(5, Name = "end")]
        [DataMember(Order = 5, Name = "end")]
        [YamlMember(Alias = "end")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "end"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
        [System.Text.Json.Serialization.JsonPropertyName("end"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
        protected virtual OneOf<EndDefinition, bool>? EndValue { get; set; }

        /// <summary>
        /// Gets/sets the object used to configure the state definition's transition to another state definition upon completion
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual EndDefinition? End
        {
            get
            {
                if (this.EndValue?.T1Value == null
                    && !string.IsNullOrWhiteSpace(this.TransitionValue?.T2Value))
                    return new() { };
                else
                    return this.EndValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.EndValue = null;
                else
                    this.EndValue = value;
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the state definition is the end of a logicial workflow path
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual bool IsEnd
        {
            get
            {
                if (this.EndValue == null)
                    return false;
                else
                    return this.EndValue.T2Value;
            }
            set
            {
                this.EndValue = value;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Error}{(string.IsNullOrWhiteSpace(this.Code) ? string.Empty : $" (code: '{this.Code}')")}";
        }

    }

}
