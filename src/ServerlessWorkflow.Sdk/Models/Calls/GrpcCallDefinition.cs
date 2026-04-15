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
/// Represents the definition of a GRPC call
/// </summary>
[Description("Represents the definition of a GRPC call")]
[DataContract]
public sealed record GrpcCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets the proto resource that describes the GRPC service to call
    /// </summary>
    [Description("The proto resource that describes the GRPC service to call")]
    [Required]
    [DataMember(Order = 1, Name = "proto"), JsonPropertyOrder(1), JsonPropertyName("proto")]
    public required ExternalResourceDefinition Proto { get; init; }

    /// <summary>
    /// Gets/sets the definition of the GRPC service to call
    /// </summary>
    [Description("The definition of the GRPC service to call")]
    [Required]
    [DataMember(Order = 2, Name = "service"), JsonPropertyOrder(2), JsonPropertyName("service")]
    public required GrpcServiceDefinition Service { get; init; }

    /// <summary>
    /// Gets/sets the name of the GRPC service method to call
    /// </summary>
    [Description("The name of the GRPC service method to call")]
    [Required, MinLength(1)]
    [DataMember(Order = 3, Name = "method"), JsonPropertyOrder(3), JsonPropertyName("method")]
    public required string Method { get; init; }

    /// <summary>
    /// Gets/sets the method call's arguments, if any
    /// </summary>
    [Description("The method call's arguments, if any")]
    [DataMember(Order = 4, Name = "arguments"), JsonPropertyOrder(4), JsonPropertyName("arguments")]
    public JsonObject? Arguments { get; init; }

}
