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
/// Represents the definition of a task used to wait a certain amount of time
/// </summary>
[DataContract]
public record WaitTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Type => TaskType.Wait;

    /// <summary>
    /// Gets/sets the amount of time to wait before resuming workflow
    /// </summary>
    [Required]
    [DataMember(Name = "wait", Order = 1), JsonPropertyName("wait"), JsonPropertyOrder(1), YamlMember(Alias = "wait", Order = 1)]
    public required virtual Duration Wait { get; set; }

}
