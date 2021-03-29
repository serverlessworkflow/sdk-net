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
namespace ServerlessWorkflow.Sdk.Models
{
    /// <summary>
    /// Represents an object used to define the time/repeating intervals at which workflow instances can/should be started 
    /// </summary>
    public class ScheduleDefinition
    {

        /// <summary>
        /// Gets/sets the time interval (ISO 8601 format) describing when workflow instances can be created.
        /// </summary>
        public virtual string Interval { get; set; }

        /// <summary>
        /// Gets/sets a CRON expression that defines when the workflow instance should be created
        /// </summary>
        public virtual CronDefinition Cron { get; set; }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not workflow instances can be created outside of the defined interval/cron. Defaults to false.
        /// </summary>
        public virtual bool DirectInvoke { get; set; } = false;

        /// <summary>
        /// Gets/sets the timezone name used to evaluate the cron expression. Not used for interval as timezone can be specified there directly. If not specified, should default to local machine timezone.
        /// </summary>
        public virtual string Timezone { get; set; }

    }

}
