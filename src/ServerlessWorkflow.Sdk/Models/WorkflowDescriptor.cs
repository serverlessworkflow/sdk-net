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
/// Represents an runtime expression argument used to describe the workflow being executed
/// </summary>
[Description("Represents an runtime expression argument used to describe the workflow being executed.")]
[DataContract]
public sealed record WorkflowDescriptor
{

    /// <summary>
    /// Gets/sets the workflow's id
    /// </summary>
    [Description("The workflow's id.")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>
    /// Gets/sets the workflow's definition
    /// </summary>
    [Description("The workflow's definition.")]
    [Required]
    [DataMember(Order = 2, Name = "definition"), JsonPropertyOrder(2), JsonPropertyName("definition")]
    public required WorkflowDefinition Definition { get; init; }

    /// <summary>
    /// Gets/sets the workflow's raw, untransformed input
    /// </summary>
    [Description("The workflow's raw, untransformed input.")]
    [DataMember(Order = 3, Name = "input"), JsonPropertyOrder(3), JsonPropertyName("input")]
    public JsonObject? Input { get; init; }

    /// <summary>
    /// Gets/sets the date and time at which the workflow has started
    /// </summary>
    [Description("The date and time at which the workflow has started.")]
    [Required]
    [DataMember(Order = 4, Name = "startedAt"), JsonPropertyOrder(4), JsonPropertyName("startedAt")]
    public DateTimeDescriptor? StartedAt { get; init; }

}
