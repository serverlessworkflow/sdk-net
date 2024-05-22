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

namespace ServerlessWorkflow.Sdk.Models.Tasks;

/// <summary>
/// Represents the definition of a task that executes a set of subtasks iteratively for each element in a collection
/// </summary>
[DataContract]
public record ForTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Type => TaskType.For;

    /// <summary>
    /// Gets/sets the definition of the loop that iterates over a range of values
    /// </summary>
    [Required]
    [DataMember(Name = "for", Order = 1), JsonPropertyName("for"), JsonPropertyOrder(1), YamlMember(Alias = "for", Order = 1)]
    public required virtual ForLoopDefinition For { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression that represents the condition, if any, that must be met for the iteration to continue
    /// </summary>
    [DataMember(Name = "while", Order = 2), JsonPropertyName("while"), JsonPropertyOrder(2), YamlMember(Alias = "while", Order = 2)]
    public virtual string? While { get; set; }

    /// <summary>
    /// Gets/sets the task to perform for each item in the collection
    /// </summary>
    [Required]
    [DataMember(Name = "do", Order = 3), JsonPropertyName("do"), JsonPropertyOrder(3), YamlMember(Alias = "do", Order = 3)]
    public required virtual TaskDefinition Do { get; set; }

}
