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
[DataContract]
public record GrpcCallDefinition
    : CallDefinition
{

    /// <summary>
    /// Gets the proto resource that describes the GRPC service to call
    /// </summary>
    [Required]
    [DataMember(Name = "proto", Order = 1), JsonPropertyName("proto"), JsonPropertyOrder(1), YamlMember(Alias = "proto", Order = 1)]
    public required virtual ExternalResourceDefinition Proto { get; set; }

    /// <summary>
    /// Gets/sets the definition of the GRPC service to call
    /// </summary>
    [Required]
    [DataMember(Name = "service", Order = 2), JsonPropertyName("service"), JsonPropertyOrder(2), YamlMember(Alias = "service", Order = 2)]
    public required virtual GrpcServiceDefinition Service { get; set; }

    /// <summary>
    /// Gets/sets the name of the GRPC service method to call
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "method", Order = 3), JsonPropertyName("method"), JsonPropertyOrder(3), YamlMember(Alias = "method", Order = 3)]
    public required virtual string Method { get; set; }

    /// <summary>
    /// Gets/sets the method call's arguments, if any
    /// </summary>
    [DataMember(Name = "arguments", Order = 4), JsonPropertyName("arguments"), JsonPropertyOrder(4), YamlMember(Alias = "arguments", Order = 4)]
    public virtual IDictionary<string, object>? Arguments { get; set; }

}
