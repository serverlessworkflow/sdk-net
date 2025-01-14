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
/// Represents an object used to configure branches to perform concurrently
/// </summary>
[DataContract]
public record BranchingDefinition
{

    /// <summary>
    /// Gets/sets a name/definition mapping of the subtasks to perform concurrently
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "branches", Order = 1), JsonPropertyName("branches"), JsonPropertyOrder(1), YamlMember(Alias = "branches", Order = 1)]
    public required virtual Map<string, TaskDefinition> Branches { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the branches should compete each other. If `true` and if a branch completes, it will cancel all other branches then it will return its output as the task's output
    /// </summary>
    [DataMember(Name = "compete", Order = 1), JsonPropertyName("compete"), JsonPropertyOrder(1), YamlMember(Alias = "compete", Order = 1)]
    public virtual bool Compete { get; set; }

}