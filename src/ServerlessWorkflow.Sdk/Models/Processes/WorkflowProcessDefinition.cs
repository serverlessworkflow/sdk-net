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

namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents the definition of a (sub)workflow process
/// </summary>
[DataContract]
public record WorkflowProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the namespace the workflow to run belongs to
    /// </summary>
    [Required, MinLength(1), MaxLength(63)]
    [DataMember(Name = "namespace", Order = 1), JsonPropertyName("namespace"), JsonPropertyOrder(1), YamlMember(Alias = "namespace", Order = 1)]
    public required virtual string Namespace { get; set; }

    /// <summary>
    /// Gets/sets the name of the workflow to run
    /// </summary>
    [Required, MinLength(1), MaxLength(63)]
    [DataMember(Name = "name", Order = 2), JsonPropertyName("name"), JsonPropertyOrder(2), YamlMember(Alias = "name", Order = 2)]
    public required virtual string Name { get; set; }

    /// <summary>
    /// Gets/sets the version of the workflow to run. Defaults to `latest`
    /// </summary>
    [SemanticVersion]
    [DataMember(Name = "version", Order = 3), JsonPropertyName("version"), JsonPropertyOrder(3), YamlMember(Alias = "version", Order = 3, ScalarStyle = ScalarStyle.SingleQuoted)]
    public virtual string Version { get; set; } = "latest";

    /// <summary>
    /// Gets/sets the data, if any, to pass as input to the workflow to execute. The value should be validated against the target workflow's input schema, if specified
    /// </summary>
    [DataMember(Name = "input", Order = 4), JsonPropertyName("input"), JsonPropertyOrder(4), YamlMember(Alias = "input", Order = 4)]
    public virtual object? Input { get; set; }

}