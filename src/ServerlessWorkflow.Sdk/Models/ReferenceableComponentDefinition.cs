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
/// Represents the base class for all ServerlessWorkflow referenceable workflow components
/// </summary>
[DataContract]
public abstract record ReferenceableComponentDefinition
    : ComponentDefinition, IReferenceable
{

    /// <summary>
    /// Gets/sets an URI, if any, that reference the component's definition
    /// </summary>
    [DataMember(Order = 1, Name = "$ref"), JsonPropertyOrder(1), JsonPropertyName("$ref"), YamlMember(Order = 1, Alias = "$ref")]
    public virtual Uri? Ref { get; set; }

    /// <summary>
    /// Gets/sets the endpoint's authentication policy, if any
    /// </summary>
    [DataMember(Name = "authentication", Order = 2), JsonPropertyName("authentication"), JsonPropertyOrder(2), YamlMember(Alias = "authentication", Order = 2)]
    public virtual AuthenticationPolicyDefinition? Authentication { get; set; }

}