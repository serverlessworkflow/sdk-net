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
[DataContract]
public record DoTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Type => TaskType.Do;

    /// <summary>
    /// Gets/sets a name/definition mapping of the subtasks to perform sequentially
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "do", Order = 1), JsonPropertyName("do"), JsonPropertyOrder(1), YamlMember(Alias = "do", Order = 1)]
    public required virtual Map<string, TaskDefinition> Do { get; set; }

}
