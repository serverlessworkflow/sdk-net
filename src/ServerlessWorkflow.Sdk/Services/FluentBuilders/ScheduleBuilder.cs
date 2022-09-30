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
using Cronos;
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IScheduleBuilder"/> interface
    /// </summary>
    public class ScheduleBuilder
        : IScheduleBuilder
    {

        /// <summary>
        /// Gets the <see cref="ScheduleDefinition"/> to build
        /// </summary>
        protected ScheduleDefinition Schedule { get; } = new();

        /// <inheritdoc/>
        public virtual IScheduleBuilder AtInterval(TimeSpan interval)
        {
            this.Schedule.Interval = interval;
            this.Schedule.CronExpression = null;
            this.Schedule.Cron = null;
            return this;
        }

        /// <inheritdoc/>
        public virtual IScheduleBuilder Every(string cronExpression, DateTime? validUntil = null)
        {
            if (string.IsNullOrWhiteSpace(cronExpression)) throw new ArgumentNullException(nameof(cronExpression));
            if (!Cron.TryParse(cronExpression, out _)) throw new ArgumentException($"The specified value '{cronExpression}' is not a valid CRON expression");
            if (validUntil.HasValue)
                this.Schedule.Cron = new CronDefinition() { Expression = cronExpression, ValidUntil = validUntil.Value };
            else
                this.Schedule.CronExpression = cronExpression;
            this.Schedule.Interval = null;
            return this;
        }

        /// <inheritdoc/>
        public virtual IScheduleBuilder UseTimezone(string? timezone)
        {
            this.Schedule.Timezone = timezone;
            return this;
        }

        /// <inheritdoc/>
        public virtual ScheduleDefinition Build()
        {
            return this.Schedule;
        }

    }

}
