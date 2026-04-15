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
/// Represents an object used to configure an AsyncAPI subscription
/// </summary>
[Description("Represents an object used to configure an AsyncAPI subscription")]
[DataContract]
public sealed record AsyncApiSubscriptionDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to filter consumed messages
    /// </summary>
    [Description("A runtime expression, if any, used to filter consumed messages")]
    [DataMember(Order = 1, Name = "filter"), JsonPropertyOrder(1), JsonPropertyName("filter")]
    public string? Filter { get; init; }

    /// <summary>
    /// Gets/sets an object used to configure the subscription's lifetime.
    /// </summary>
    [Description("An object used to configure the subscription's lifetime")]
    [Required]
    [DataMember(Order = 2, Name = "consume"), JsonPropertyOrder(2), JsonPropertyName("consume")]
    public required AsyncApiSubscriptionLifetimeDefinition Consume { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the iterator, if any, used to process each consumed message
    /// </summary>
    [Description("The configuration of the iterator, if any, used to process each consumed message")]
    [DataMember(Order = 3, Name = "foreach"), JsonPropertyOrder(3), JsonPropertyName("foreach")]
    public SubscriptionIteratorDefinition? Foreach { get; init; }

}
