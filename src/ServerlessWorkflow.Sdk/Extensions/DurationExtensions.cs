// Copyright © 2024-Present The Serverless Workflow Specification Authors
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

using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="Duration"/>s
/// </summary>
public static class DurationExtensions
{

    /// <summary>
    /// Converts the <see cref="Duration"/> into a new <see cref="TimeSpan"/>
    /// </summary>
    /// <param name="duration">The <see cref="Duration"/> to convert</param>
    /// <returns>A new <see cref="TimeSpan"/></returns>
    public static TimeSpan ToTimeSpan(this Duration duration) => new(duration.Days.HasValue ? (int)duration.Days : 0, duration.Hours.HasValue ? (int)duration.Hours : 0, duration.Minutes.HasValue ? (int)duration.Minutes : 0, duration.Seconds.HasValue ? (int)duration.Seconds : 0, duration.Milliseconds.HasValue ? (int)duration.Milliseconds : 0);

}