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
using System.Globalization;
using System.Xml;
using YamlDotNet.Serialization;
namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents an object that defines workflow states retry policy strategy. This is an explicit definition and can be reused across multiple defined workflow state errors.
    /// </summary>
    public class RetryStrategyDefinition
    {

        /// <summary>
        /// Gets/sets the <see cref="RetryStrategyDefinition"/>'s name
        /// </summary>
        [Required]
        [Newtonsoft.Json.JsonRequired]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets/sets delay between retry attempts
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? Delay { get; set; }

        /// <summary>
        /// Gets/sets the maximum amount of retries allowed
        /// </summary>
        public virtual uint? MaxAttempts { get; set; }

        /// <summary>
        /// Gets/sets the maximum delay between retries
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? MaxDelay { get; set; }

        /// <summary>
        /// Gets/sets the duration which will be added to the delay between successive retries
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.Iso8601TimeSpanConverter))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.Iso8601TimeSpanConverter))]
        public virtual TimeSpan? Increment { get; set; }

        /// <summary>
        /// Gets/sets a value by which the delay is multiplied before each attempt. For example: "1.2" meaning that each successive delay is 20% longer than the previous delay. 
        /// For example, if delay is 'PT10S', then the delay between the first and second attempts will be 10 seconds, and the delay before the third attempt will be 12 seconds.
        /// </summary>
        public virtual float? Multiplier { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="JToken"/> that represents the <see cref="RetryStrategyDefinition"/>'s jitter.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "jitter")]
        [System.Text.Json.Serialization.JsonPropertyName("jitter")]
        [YamlMember(Alias = "jitter")]
        protected virtual JToken JitterToken { get; set; }

        /// <summary>
        /// Gets/sets the maximum amount of random time added or subtracted from the delay between each retry relative to total delay
        /// </summary>
        public virtual float? JitterMultiplier
        {
            get
            {
                if (this.JitterToken?.Type != JTokenType.Float
                    || (this.JitterToken?.Type == JTokenType.String && !float.TryParse(this.JitterToken.Value<string>(), NumberStyles.Float, CultureInfo.InvariantCulture, out _)))
                    return null;
                return this.JitterToken.ToObject<float>();
            }
            set
            {
                if (value == null)
                {
                    this.JitterToken = null;
                    return;
                }
                this.JitterToken = JToken.FromObject(value);
            }
        }

        /// <summary>
        /// Gets/sets the absolute maximum amount of random time added or subtracted from the delay between each retry
        /// </summary>
        public virtual TimeSpan? JitterDuration
        {
            get
            {
                if (this.JitterToken?.Type != JTokenType.String
                    || float.TryParse(this.JitterToken.Value<string>(), NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                    return null;
                return XmlConvert.ToTimeSpan(this.JitterToken.ToString());
            }
            set
            {
                if (value == null)
                {
                    this.JitterToken = null;
                    return;
                }
                this.JitterToken = XmlConvert.ToString(value.Value);
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.Name;
        }

    }

}
