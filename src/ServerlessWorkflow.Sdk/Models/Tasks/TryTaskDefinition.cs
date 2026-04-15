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
/// Represents the definition of a task used to try one or more subtasks, and to catch/handle the errors that can potentially be raised during execution
/// </summary>
[Description("Represents the definition of a task used to try one or more subtasks, and to catch/handle the errors that can potentially be raised during execution")]
[DataContract]
public sealed record TryTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Try;

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to try running
    /// </summary>
    [Description("A name/definition map of the tasks to try running")]
    [Required]
    [DataMember(Order = 1, Name = "try"), JsonPropertyOrder(1), JsonPropertyName("try")]
    public required Map<string, TaskDefinition> Try { get; init; }

    /// <summary>
    /// Gets/sets the object used to define the errors to catch
    /// </summary>
    [Description("The object used to define the errors to catch")]
    [Required]
    [DataMember(Order = 2, Name = "catch"), JsonPropertyOrder(2), JsonPropertyName("catch")]
    public required ErrorCatcherDefinition Catch { get; init; }

}
