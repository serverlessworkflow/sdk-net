// Copyright © 2023-Present The Serverless Workflow Specification Authors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for Iso8601DurationHelper.Durations
/// </summary>
public static class DurationExtensions
{

    /// <summary>
    /// Converts the <see cref="Iso8601DurationHelper.Duration"/> into a <see cref="TimeSpan"/>
    /// </summary>
    /// <param name="duration">The <see cref="Iso8601DurationHelper.Duration"/> to convert</param>
    /// <returns>The converted <see cref="TimeSpan"/></returns>
    public static TimeSpan ToTimeSpan(this Iso8601DurationHelper.Duration duration)
    {
        return new TimeSpan((int)(duration.Days + duration.Weeks * 7 + duration.Months * 30 + duration.Years * 365), (int)duration.Hours, (int)duration.Minutes, (int)duration.Seconds);
    }

}
