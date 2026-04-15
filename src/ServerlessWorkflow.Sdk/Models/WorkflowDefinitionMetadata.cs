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
/// Represents the metadata of a workflow, including its name, version, and description.
/// </summary>
[Description("Represents the metadata of a workflow, including its name, version, and description.")]
[DataContract]
public sealed record WorkflowDefinitionMetadata
{

    /// <summary>
    /// Gets the namespace to use by default for workflow definitions
    /// </summary>
    public const string DefaultNamespace = "default";

    /// <summary>
    /// Gets/sets the version of the DSL used to define the workflow
    /// </summary>
    [Description("The version of the DSL used to define the workflow.")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "dsl"), JsonPropertyOrder(1), JsonPropertyName("dsl")]
    public required string Dsl { get; init; } = null!;

    /// <summary>
    /// Gets/sets the workflow's namespace
    /// </summary>
    [Description("The workflow's namespace.")]
    [DataMember(Order = 2, Name = "namespace"), JsonPropertyOrder(2), JsonPropertyName("namespace")]
    public string Namespace { get; init; } = DefaultNamespace;

    /// <summary>
    /// Gets/sets the workflow's name
    /// </summary>
    [Description("The workflow's name.")]
    [Required, MinLength(1)]
    [DataMember(Order = 3, Name = "name"), JsonPropertyOrder(3), JsonPropertyName("name")]
    public required string Name { get; init; } = null!;

    /// <summary>
    /// Gets/sets the workflow's semantic version
    /// </summary>
    [Description("The workflow's semantic version.")]
    [Required, MinLength(1)]
    [DataMember(Order = 4, Name = "version"), JsonPropertyOrder(4), JsonPropertyName("version")]
    public required string Version { get; init; } = null!;

    /// <summary>
    /// Gets/sets the workflow's title, if any
    /// </summary>
    [Description("The workflow's title, if any.")]
    [DataMember(Order = 5, Name = "title"), JsonPropertyOrder(5), JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>
    /// Gets/sets the workflow's Markdown summary, if any
    /// </summary>
    [Description("The workflow's Markdown summary, if any.")]
    [DataMember(Order = 6, Name = "summary"), JsonPropertyOrder(6), JsonPropertyName("summary")]
    public string? Summary { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping of the workflow's tags, if any
    /// </summary>
    [Description("A key/value mapping of the workflow's tags, if any.")]
    [DataMember(Order = 7, Name = "tags"), JsonPropertyOrder(7), JsonPropertyName("tags")]
    public EquatableDictionary<string, string>? Tags { get; init; }

}