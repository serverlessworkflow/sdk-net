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
/// Represents the definition of a task used to call a predefined function
/// </summary>
[DataContract]
public record CallTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public override string Type => TaskType.Call;

    /// <summary>
    /// Gets/sets the reference to the function to call
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Name = "call", Order = 1), JsonPropertyName("call"), JsonPropertyOrder(1), YamlMember(Alias = "call", Order = 1)]
    public required virtual string Call { get; set; }

    /// <summary>
    /// Gets/sets a key/value mapping of the call's arguments
    /// </summary>
    [DataMember(Name = "with", Order = 2), JsonPropertyName("with"), JsonPropertyOrder(2), YamlMember(Alias = "with", Order = 2)]
    public virtual EquatableDictionary<string, object>? With { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to wait for the called function to return. Defaults to true.
    /// </summary>
    [DataMember(Name = "await", Order = 3), JsonPropertyName("await"), JsonPropertyOrder(3), YamlMember(Alias = "await", Order = 3)]
    public virtual bool? Await { get; set; }

}
