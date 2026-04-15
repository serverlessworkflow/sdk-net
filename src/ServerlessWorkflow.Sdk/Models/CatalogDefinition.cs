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
/// Represents the definition of a workflow component catalog
/// </summary>
[Description("Represents the definition of a workflow component catalog.")]
[DataContract]
public sealed record CatalogDefinition
{

    /// <summary>
    /// Gets the name of the default catalog
    /// </summary>
    public const string DefaultCatalogName = "default";

    /// <summary>
    /// Gets/sets the endpoint that defines the root URL at which the catalog is located
    /// </summary>
    [Description("The endpoint that defines the root URL at which the catalog is located.")]
    [Required]
    [DataMember(Order = 1, Name = "endpoint"), JsonPropertyOrder(1), JsonPropertyName("endpoint"), JsonConverter(typeof(OneOfJsonConverter<EndpointDefinition, Uri>))]
    public required OneOf<EndpointDefinition, Uri> Endpoint { get; init; }

}
