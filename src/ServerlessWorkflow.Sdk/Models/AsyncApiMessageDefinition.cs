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
/// Represents the definition of an AsyncAPI message
/// </summary>
[DataContract]
public record AsyncApiMessageDefinition
{

    /// <summary>
    /// Gets/sets the message's payload, if any
    /// </summary>
    [DataMember(Name = "payload", Order = 1), JsonPropertyName("payload"), JsonPropertyOrder(1), YamlMember(Alias = "payload", Order = 1)]
    public virtual object? Payload { get; set; }

    /// <summary>
    /// Gets/sets the message's headers, if any
    /// </summary>
    [DataMember(Name = "headers", Order = 2), JsonPropertyName("headers"), JsonPropertyOrder(2), YamlMember(Alias = "headers", Order = 2)]
    public virtual object? Headers { get; set; }

}