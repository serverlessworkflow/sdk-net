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
[Description("Represents the definition of a task that executes a set of subtasks iteratively for each element in a collection")]
[DataContract]
public sealed record ForTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.For;

    /// <summary>
    /// Gets/sets the definition of the loop that iterates over a range of values
    /// </summary>
    [Description("The definition of the loop that iterates over a range of values")]
    [Required]
    [DataMember(Order = 1, Name = "for"), JsonPropertyOrder(1), JsonPropertyName("for")]
    public required ForLoopDefinition For { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression that represents the condition, if any, that must be met for the iteration to continue
    /// </summary>
    [Description("A runtime expression that represents the condition, if any, that must be met for the iteration to continue")]
    [DataMember(Order = 2, Name = "while"), JsonPropertyOrder(2), JsonPropertyName("while")]
    public string? While { get; init; }

    /// <summary>
    /// Gets/sets the tasks to perform for each item in the collection
    /// </summary>
    [Description("The tasks to perform for each item in the collection")]
    [Required]
    [DataMember(Order = 3, Name = "do"), JsonPropertyOrder(3), JsonPropertyName("do")]
    public required Map<string, TaskDefinition> Do { get; init; }

}
