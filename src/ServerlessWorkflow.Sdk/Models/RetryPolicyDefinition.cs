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
[DataContract]
public record RetryPolicyDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    [DataMember(Name = "when", Order = 1), JsonPropertyName("when"), JsonPropertyOrder(1), YamlMember(Alias = "when", Order = 1)]
    public virtual string? When { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    [DataMember(Name = "exceptWhen", Order = 2), JsonPropertyName("exceptWhen"), JsonPropertyOrder(2), YamlMember(Alias = "exceptWhen", Order = 2)]
    public virtual string? ExceptWhen { get; set; }

    /// <summary>
    /// Gets/sets the limits, if any, of the retry policy
    /// </summary>
    [DataMember(Name = "limit", Order = 3), JsonPropertyName("limit"), JsonPropertyOrder(2), YamlMember(Alias = "limit", Order = 3)]
    public virtual RetryPolicyLimitDefinition? Limit { get; set; }

    /// <summary>
    /// Gets/sets the delay duration between retry attempts
    /// </summary>
    [DataMember(Name = "delay", Order = 4), JsonPropertyName("delay"), JsonPropertyOrder(4), YamlMember(Alias = "delay", Order = 4)]
    public virtual Duration? Delay { get; set; }

    /// <summary>
    /// Gets/sets the backoff strategy to use, if any
    /// </summary>
    [DataMember(Name = "backoff", Order = 5), JsonPropertyName("backoff"), JsonPropertyOrder(5), YamlMember(Alias = "backoff", Order = 5)]
    public virtual BackoffStrategyDefinition? Backoff { get; set; }

    /// <summary>
    /// Gets/sets the parameters, if any, that control the randomness or variability of the delay between retry attempts
    /// </summary>
    [DataMember(Name = "jitter", Order = 6), JsonPropertyName("jitter"), JsonPropertyOrder(6), YamlMember(Alias = "jitter", Order = 6)]
    public virtual JitterDefinition? Jitter { get; set; }

}
