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
        public virtual string Error { get; set; }

        /// <summary>
        /// Gets/sets the error code. Can be used in addition to the name to help runtimes resolve to technical errors/exceptions. Should not be defined if error is set to '*'.
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual string Code { get; set; }

        /// <summary>
        /// Gets/sets a reference to the <see cref="RetryStrategyDefinition"/> to use 
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "retryRef")]
        [System.Text.Json.Serialization.JsonPropertyName("retryRef")]
        [YamlMember(Alias = "retryRef")]
        [ProtoMember(3, Name = "retryRef")]
        [DataMember(Order = 3, Name = "retryRef")]
        public virtual string Retry { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="TransitionDefinition"/>
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<TransitionDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<TransitionDefinition, string>))]
        public virtual OneOf<TransitionDefinition, string> Transition { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="ErrorHandlerDefinition"/>'s <see cref="EndDefinition"/>
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<EndDefinition, bool>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<EndDefinition, bool>))]
        public virtual OneOf<EndDefinition, bool> End { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{this.Error}{(string.IsNullOrWhiteSpace(this.Code) ? string.Empty : $" (code: '{this.Code}')")}";
        }

    }

}
