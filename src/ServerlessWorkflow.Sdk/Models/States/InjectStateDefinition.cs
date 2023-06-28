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
/// Represents the definition of an inject state
/// </summary>
[DataContract]
[DiscriminatorValue(StateType.Inject)]
public class InjectStateDefinition
    : StateDefinition
{

    /// <summary>
    /// Initializes a new <see cref="InjectStateDefinition"/>
    /// </summary>
    public InjectStateDefinition() : base(StateType.Inject) { }

    /// <summary>
    /// Gets/sets the object to inject within the state's data input and can be manipulated via filter
    /// </summary>
    [DataMember(Order = 6, Name = "data"), JsonPropertyOrder(6), JsonPropertyName("data"), YamlMember(Alias = "data", Order = 6)]
    public virtual object Data { get; set; } = null!;

}
