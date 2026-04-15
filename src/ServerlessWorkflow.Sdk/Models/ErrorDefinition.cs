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
[Description("Represents the definition an error to raise")]
[DataContract]
public sealed record ErrorDefinition
    : ReferenceableComponentDefinition
{

    /// <summary>
    /// Gets/sets an uri that reference the type of the described error.
    /// </summary>
    [Description("An uri that reference the type of the described error.")]
    [DataMember(Order = 1, Name = "type"), JsonPropertyOrder(1), JsonPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>
    /// Gets/sets a short, human-readable summary of the error type.It SHOULD NOT change from occurrence to occurrence of the error, except for purposes of localization.
    /// </summary>
    [Description("A short, human-readable summary of the error type.It SHOULD NOT change from occurrence to occurrence of the error, except for purposes of localization.")]
    [DataMember(Order = 2, Name = "title"), JsonPropertyOrder(2), JsonPropertyName("title")]
    public required string Title { get; init; }

    /// <summary>
    /// Gets/sets the status code produced by the described error
    /// </summary>
    [Description("The status code produced by the described error")]
    [DataMember(Order = 3, Name = "status"), JsonPropertyOrder(3), JsonPropertyName("status")]
    public required string Status { get; init; }

    /// <summary>
    /// Gets/sets a human-readable explanation specific to this occurrence of the error.
    /// </summary>
    [Description("A human-readable explanation specific to this occurrence of the error.")]
    [DataMember(Order = 4, Name = "detail"), JsonPropertyOrder(4), JsonPropertyName("detail")]
    public string? Detail { get; init; }

    /// <summary>
    /// Gets/sets a <see cref="Uri"/> reference that identifies the specific occurrence of the error.It may or may not yield further information if dereferenced.
    /// </summary>
    [Description("A reference that identifies the specific occurrence of the error.It may or may not yield further information if dereferenced.")]
    [DataMember(Order = 5, Name = "instance"), JsonPropertyOrder(5), JsonPropertyName("instance")]
    public string? Instance { get; init; }

    /// <summary>
    /// Gets/sets a mapping containing error details extension data, if any
    /// </summary>
    [Description("A mapping containing error details extension data, if any")]
    [DataMember(Order = 6, Name = "extensionData"), JsonExtensionData]
    public IDictionary<string, JsonElement>? ExtensionData { get; set; }

}