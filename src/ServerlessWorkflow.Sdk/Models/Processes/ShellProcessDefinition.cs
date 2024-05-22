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

namespace ServerlessWorkflow.Sdk.Models.Processes;

/// <summary>
/// Represents the definition of a shell process
/// </summary>
[DataContract]
public record ShellProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the shell command to run
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "command", Order = 1), JsonPropertyName("command"), JsonPropertyOrder(1), YamlMember(Alias = "command", Order = 1)]
    public required virtual string Command { get; set; }

    /// <summary>
    /// Gets/sets the arguments of the shell command to run
    /// </summary>
    [DataMember(Name = "arguments", Order = 2), JsonPropertyName("arguments"), JsonPropertyOrder(2), YamlMember(Alias = "arguments", Order = 2)]
    public virtual EquatableList<string>? Arguments { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    [DataMember(Name = "environment", Order = 3), JsonPropertyName("environment"), JsonPropertyOrder(3), YamlMember(Alias = "environment", Order = 3)]
    public virtual EquatableDictionary<string, string>? Environment { get; set; }

}
