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
/// Represents the definition of a GRPC service
/// </summary>
[DataContract]
public record GrpcServiceDefinition
{

    /// <summary>
    /// Gets/sets the GRPC service name
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "name", Order = 1), JsonPropertyName("name"), JsonPropertyOrder(1), YamlMember(Alias = "name", Order = 1)]
    public required virtual string Name { get; set; }

    /// <summary>
    /// Gets/sets the hostname of the GRPC service to call
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "host", Order = 2), JsonPropertyName("host"), JsonPropertyOrder(2), YamlMember(Alias = "host", Order = 2)]
    public required virtual string Host { get; set; }

    /// <summary>
    /// Gets/sets the port number of the GRPC service to call
    /// </summary>
    [DataMember(Name = "port", Order = 3), JsonPropertyName("port"), JsonPropertyOrder(3), YamlMember(Alias = "port", Order = 3)]
    public virtual int? Port { get; set; }

    /// <summary>
    /// Gets/sets the endpoint's authentication policy, if any
    /// </summary>
    [DataMember(Name = "authentication", Order = 4), JsonPropertyName("authentication"), JsonPropertyOrder(4), JsonInclude, YamlMember(Alias = "authentication", Order = 4)]
    public virtual AuthenticationPolicyDefinition? Authentication { get; set; }

}