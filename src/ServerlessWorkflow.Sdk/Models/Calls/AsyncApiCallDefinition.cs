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
[Description("Represents the definition of an AsyncAPI call")]
[DataContract]
public sealed record AsyncApiCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets/sets the document that defines the AsyncAPI operation to call
    /// </summary>
    [Description("The document that defines the AsyncAPI operation to call")]
    [Required]
    [DataMember(Order = 1, Name = "document"), JsonPropertyOrder(1), JsonPropertyName("document")]
    public required ExternalResourceDefinition Document { get; init; }

    /// <summary>
    /// Gets/sets the name of the channel on which to perform the operation. The operation to perform is defined by declaring either message, in which case the channel's publish operation will be executed, or subscription, in which case the channel's subscribe operation will be executed.<para></para>
    /// Used only in case the referenced document uses AsyncAPI v2.6.0
    /// </summary>
    [Description("The name of the channel on which to perform the operation. The operation to perform is defined by declaring either message, in which case the channel's publish operation will be executed, or subscription, in which case the channel's subscribe operation will be executed. Used only in case the referenced document uses AsyncAPI v2.6.0")]
    [DataMember(Order = 2, Name = "channel"), JsonPropertyOrder(2), JsonPropertyName("channel")]
    public string? Channel { get; init; }

    /// <summary>
    /// Gets/sets a reference to the AsyncAPI operation to call.<para></para>
    /// Used only in case the referenced document uses AsyncAPI v3.0.0.
    /// </summary>
    [Description("A reference to the AsyncAPI operation to call. Used only in case the referenced document uses AsyncAPI v3.0.0.")]
    [DataMember(Order = 3, Name = "operation"), JsonPropertyOrder(3), JsonPropertyName("operation")]
    public string? Operation { get; init; }

    /// <summary>
    /// Gets/sets a object used to configure to the server to call the specified AsyncAPI operation on.<para></para>
    /// If not set, default to the first server matching the operation's channel.
    /// </summary>
    [Description("A object used to configure to the server to call the specified AsyncAPI operation on. If not set, default to the first server matching the operation's channel.")]
    [DataMember(Order = 4, Name = "server"), JsonPropertyOrder(4), JsonPropertyName("server")]
    public string? Server { get; init; }

    /// <summary>
    /// Gets/sets the protocol to use to select the target server.<para></para>
    /// Ignored if <see cref="Server"/> has been set.
    /// </summary>
    [Description("The protocol to use to select the target server. Ignored if Server has been set.")]
    [DataMember(Order = 5, Name = "protocol"), JsonPropertyOrder(5), JsonPropertyName("protocol")]
    public string? Protocol { get; init; }

    /// <summary>
    /// Gets/sets an object used to configure the message to publish using the target operation.<para></para>
    /// Required if <see cref="Subscription"/> has not been set.
    /// </summary>
    [Description("An object used to configure the message to publish using the target operation. Required if Subscription has not been set.")]
    [DataMember(Order = 6, Name = "message"), JsonPropertyOrder(6), JsonPropertyName("message")]
    public AsyncApiMessageDefinition? Message { get; init; }

    /// <summary>
    /// Gets/sets an object used to configure the subscription to messages consumed using the target operation.<para></para>
    /// Required if <see cref="Message"/> has not been set.
    /// </summary>
    [Description("An object used to configure the subscription to messages consumed using the target operation. Required if Message has not been set.")]
    [DataMember(Order = 7, Name = "subscription"), JsonPropertyOrder(7), JsonPropertyName("subscription")]
    public AsyncApiSubscriptionDefinition? Subscription { get; init; }

    /// <summary>
    /// Gets/sets the authentication policy, if any, to use when calling the AsyncAPI operation
    /// </summary>
    [Description("The authentication policy, if any, to use when calling the AsyncAPI operation")]
    [DataMember(Order = 8, Name = "authentication"), JsonPropertyOrder(8), JsonPropertyName("authentication")]
    public AuthenticationPolicyDefinition? Authentication { get; init; }

}
