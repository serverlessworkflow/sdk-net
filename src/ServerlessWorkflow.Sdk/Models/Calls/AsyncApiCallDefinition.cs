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

namespace ServerlessWorkflow.Sdk.Models.Calls;

/// <summary>
/// Represents the definition of an AsyncAPI call
/// </summary>
[DataContract]
public record AsyncApiCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the document that defines the AsyncAPI operation to call
    /// </summary>
    [Required]
    [DataMember(Name = "document", Order = 1), JsonPropertyName("document"), JsonPropertyOrder(1), YamlMember(Alias = "document", Order = 1)]
    public required virtual ExternalResourceDefinition Document { get; set; }

    /// <summary>
    /// Gets/sets the name of the channel on which to perform the operation. The operation to perform is defined by declaring either message, in which case the channel's publish operation will be executed, or subscription, in which case the channel's subscribe operation will be executed.<para></para>
    /// Used only in case the referenced document uses AsyncAPI v2.6.0
    /// </summary>
    [DataMember(Name = "channel", Order = 2), JsonPropertyName("channel"), JsonPropertyOrder(2), JsonInclude, YamlMember(Alias = "channel", Order = 2)]
    public virtual string? Channel { get; set; }

    /// <summary>
    /// Gets/sets a reference to the AsyncAPI operation to call.<para></para>
    /// Used only in case the referenced document uses AsyncAPI v3.0.0.
    /// </summary>
    [DataMember(Name = "operation", Order = 3), JsonPropertyName("operation"), JsonPropertyOrder(3), JsonInclude, YamlMember(Alias = "operation", Order = 3)]
    public virtual string? Operation { get; set; }

    /// <summary>
    /// Gets/sets a object used to configure to the server to call the specified AsyncAPI operation on.<para></para>
    /// If not set, default to the first server matching the operation's channel.
    /// </summary>
    [DataMember(Name = "server", Order = 4), JsonPropertyName("server"), JsonPropertyOrder(4), JsonInclude, YamlMember(Alias = "server", Order = 4)]
    public virtual string? Server { get; set; }

    /// <summary>
    /// Gets/sets the protocol to use to select the target server.<para></para>
    /// Ignored if <see cref="Server"/> has been set.
    /// </summary>
    [DataMember(Name = "protocol", Order = 5), JsonPropertyName("protocol"), JsonPropertyOrder(5), JsonInclude, YamlMember(Alias = "protocol", Order = 5)]
    public virtual string? Protocol { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the message to publish using the target operation.<para></para>
    /// Required if <see cref="Subscription"/> has not been set.
    /// </summary>
    [DataMember(Name = "message", Order = 6), JsonPropertyName("message"), JsonPropertyOrder(6), JsonInclude, YamlMember(Alias = "message", Order = 6)]
    public virtual AsyncApiMessageDefinition? Message { get; set; }

    /// <summary>
    /// Gets/sets an object used to configure the subscription to messages consumed using the target operation.<para></para>
    /// Required if <see cref="Message"/> has not been set.
    /// </summary>
    [DataMember(Name = "subscription", Order = 7), JsonPropertyName("subscription"), JsonPropertyOrder(7), JsonInclude, YamlMember(Alias = "subscription", Order = 7)]
    public virtual AsyncApiSubscriptionDefinition? Subscription { get; set; }

    /// <summary>
    /// Gets/sets the authentication policy, if any, to use when calling the AsyncAPI operation
    /// </summary>
    [DataMember(Name = "authentication", Order = 8), JsonPropertyName("authentication"), JsonPropertyOrder(8), JsonInclude, YamlMember(Alias = "authentication", Order = 8)]
    public virtual AuthenticationPolicyDefinition? Authentication { get; set; }

}
