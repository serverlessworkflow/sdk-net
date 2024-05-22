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

using System.Text.Json.Nodes;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a schema
/// </summary>
[DataContract]
public record SchemaDefinition
{

    /// <summary>
    /// Gets/sets the schema's format. Defaults to 'json'. The (optional) version of the format can be set using `{format}:{version}`.
    /// </summary>
    [Required]
    [DataMember(Name = "format", Order = 1), JsonPropertyName("format"), JsonPropertyOrder(1), YamlMember(Alias = "format", Order = 1)]
    public virtual string Format { get; set; } = SchemaFormat.Json;

    /// <summary>
    /// Gets/sets the schema's external resource, if any. Required if <see cref="Document"/> has not been set.
    /// </summary>
    [DataMember(Name = "resource", Order = 2), JsonPropertyName("resource"), JsonPropertyOrder(2), YamlMember(Alias = "resource", Order = 2)]
    public virtual ExternalResourceDefinition? Resource { get; set; }

    /// <summary>
    /// Gets/sets the inline definition of the schema to use. Required if <see cref="Resource"/> has not been set.
    /// </summary>
    [DataMember(Name = "document", Order = 3), JsonPropertyName("document"), JsonPropertyOrder(3), YamlMember(Alias = "document", Order = 3)]
    public virtual object? Document { get; set; }

}
