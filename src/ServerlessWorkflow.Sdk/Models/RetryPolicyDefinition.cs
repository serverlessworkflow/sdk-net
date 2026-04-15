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
/// Represents the definition of a retry policy
/// </summary>
[Description("Represents the definition of a retry policy")]
[DataContract]
public sealed record RetryPolicyDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    [Description("A runtime expression used to determine whether or not to retry running the task, in a given context")]
    [DataMember(Order = 1, Name = "when"), JsonPropertyOrder(1), JsonPropertyName("when")]
    public string? When { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    [Description("A runtime expression used to determine whether or not to retry running the task, in a given context")]
    [DataMember(Order = 2, Name = "exceptWhen"), JsonPropertyOrder(2), JsonPropertyName("exceptWhen")]
    public string? ExceptWhen { get; init; }

    /// <summary>
    /// Gets/sets the limits, if any, of the retry policy
    /// </summary>
    [Description("The limits, if any, of the retry policy")]
    [DataMember(Order = 3, Name = "limit"), JsonPropertyOrder(3), JsonPropertyName("limit")]
    public RetryPolicyLimitDefinition? Limit { get; init; }

    /// <summary>
    /// Gets/sets the delay duration between retry attempts
    /// </summary>
    [Description("The delay duration between retry attempts")]
    [DataMember(Order = 4, Name = "delay"), JsonPropertyOrder(4), JsonPropertyName("delay")]
    public Duration? Delay { get; init; }

    /// <summary>
    /// Gets/sets the backoff strategy to use, if any
    /// </summary>
    [Description("The backoff strategy to use, if any")]
    [DataMember(Order = 5, Name = "backoff"), JsonPropertyOrder(5), JsonPropertyName("backoff")]
    public BackoffStrategyDefinition? Backoff { get; init; }

    /// <summary>
    /// Gets/sets the parameters, if any, that control the randomness or variability of the delay between retry attempts
    /// </summary>
    [Description("The parameters, if any, that control the randomness or variability of the delay between retry attempts")]
    [DataMember(Order = 6, Name = "jitter"), JsonPropertyOrder(6), JsonPropertyName("jitter")]
    public JitterDefinition? Jitter { get; init; }

}
