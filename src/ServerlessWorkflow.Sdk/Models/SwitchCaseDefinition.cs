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
/// Represents the definition of a case within a switch task, defining a condition and corresponding tasks to execute if the condition is met
/// </summary>
[DataContract]
public record SwitchCaseDefinition
{

    /// <summary>
    /// Gets/sets the condition that determines whether or not the case should be executed in a switch task
    /// </summary>
    [DataMember(Name = "when", Order = 1), JsonPropertyName("when"), JsonPropertyOrder(1), YamlMember(Alias = "when", Order = 1)]
    public virtual string? When { get; set; }

    /// <summary>
    /// Gets/sets the transition to perform when the case matches
    /// </summary>
    [DataMember(Name = "then", Order = 2), JsonPropertyName("then"), JsonPropertyOrder(2), YamlMember(Alias = "then", Order = 2)]
    public virtual string? Then { get; set; }

}