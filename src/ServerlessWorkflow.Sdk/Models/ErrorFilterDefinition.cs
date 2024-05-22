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
/// Represents the definition an an error filter
/// </summary>
[DataContract]
public record ErrorFilterDefinition
{

    /// <summary>
    /// Gets/sets a key/value mapping of the properties errors to filter must define
    /// </summary>
    [DataMember(Name = "with", Order = 1), JsonPropertyName("with"), JsonPropertyOrder(1), YamlMember(Alias = "with", Order = 1)]
    public virtual EquatableDictionary<string, object>? With { get; set; }

}
