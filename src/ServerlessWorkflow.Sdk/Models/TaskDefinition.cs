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

using ServerlessWorkflow.Sdk.Serialization.Json;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a task
/// </summary>
[DataContract, JsonConverter(typeof(TaskDefinitionJsonConverter))]
public abstract record TaskDefinition
    : ComponentDefinition
{

    /// <summary>
    /// Gets the type of the defined task
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public abstract string Type { get; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine whether or not the execute the task in the current context
    /// </summary>
    [DataMember(Name = "if", Order = 0), JsonPropertyName("if"), JsonPropertyOrder(0), YamlMember(Alias = "if", Order = 0)]
    public virtual string? If { get; set; }

    /// <summary>
    /// Gets/sets the definition, if any, of the task's input data
    /// </summary>
    [DataMember(Name = "input", Order = 10), JsonPropertyName("input"), JsonPropertyOrder(10), YamlMember(Alias = "input", Order = 10)]
    public virtual InputDataModelDefinition? Input { get; set; }

    /// <summary>
    /// Gets/sets the definition, if any, of the task's output data
    /// </summary>
    [DataMember(Name = "output", Order = 11), JsonPropertyName("output"), JsonPropertyOrder(11), YamlMember(Alias = "output", Order = 11)]
    public virtual OutputDataModelDefinition? Output { get; set; }

    /// <summary>
    /// Gets/sets the optional configuration for exporting data within the task's context
    /// </summary>
    [DataMember(Name = "export", Order = 12), JsonPropertyName("export"), JsonPropertyOrder(12), YamlMember(Alias = "export", Order = 12)]
    public virtual OutputDataModelDefinition? Export { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to return the result, if any, of the defined task
    /// </summary>
    [DataMember(Name = "timeout", Order = 13), JsonPropertyName("timeout"), JsonPropertyOrder(13), YamlMember(Alias = "timeout", Order = 13)]
    public virtual TimeoutDefinition? Timeout { get; set; }

    /// <summary>
    /// Gets/sets the flow directive to be performed upon completion of the task
    /// </summary>
    [DataMember(Name = "then", Order = 14), JsonPropertyName("then"), JsonPropertyOrder(14), YamlMember(Alias = "then", Order = 14)]
    public virtual string? Then { get; set; }

}

