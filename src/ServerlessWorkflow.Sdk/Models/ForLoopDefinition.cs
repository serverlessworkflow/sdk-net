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
/// Represents the definition of a loop that iterates over a range of values
/// </summary>
[DataContract]
public record ForLoopDefinition
{

    /// <summary>
    /// Gets/sets the name of the variable that represents each element in the collection during iteration
    /// </summary>
    [Required]
    [DataMember(Name = "each", Order = 1), JsonPropertyName("each"), JsonPropertyOrder(1), YamlMember(Alias = "each", Order = 1)]
    public required virtual string Each { get; set; }

    /// <summary>
    /// Gets/sets the runtime expression used to get the collection to iterate over
    /// </summary>
    [DataMember(Name = "in", Order = 2), JsonPropertyName("in"), JsonPropertyOrder(2), YamlMember(Alias = "in", Order = 2)]
    public required virtual string In { get; set; }

    /// <summary>
    /// Gets/sets the name of the variable used to hold the index of each element in the collection during iteration
    /// </summary>
    [DataMember(Name = "at", Order = 3), JsonPropertyName("at"), JsonPropertyOrder(3), YamlMember(Alias = "at", Order = 3)]
    public virtual string? At { get; set; }

    /// <summary>
    /// Gets/sets the definition of the data, if any, to pass to iterations to run
    /// </summary>
    [DataMember(Name = "input", Order = 4), JsonPropertyName("input"), JsonPropertyOrder(4), YamlMember(Alias = "input", Order = 4)]
    public virtual InputDataModelDefinition? Input { get; set; }

}
