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

using YamlDotNet.Core;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the metadata of a workflow, including its name, version, and description.
/// </summary>
[DataContract]
public record WorkflowDefinitionMetadata
{

    /// <summary>
    /// Gets the namespace to use by default for workflow definitions
    /// </summary>
    public const string DefaultNamespace = "default";

    /// <summary>
    /// Gets/sets the version of the DSL used to define the workflow
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "dsl", Order = 1), JsonPropertyName("dsl"), JsonPropertyOrder(1), YamlMember(Alias = "dsl", Order = 1, ScalarStyle = ScalarStyle.SingleQuoted)]
    public required virtual string Dsl { get; set; } = null!;

    /// <summary>
    /// Gets/sets the workflow's namespace
    /// </summary>
    [DataMember(Name = "namespace", Order = 2), JsonPropertyName("namespace"), JsonPropertyOrder(2), YamlMember(Alias = "namespace", Order = 2)]
    public virtual string Namespace { get; set; } = DefaultNamespace;

    /// <summary>
    /// Gets/sets the workflow's name
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "name", Order = 3), JsonPropertyName("name"), JsonPropertyOrder(3), YamlMember(Alias = "name", Order = 3)]
    public required virtual string Name { get; set; } = null!;

    /// <summary>
    /// Gets/sets the workflow's semantic version
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "version", Order = 4), JsonPropertyName("version"), JsonPropertyOrder(4), YamlMember(Alias = "version", Order = 4, ScalarStyle = ScalarStyle.SingleQuoted)]
    public required virtual string Version { get; set; } = null!;

    /// <summary>
    /// Gets/sets the workflow's title, if any
    /// </summary>
    [DataMember(Name = "title", Order = 5), JsonPropertyName("title"), JsonPropertyOrder(5), YamlMember(Alias = "title", Order = 5)]
    public virtual string? Title { get; set; }

    /// <summary>
    /// Gets/sets the workflow's Markdown summary, if any
    /// </summary>
    [DataMember(Name = "summary", Order = 6), JsonPropertyName("summary"), JsonPropertyOrder(6), YamlMember(Alias = "summary", Order = 6)]
    public virtual string? Summary { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the workflow's tags, if any
    /// </summary>
    [DataMember(Name = "tags", Order = 7), JsonPropertyName("tags"), JsonPropertyOrder(7), YamlMember(Alias = "tags", Order = 7)]
    public virtual EquatableDictionary<string, string>? Tags { get; set; }

}