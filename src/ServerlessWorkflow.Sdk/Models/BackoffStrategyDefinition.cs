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
/// Represents the definition of a retry backoff strategy
/// </summary>
[Description("Represents the definition of a retry backoff strategy")]
[DataContract]
public sealed record BackoffStrategyDefinition
{

    /// <summary>
    /// Gets/sets the definition of the constant backoff to use, if any
    /// </summary>
    [Description("The definition of the constant backoff to use, if any")]
    [DataMember(Order = 1, Name = "constant"), JsonPropertyOrder(1), JsonPropertyName("constant")]
    public ConstantBackoffDefinition? Constant { get; init; }

    /// <summary>
    /// Gets/sets the definition of the exponential backoff to use, if any
    /// </summary>
    [Description("The definition of the exponential backoff to use, if any")]
    [DataMember(Order = 2, Name = "exponential"), JsonPropertyOrder(2), JsonPropertyName("exponential")]
    public ExponentialBackoffDefinition? Exponential { get; init; }

    /// <summary>
    /// Gets/sets the definition of the linear backoff to use, if any
    /// </summary>
    [Description("The definition of the linear backoff to use, if any")]
    [DataMember(Order = 3, Name = "linear"), JsonPropertyOrder(3), JsonPropertyName("linear")]
    public LinearBackoffDefinition? Linear { get; init; }

}
