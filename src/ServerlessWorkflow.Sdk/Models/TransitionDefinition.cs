// Copyright © 2023-Present The Serverless Workflow Specification Authors
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
/// Represents an object used to define a state transition
/// </summary>
[DataContract]
public class TransitionDefinition
    : StateOutcomeDefinition
{

    /// <summary>
    /// Gets/sets the name of state to transition to
    /// </summary>
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "nextState", IsRequired = true), JsonPropertyName("nextState"), YamlMember(Alias = "nextState")]
    public virtual string NextState { get; set; } = null!;

    /// <summary>
    /// Gets/sets an <see cref="IEnumerable{T}"/> containing the events to be produced before the transition happens
    /// </summary>
    [DataMember(Order = 2, Name = "produceEvents"), JsonPropertyName("produceEvents"), YamlMember(Alias = "produceEvents")]
    public virtual List<ProduceEventDefinition>? ProduceEvents { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not to trigger workflow compensation before the transition is taken. Default is false
    /// </summary>
    [DataMember(Order = 3, Name = "compensate"), JsonPropertyName("compensate"), YamlMember(Alias = "compensate")]
    public virtual bool Compensate { get; set; } = false;

}
