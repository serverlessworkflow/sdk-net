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

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents an object used to define the time/repeating intervals at which workflow instances can/should be started 
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class ScheduleDefinition
    {

        /// <summary>
        /// Gets/sets the time interval (ISO 8601 format) describing when workflow instances can be created.
        /// </summary>
        [ProtoMember(1)]
        [DataMember(Order = 1)]
        public virtual string Interval { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="JToken"/> that represents the CRON expression that defines when the workflow instance should be created
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "cron")]
        [System.Text.Json.Serialization.JsonPropertyName("cron")]
        [YamlMember(Alias = "cron")]
        [ProtoMember(2, Name = "cron")]
        [DataMember(Order = 2, Name = "cron")]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.OneOfConverter<CronDefinition, string>))]
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.Converters.OneOfConverter<CronDefinition, string>))]
        public virtual OneOf<CronDefinition, string> CronToken { get; set; }

        private CronDefinition _Cron;
        /// <summary>
        /// Gets/sets a CRON expression that defines when the workflow instance should be created
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        [YamlIgnore]
        [ProtoIgnore]
        [IgnoreDataMember]
        public virtual CronDefinition Cron
        {
            get
            {
                if (this._Cron == null
                   && this.CronToken != null)
                {
                    if (this.CronToken.T1Value == null)
                        this._Cron = new() { Expression = this.CronToken.T2Value };
                    else
                        this._Cron = this.CronToken.T1Value;
                }
                return this._Cron;
            }
            set
            {
                if (value == null)
                {
                    this._Cron = null;
                    this.CronToken = null;
                    return;
                }
                this._Cron = value;
                this.CronToken = new(value);
            }
        }

        /// <summary>
        /// Gets/sets the timezone name used to evaluate the cron expression. Not used for interval as timezone can be specified there directly. If not specified, should default to local machine timezone.
        /// </summary>
        [ProtoMember(3)]
        [DataMember(Order = 3)]
        public virtual string Timezone { get; set; }

    }

}
