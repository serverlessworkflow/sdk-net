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
[Description("Represents the definition of a GRPC service")]
[DataContract]
public sealed record GrpcServiceDefinition
{

    /// <summary>
    /// Gets/sets the GRPC service name
    /// </summary>
    [Description("The GRPC service name")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets/sets the hostname of the GRPC service to call
    /// </summary>
    [Description("The hostname of the GRPC service to call")]
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "host"), JsonPropertyOrder(2), JsonPropertyName("host")]
    public required string Host { get; init; }

    /// <summary>
    /// Gets/sets the port number of the GRPC service to call
    /// </summary>
    [Description("The port number of the GRPC service to call")]
    [DataMember(Order = 3, Name = "port"), JsonPropertyOrder(3), JsonPropertyName("port")]
    public int? Port { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's authentication policy, if any
    /// </summary>
    [Description("The endpoint's authentication policy, if any")]
    [DataMember(Order = 4, Name = "authentication"), JsonPropertyOrder(4), JsonPropertyName("authentication")]
    public AuthenticationPolicyDefinition? Authentication { get; init; }

}