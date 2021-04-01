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
using Iso8601DurationHelper;
using System;

namespace ServerlessWorkflow.Sdk
{

    /// <summary>
    /// Represents an helper class for handling ISO 8601 timespans
    /// </summary>
    public static class Iso8601TimeSpan
    {

        public static TimeSpan Parse(string input)
        {
            Duration duration = Duration.Parse(input);
            return duration.ToTimeSpan();
        }

    }

    /// <summary>
    /// Defines extensions for <see cref="Duration"/>s
    /// </summary>
    public static class DurationExtensions
    {

        public static TimeSpan ToTimeSpan(this Duration duration)
        {
            int days = (int)duration.Days;
            days += (int)duration.Weeks * 7;
            days += (int)duration.Months * 30;
            days += (int)duration.Years * 365;
            return new TimeSpan(days, (int)duration.Hours, (int)duration.Minutes, (int)duration.Seconds);
        }

    }

}
