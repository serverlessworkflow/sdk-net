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
/// Represents the definition an error to raise
/// </summary>
[DataContract]
public record ErrorDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets an uri that reference the type of the described error.
    /// </summary>
    [DataMember(Order = 1, Name = "type"), JsonPropertyName("type"), JsonPropertyOrder(1), YamlMember(Alias = "type", Order = 1)]
    public required virtual string Type { get; set; }

    /// <summary>
    /// Gets/sets a short, human-readable summary of the error type.It SHOULD NOT change from occurrence to occurrence of the error, except for purposes of localization.
    /// </summary>
    [DataMember(Order = 2, Name = "title"), JsonPropertyName("title"), JsonPropertyOrder(2), YamlMember(Alias = "title", Order = 2)]
    public required virtual string Title { get; set; }

    /// <summary>
    /// Gets/sets the status code produced by the described error
    /// </summary>
    [DataMember(Order = 3, Name = "status"), JsonPropertyName("status"), JsonPropertyOrder(3), YamlMember(Alias = "status", Order = 3)]
    public required virtual object Status { get; set; }

    /// <summary>
    /// Gets/sets a human-readable explanation specific to this occurrence of the error.
    /// </summary>
    [DataMember(Order = 4, Name = "detail"), JsonPropertyName("detail"), JsonPropertyOrder(4), YamlMember(Alias = "detail", Order = 4)]
    public virtual string? Detail { get; set; }

    /// <summary>
    /// Gets/sets a <see cref="Uri"/> reference that identifies the specific occurrence of the error.It may or may not yield further information if dereferenced.
    /// </summary>
    [DataMember(Order = 5, Name = "instance"), JsonPropertyName("instance"), JsonPropertyOrder(5), YamlMember(Alias = "instance", Order = 5)]
    public virtual string? Instance { get; set; }

    /// <summary>
    /// Gets/sets a mapping containing error details extension data, if any
    /// </summary>
    [DataMember(Order = 6, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

}