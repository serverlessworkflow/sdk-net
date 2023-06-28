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
/// Represents the definition of a state's data filter
/// </summary>
[DataContract]
public class StateDataFilterDefinition
    : IExtensible
{

    /// <summary>
    /// Gets/sets an expression to filter the states data input
    /// </summary>
    [DataMember(Order = 1, Name = "input"), JsonPropertyName("input"), YamlMember(Alias = "input")]
    public virtual string? Input { get; set; }

    /// <summary>
    /// Gets/sets an expression that filters the states data output
    /// </summary>
    [DataMember(Order = 2), JsonPropertyName("output"), YamlMember(Alias = "output")]
    public virtual string? Output { get; set; }

    /// <inheritdoc/>
    [DataMember(Order = 3, Name = "extensionData"), JsonExtensionData]
    public virtual IDictionary<string, object>? ExtensionData { get; set; }

}