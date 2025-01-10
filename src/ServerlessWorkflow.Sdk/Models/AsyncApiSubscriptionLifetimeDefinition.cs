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
/// Represents an object used to configure the lifetime of an AsyncAPI subscription
/// </summary>
[DataContract]
public record AsyncApiSubscriptionLifetimeDefinition
{

    /// <summary>
    /// Gets/sets the duration that defines for how long to consume messages
    /// /// </summary>
    [DataMember(Name = "for", Order = 1), JsonPropertyName("for"), JsonPropertyOrder(1), YamlMember(Alias = "for", Order = 1)]
    public virtual Duration? For { get; set; }

    /// <summary>
    /// Gets/sets the amount of messages to consume.<para></para>
    /// Required if <see cref="While"/> and <see cref="Until"/> have not been set.
    /// /// </summary>
    [DataMember(Name = "amount", Order = 2), JsonPropertyName("amount"), JsonPropertyOrder(2), YamlMember(Alias = "amount", Order = 2)]
    public virtual int? Amount { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine whether or not to keep consuming messages.<para></para>
    /// Required if <see cref="Amount"/> and <see cref="Until"/> have not been set.
    /// /// </summary>
    [DataMember(Name = "while", Order = 3), JsonPropertyName("while"), JsonPropertyOrder(3), YamlMember(Alias = "while", Order = 3)]
    public virtual string? While { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine until when to consume messages..<para></para>
    /// Required if <see cref="Amount"/> and <see cref="While"/> have not been set.
    /// /// </summary>
    [DataMember(Name = "until", Order = 4), JsonPropertyName("until"), JsonPropertyOrder(4), YamlMember(Alias = "until", Order = 4)]
    public virtual string? Until { get; set; }

}