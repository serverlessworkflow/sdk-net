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
/// Represents the definition of an output data model
/// </summary>
[DataContract]
public record OutputDataModelDefinition
{

    /// <summary>
    /// Gets/sets the schema, if any, that defines and describes the output data of a workflow or task
    /// </summary>
    [DataMember(Name = "schema", Order = 1), JsonPropertyName("schema"), JsonPropertyOrder(1), YamlMember(Alias = "schema", Order = 1)]
    public virtual SchemaDefinition? Schema { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to output specific data to the scope data
    /// </summary>
    [DataMember(Name = "as", Order = 3), JsonPropertyName("as"), JsonPropertyOrder(3), YamlMember(Alias = "as", Order = 3)]
    public virtual object? As { get; set; }

}
