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
[Description("Represents the definition of a task used to call a predefined function")]
[DataContract]
public sealed record CallTaskDefinition
    : TaskDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Type => TaskType.Call;

    /// <summary>
    /// Gets/sets the reference to the function to call
    /// </summary>
    [Description("The reference to the function to call")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "call"), JsonPropertyOrder(1), JsonPropertyName("call")]
    public required string Call { get; init; }

    /// <summary>
    /// Gets/sets a key/value mapping, if any, of the call's arguments
    /// </summary>
    [Description("A key/value mapping, if any, of the call's arguments")]
    [DataMember(Order = 2, Name = "with"), JsonPropertyOrder(2), JsonPropertyName("with")]
    public JsonObject? With { get; init; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to wait for the called function to return. Defaults to true.
    /// </summary>
    [Description("A boolean indicating whether or not to wait for the called function to return. Defaults to true.")]
    [DataMember(Order = 3, Name = "await"), JsonPropertyOrder(3), JsonPropertyName("await")]
    public bool? Await { get; init; }

}
