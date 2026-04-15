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

namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents the definition of a (sub)workflow process
/// </summary>
[DataContract]
public sealed record WorkflowProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the namespace the workflow to run belongs to
    /// </summary>
    [Description("The namespace the workflow to run belongs to")]
    [Required, StringLength(63, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "namespace"), JsonPropertyOrder(1), JsonPropertyName("namespace")]
    public required string Namespace { get; init; }

    /// <summary>
    /// Gets/sets the name of the workflow to run
    /// </summary>
    [Description("The name of the workflow to run")]
    [Required, StringLength(63, MinimumLength = 1)]
    [DataMember(Order = 2, Name = "name"), JsonPropertyOrder(2), JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets/sets the version of the workflow to run. Defaults to `latest`
    /// </summary>
    [Description("The version of the workflow to run. Defaults to `latest`")]
    [SemanticVersion]
    [DataMember(Order = 3, Name = "version"), JsonPropertyOrder(3), JsonPropertyName("version")]
    public string Version { get; init; } = "latest";

    /// <summary>
    /// Gets/sets the data, if any, to pass as input to the workflow to execute. The value should be validated against the target workflow's input schema, if specified
    /// </summary>
    [Description("The data, if any, to pass as input to the workflow to execute. The value should be validated against the target workflow's input schema, if specified")]
    [DataMember(Order = 4, Name = "input"), JsonPropertyOrder(4), JsonPropertyName("input")]
    public JsonObject? Input { get; init; }

}