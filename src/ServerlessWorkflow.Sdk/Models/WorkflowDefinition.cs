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
/// Represents the definition of a workflow
/// </summary>
[Description("Represents the definition of a workflow.")]
[DataContract]
public sealed record WorkflowDefinition
{

    /// <summary>
    /// Gets/sets an object used to document the defined workflow
    /// </summary>
    [Description("An object used to document the defined workflow.")]
    [Required]
    [DataMember(Order = 1, Name = "document"), JsonPropertyOrder(1), JsonPropertyName("document")]
    public required WorkflowDefinitionMetadata Document { get; init; }

    /// <summary>
    /// Gets/sets the workflow's input definition, if any
    /// </summary>
    [Description("The workflow's input definition, if any.")]
    [DataMember(Order = 2, Name = "input"), JsonPropertyOrder(2), JsonPropertyName("input")]
    public InputDataModelDefinition? Input { get; init; }

    /// <summary>
    /// Gets/sets a collection that contains reusable components for the workflow definition
    /// </summary>
    [Description("A collection that contains reusable components for the workflow definition.")]
    [DataMember(Order = 3, Name = "use"), JsonPropertyOrder(3), JsonPropertyName("use")]
    public ComponentDefinitionCollection? Use { get; init; }

    /// <summary>
    /// Gets/sets the workflow's timeout, if any
    /// </summary>
    [Description("The workflow's timeout, if any.")]
    [DataMember(Order = 4, Name = "timeout"), JsonPropertyOrder(4), JsonPropertyName("timeout"), JsonConverter(typeof(OneOfJsonConverter<TimeoutDefinition, string>))]
    public OneOf<TimeoutDefinition, string>? Timeout { get; init; } = null!;

    /// <summary>
    /// Gets/sets the workflow's output definition, if any
    /// </summary>
    [Description("The workflow's output definition, if any.")]
    [DataMember(Order = 5, Name = "output"), JsonPropertyOrder(5), JsonPropertyName("output")]
    public OutputDataModelDefinition? Output { get; init; }

    /// <summary>
    /// Gets/sets the definition of the workflow's schedule, if any
    /// </summary>
    [Description("The definition of the workflow's schedule, if any.")]
    [DataMember(Order = 6, Name = "schedule"), JsonPropertyOrder(6), JsonPropertyName("schedule")]
    public WorkflowScheduleDefinition? Schedule { get; init; }

    /// <summary>
    /// Gets/sets the configuration of how the runtime expressions
    /// </summary>
    [Description("The configuration of how the runtime expressions should be evaluated at runtime.")]
    [DataMember(Order = 7, Name = "evaluate"), JsonPropertyOrder(7), JsonPropertyName("evaluate")]
    public RuntimeExpressionEvaluationConfiguration? Evaluate { get; init; }

    /// <summary>
    /// Gets/sets a name/value mapping of the tasks to perform
    /// </summary>
    [Description("A name/value mapping of the tasks to perform.")]
    [Required, MinLength(1)]
    [DataMember(Order = 8, Name = "do"), JsonPropertyOrder(8), JsonPropertyName("do")]
    public required Map<string, TaskDefinition> Do { get; init; } = [];

    /// <summary>
    /// Gets/sets a key/value mapping of additional information associated with the workflow
    /// </summary>
    [Description("A key/value mapping of additional information associated with the workflow.")]
    [DataMember(Order = 9, Name = "metadata"), JsonPropertyOrder(9), JsonPropertyName("metadata")]
    public EquatableDictionary<string, object>? Metadata { get; init; }

}
