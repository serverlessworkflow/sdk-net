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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents a workflow state that waits for a certain amount of time before transitioning to a next state
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Sleep)]
public class SleepStateDefinition
   : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="SleepStateDefinition"/>
    /// </summary>
    public SleepStateDefinition() : base(StateType.Sleep) { }

    /// <summary>
    /// Gets/sets the amount of time to delay when in this state
    /// </summary>
    [DataMember(Order = 6, Name = "duration"), JsonPropertyOrder(6), JsonPropertyName("duration"), YamlMember(Alias = "duration", Order = 6)]
    [JsonConverter(typeof(Iso8601TimeSpanConverter))]
    public virtual TimeSpan Duration { get; set; }

}