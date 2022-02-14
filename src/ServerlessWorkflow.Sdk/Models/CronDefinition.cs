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
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServerlessWorkflow.Sdk.Models
{

    /// <summary>
    /// Represents a CRON expression definition
    /// </summary>
    [ProtoContract]
    [DataContract]
    public class CronDefinition
    {

        /// <summary>
        /// Gets/sets the repeating interval (cron expression) describing when the workflow instance should be created
        /// </summary>
        [Required]
        [JsonRequired]
        [ProtoMember(1, IsRequired = true)]
        [DataMember(Order = 1, IsRequired = true)]
        public virtual string Expression { get; set; } = null!;

        /// <summary>
        /// Gets/sets the date and time when the cron expression invocation is no longer valid
        /// </summary>
        [ProtoMember(2)]
        [DataMember(Order = 2)]
        public virtual DateTime? ValidUntil { get; set; }

    }

}
