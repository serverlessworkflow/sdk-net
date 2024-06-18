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
/// Represents the definition of a an extension
/// </summary>
[DataContract]
public record ExtensionDefinition
    : Extendable
{

    /// <summary>
    /// Gets/sets the type of task to extend
    /// </summary>
    [Required]
    [DataMember(Name = "extend", Order = 1), JsonPropertyName("extend"), JsonPropertyOrder(1), YamlMember(Alias = "extend", Order = 1)]
    public required virtual string Extend { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to determine whether or not the extension should apply in the specified context
    /// </summary>
    [DataMember(Name = "when", Order = 2), JsonPropertyName("when"), JsonPropertyOrder(2), YamlMember(Alias = "when", Order = 2)]
    public virtual string? When { get; set; }

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to execute before the extended task, if any
    /// </summary>
    [DataMember(Name = "before", Order = 3), JsonPropertyName("before"), JsonPropertyOrder(3), YamlMember(Alias = "before", Order = 3)]
    public virtual Map<string, TaskDefinition>? Before { get; set; }

    /// <summary>
    /// Gets/sets a name/definition map of the tasks to execute after the extended task, if any
    /// </summary>
    [DataMember(Name = "after", Order = 4), JsonPropertyName("after"), JsonPropertyOrder(4), YamlMember(Alias = "after", Order = 4)]
    public virtual Map<string, TaskDefinition>? After { get; set; }

}