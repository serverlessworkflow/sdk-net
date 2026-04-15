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
/// Represents the configuration of a task that is composed of multiple subtasks to run sequentially
/// </summary>
[Description("Represents the configuration of a task that is composed of multiple subtasks to run sequentially")]
[DataContract]
public sealed record DoTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Do;

    /// <summary>
    /// Gets/sets a name/definition mapping of the subtasks to perform sequentially
    /// </summary>
    [Required, MinLength(1)]
    [Description("A name/definition mapping of the subtasks to perform sequentially")]
    [DataMember(Order = 1, Name = "do"), JsonPropertyOrder(1), JsonPropertyName("do")]
    public required Map<string, TaskDefinition> Do { get; init; }

}
