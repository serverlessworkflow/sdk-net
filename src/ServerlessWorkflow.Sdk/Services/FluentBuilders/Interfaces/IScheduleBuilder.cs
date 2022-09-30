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
using ServerlessWorkflow.Sdk.Models;
using System;

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders
{

    /// <summary>
    /// Defines the fundamentals of a service used to build a <see cref="ScheduleDefinition"/>
    /// </summary>
    public interface IScheduleBuilder
    {

        /// <summary>
        /// Configures the <see cref="ScheduleDefinition"/> to execute at the specified interval
        /// </summary>
        /// <param name="interval">The interval at which to execute the <see cref="ScheduleDefinition"/></param>
        /// <returns>The configured <see cref="IScheduleBuilder"/></returns>
        IScheduleBuilder AtInterval(TimeSpan interval);

        /// <summary>
        /// Configures the <see cref="ScheduleDefinition"/> to execute at a frequency defined by the specified CRON expression
        /// </summary>
        /// <param name="cronExpression">A CRON expression that defines the frequency at which to execute the <see cref="ScheduleDefinition"/></param>
        /// <param name="validUntil">The date and time when the cron expression invocation is no longer valid</param>
        /// <returns>The configured <see cref="IScheduleBuilder"/></returns>
        IScheduleBuilder Every(string cronExpression, DateTime? validUntil = null);

        /// <summary>
        /// Configures the <see cref="ScheduleDefinition"/> to use the specified timezone
        /// </summary>
        /// <param name="timezone">The timezone to use</param>
        /// <returns>The configured <see cref="IScheduleBuilder"/></returns>
        IScheduleBuilder UseTimezone(string? timezone);

        /// <summary>
        /// Builds a new <see cref="ScheduleDefinition"/>
        /// </summary>
        /// <returns>A new <see cref="ScheduleDefinition"/></returns>
        ScheduleDefinition Build();

    }

}
