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

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the configuration of the limits of a retry policy
/// </summary>
[Description("Represents the configuration of the limits of a retry policy")]
[DataContract]
public sealed record RetryPolicyLimitDefinition
{

    /// <summary>
    /// Gets/sets the definition of the limits for all retry attempts of a given policy
    /// </summary>
    [Description("The definition of the limits for all retry attempts of a given policy")]
    [DataMember(Order = 1, Name = "attempt"), JsonPropertyOrder(1), JsonPropertyName("attempt")]
    public RetryAttemptLimitDefinition? Attempt { get; init; }

    /// <summary>
    /// Gets/sets the maximum duration, if any, during which to retry a given task
    /// </summary>
    [Description("The maximum duration, if any, during which to retry a given task")]
    [DataMember(Order = 2, Name = "duration"), JsonPropertyOrder(2), JsonPropertyName("duration")]
    public Duration? Duration { get; init; }

}
