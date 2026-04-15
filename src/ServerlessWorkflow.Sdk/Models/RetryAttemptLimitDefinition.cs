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
/// Represents the definition of the limits for all retry attempts of a given policy
/// </summary>
[Description("Represents the definition of the limits for all retry attempts of a given policy")]
[DataContract]
public sealed record RetryAttemptLimitDefinition
{

    /// <summary>
    /// Gets/sets the maximum attempts count
    /// </summary>
    [Description("The maximum attempts count")]
    [DataMember(Order = 1, Name = "count"), JsonPropertyOrder(1), JsonPropertyName("count")]
    public uint? Count { get; init; }

    /// <summary>
    /// Gets/sets the duration limit, if any, for all retry attempts
    /// </summary>
    [Description("The duration limit, if any, for all retry attempts")]
    [DataMember(Order = 2, Name = "duration"), JsonPropertyOrder(2), JsonPropertyName("duration")]
    public Duration? Duration { get; init; }

}