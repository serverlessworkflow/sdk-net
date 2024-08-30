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
[DataContract]
public record WorkflowDefinition
{

    /// <summary>
    /// Gets/sets an object used to document the defined workflow
    /// </summary>
    [Required]
    [DataMember(Name = "document", Order = 1), JsonPropertyName("document"), JsonPropertyOrder(1), YamlMember(Alias = "document", Order = 1)]
    public required virtual WorkflowDefinitionMetadata Document { get; set; }

    /// <summary>
    /// Gets/sets the workflow's input definition, if any
    /// </summary>
    [DataMember(Name = "input", Order = 2), JsonPropertyName("input"), JsonPropertyOrder(2), YamlMember(Alias = "input", Order = 3)]
    public virtual InputDataModelDefinition? Input { get; set; }

    /// <summary>
    /// Gets/sets a collection that contains reusable components for the workflow definition
    /// </summary>
    [DataMember(Name = "use", Order = 3), JsonPropertyName("use"), JsonPropertyOrder(3), YamlMember(Alias = "use", Order = 3)]
    public virtual ComponentDefinitionCollection? Use { get; set; }

    /// <summary>
    /// Gets/sets the workflow's timeout, if any
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual TimeoutDefinition? Timeout
    {
        get => this.TimeoutValue.T1Value;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            this.TimeoutValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the reference to the workflow's timeout, if any
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? TimeoutReference
    {
        get => this.TimeoutValue.T2Value;
        set
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            this.TimeoutValue = value;
        }
    }

    /// <summary>
    /// Gets/sets the workflow's timeout, if any
    /// </summary>
    [DataMember(Name = "timeout", Order = 4), JsonPropertyName("timeout"), JsonPropertyOrder(4), YamlMember(Alias = "timeout", Order = 4)]
    protected virtual OneOf<TimeoutDefinition, string> TimeoutValue { get; set; } = null!;

    /// <summary>
    /// Gets/sets the workflow's output definition, if any
    /// </summary>
    [DataMember(Name = "output", Order = 5), JsonPropertyName("output"), JsonPropertyOrder(5), YamlMember(Alias = "output", Order = 5)]
    public virtual OutputDataModelDefinition? Output { get; set; }

    /// <summary>
    /// Gets/sets the definition of the workflow's schedule, if any
    /// </summary>
    [DataMember(Name = "schedule", Order = 6), JsonPropertyName("schedule"), JsonPropertyOrder(6), YamlMember(Alias = "schedule", Order = 6)]
    public virtual WorkflowScheduleDefinition? Schedule { get; set; }

    /// <summary>
    /// Gets/sets the configuration of how the runtime expressions
    /// </summary>
    [DataMember(Name = "evaluate", Order = 7), JsonPropertyName("evaluate"), JsonPropertyOrder(7), YamlMember(Alias = "evaluate", Order = 7)]
    public virtual RuntimeExpressionEvaluationConfiguration? Evaluate { get; set; }

    /// <summary>
    /// Gets/sets a name/value mapping of the tasks to perform
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "do", Order = 8), JsonPropertyName("do"), JsonPropertyOrder(8), YamlMember(Alias = "do", Order = 8)]
    public required virtual Map<string, TaskDefinition> Do { get; set; } = [];

    /// <summary>
    /// Gets/sets a key/value mapping of additional information associated with the workflow
    /// </summary>
    [DataMember(Name = "metadata", Order = 9), JsonPropertyName("metadata"), JsonPropertyOrder(9), YamlMember(Alias = "metadata", Order = 9)]
    public virtual EquatableDictionary<string, object>? Metadata { get; set; }

}
