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
/// Represents the definition of a script evaluation process
/// </summary>
[DataContract]
public record ScriptProcessDefinition
    : ProcessDefinition
{

    /// <summary>
    /// Gets/sets the language of the script to run
    /// </summary>
    [DataMember(Name = "language", Order = 1), JsonPropertyName("language"), JsonPropertyOrder(1), YamlMember(Alias = "language", Order = 1)]
    public required virtual string Language { get; set; }

    /// <summary>
    /// Gets/sets the script's code. Required if <see cref="Source"/> has not been set.
    /// </summary>
    [DataMember(Name = "code", Order = 2), JsonPropertyName("code"), JsonPropertyOrder(2), YamlMember(Alias = "code", Order = 2)]
    public virtual string? Code { get; set; }

    /// <summary>
    /// Gets the the script's source. Required if <see cref="Code"/> has not been set.
    /// </summary>
    [DataMember(Name = "source", Order = 3), JsonPropertyName("source"), JsonPropertyOrder(3), YamlMember(Alias = "source", Order = 3)]
    public virtual ExternalResourceDefinition? Source { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the environment variables, if any, to use when running the configured process
    /// </summary>
    [DataMember(Name = "environment", Order = 4), JsonPropertyName("environment"), JsonPropertyOrder(4), YamlMember(Alias = "environment", Order = 4)]
    public virtual EquatableDictionary<string, string>? Environment { get; set; }


}
