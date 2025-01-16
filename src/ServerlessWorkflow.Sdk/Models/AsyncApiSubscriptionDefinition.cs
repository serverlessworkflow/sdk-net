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
[DataContract]
public record AsyncApiSubscriptionDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to filter consumed messages
    /// </summary>
    [DataMember(Name = "filter", Order = 1), JsonPropertyName("filter"), JsonPropertyOrder(1), YamlMember(Alias = "filter", Order = 1)]
    public virtual string? Filter { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the subscription's lifetime.
    /// </summary>
    [Required]
    [DataMember(Name = "consume", Order = 2), JsonPropertyName("consume"), JsonPropertyOrder(2), YamlMember(Alias = "consume", Order = 2)]
    public required virtual AsyncApiSubscriptionLifetimeDefinition Consume { get; set; }

    /// <summary>
    /// Gets/sets the configuration of the iterator, if any, used to process each consumed message
    /// </summary>
    [DataMember(Name = "foreach", Order = 3), JsonPropertyName("foreach"), JsonPropertyOrder(3), YamlMember(Alias = "foreach", Order = 3)]
    public virtual SubscriptionIteratorDefinition? Foreach { get; set; }

}
