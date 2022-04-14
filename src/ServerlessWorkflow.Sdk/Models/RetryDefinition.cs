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
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object that defines workflow states retry policy strategy. This is an explicit definition and can be reused across multiple defined workflow state errors.
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class RetryDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="RetryDefinition"/>'s name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Name { get; set; } = null!;

        /// <summary>
        /// Gets/sets delay between retry attempts
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601NullableTimeSpanConverter))]
        public virtual TimeSpan? Delay { get; set; }

        /// <summary>
        /// Gets/sets the maximum amount of retries allowed
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual uint? MaxAttempts { get; set; }

        /// <summary>
        /// Gets/sets the maximum delay between retries
        /// </summary>
        [ProtoMember(4)]
        [DataMember(Order = 4)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601NullableTimeSpanConverter))]
        public virtual TimeSpan? MaxDelay { get; set; }

        /// <summary>
        /// Gets/sets the duration which will be added to the delay between successive retries
        /// </summary>
        [ProtoMember(5)]
        [DataMember(Order = 5)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601NullableTimeSpanConverter))]
        public virtual TimeSpan? Increment { get; set; }

        /// <summary>
        /// Gets/sets a value by which the delay is multiplied before each attempt. For example: "1.2" meaning that each successive delay is 20% longer than the previous delay. 
        /// For example, if delay is 'PT10S', then the delay between the first and second attempts will be 10 seconds, and the delay before the third attempt will be 12 seconds.
        /// </summary>
        [ProtoMember(6)]
        [DataMember(Order = 6)]
        public virtual float? Multiplier { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="RetryDefinition"/>'s jitter.<para></para>
        /// If float type, maximum amount of random time added or subtracted from the delay between each retry relative to total delay (between 0.0 and 1.0).<para></para>
        /// If string type, absolute maximum amount of random time added or subtracted from the delay between each retry (ISO 8601 duration format)
        /// </summary>
        [ProtoMember(7, Name = "jitter")]
        [DataMember(Order = 7, Name = "jitter")]
        [YamlMember(Alias = "jitter")]
        [Newtonsoft.Json.JsonProperty(PropertyName = "jitter"), Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<float?, string>))]
        [System.Text.Json.Serialization.JsonPropertyName("jitter"), System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<float?, string>))]
        protected virtual OneOf<float?, string>? JitterValue { get; set; }

        /// <summary>
        /// Gets/sets the maximum amount of random time added or subtracted from the delay between each retry relative to total delay (between 0.0 and 1.0)
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual float? JitterMultiplier
        {
            get
            {
                return this.JitterValue?.T1Value;
            }
            set
            {
                if (value == null)
                    this.JitterValue = null;
                else
                    this.JitterValue = value;
            }
        }

        /// <summary>
        /// Gets/sets the absolute maximum amount of random time added or subtracted from the delay between each retry
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual TimeSpan? JitterDuration
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.JitterValue?.T2Value))
                    return null;
                return Iso8601TimeSpan.Parse(this.JitterValue.T2Value);
            }
            set
            {
                if (value == null)
                    this.JitterValue = null;
                else
                    this.JitterValue = Iso8601TimeSpan.Format(value.Value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
