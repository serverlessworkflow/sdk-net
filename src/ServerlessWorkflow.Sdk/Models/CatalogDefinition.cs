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
[DataContract]
public record CatalogDefinition
{

    /// <summary>
    /// Gets the name of the default catalog
    /// </summary>
    public const string DefaultCatalogName = "default";

    /// <summary>
    /// Gets/sets the endpoint that defines the root URL at which the catalog is located
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual EndpointDefinition Endpoint
    {
        get => this.EndpointValue.T1Value ?? new() { Uri = this.EndpointUri };
        set => this.EndpointValue = value;
    }

    /// <summary>
    /// Gets/sets the endpoint that defines the root URL at which the catalog is located
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual Uri EndpointUri
    {
        get => this.EndpointValue.T1Value?.Uri ?? this.EndpointValue.T2Value!;
        set => this.EndpointValue = value;
    }

    /// <summary>
    /// Gets/sets the endpoint that defines the root URL at which the catalog is located
    /// </summary>
    [Required]
    [DataMember(Name = "endpoint", Order = 1), JsonInclude, JsonPropertyName("endpoint"), JsonPropertyOrder(1), YamlMember(Alias = "endpoint", Order = 1)]
    protected virtual OneOf<EndpointDefinition, Uri> EndpointValue { get; set; } = null!;

}
