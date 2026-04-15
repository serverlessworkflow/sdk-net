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
/// Represents the definition of an external resource
/// </summary>
[Description("Represents the definition of an external resource")]
[DataContract]
public sealed record ExternalResourceDefinition
{

    /// <summary>
    /// Gets/sets the external resource's name, if any
    /// </summary>
    [Description("The external resource's name, if any")]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [Description("The endpoint at which to get the defined resource")]
    [Required]
    [DataMember(Order = 2, Name = "endpoint"), JsonPropertyOrder(2), JsonPropertyName("endpoint"), JsonConverter(typeof(OneOfJsonConverter<EndpointDefinition, Uri>))]
    public required OneOf<EndpointDefinition, Uri> Endpoint { get; init; }

}