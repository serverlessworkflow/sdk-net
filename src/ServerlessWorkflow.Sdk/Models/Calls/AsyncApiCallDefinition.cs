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
    /// Gets/sets a reference to the AsyncAPI operation to call
    /// </summary>
    [Required]
    [DataMember(Name = "operationRef", Order = 2), JsonPropertyName("operationRef"), JsonPropertyOrder(2), JsonInclude, YamlMember(Alias = "operationRef", Order = 2)]
    public required virtual string OperationRef { get; set; }

    /// <summary>
    /// Gets/sets a reference to the server to call the specified AsyncAPI operation on. If not set, default to the first server matching the operation's channel.
    /// </summary>
    [DataMember(Name = "server", Order = 3), JsonPropertyName("server"), JsonPropertyOrder(3), JsonInclude, YamlMember(Alias = "server", Order = 3)]
    public virtual string? Server { get; set; }

    /// <summary>
    /// Gets/sets the name of the message to use. If not set, defaults to the first message defined by the operation
    /// </summary>
    [DataMember(Name = "message", Order = 4), JsonPropertyName("message"), JsonPropertyOrder(4), JsonInclude, YamlMember(Alias = "message", Order = 4)]
    public virtual string? Message { get; set; }

    /// <summary>
    /// Gets/sets the name of the binding to use. If not set, defaults to the first binding defined by the operation
    /// </summary>
    [DataMember(Name = "binding", Order = 5), JsonPropertyName("binding"), JsonPropertyOrder(5), JsonInclude, YamlMember(Alias = "binding", Order = 5)]
    public virtual string? Binding { get; set; }

    /// <summary>
    /// Gets/sets the payload to call the AsyncAPI operation with
    /// </summary>
    [DataMember(Name = "payload", Order = 6), JsonPropertyName("payload"), JsonPropertyOrder(6), JsonInclude, YamlMember(Alias = "payload", Order = 6)]
    public virtual object? Payload { get; set; }

    /// <summary>
    /// Gets/sets the authentication policy, if any, to use when calling the AsyncAPI operation
    /// </summary>
    [DataMember(Name = "authentication", Order = 7), JsonPropertyName("authentication"), JsonPropertyOrder(7), JsonInclude, YamlMember(Alias = "authentication", Order = 7)]
    public virtual AuthenticationPolicyDefinition? Authentication { get; set; }

}