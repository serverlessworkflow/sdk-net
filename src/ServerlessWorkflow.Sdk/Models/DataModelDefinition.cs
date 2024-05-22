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
/// Represents the definition of a data model
/// </summary>
[DataContract]
public abstract record DataModelDefinition
{

    /// <summary>
    /// Gets/sets the schema, if any, that defines and describes the defined data model
    /// </summary>
    [DataMember(Name = "schema", Order = 1), JsonPropertyName("schema"), JsonPropertyOrder(1), YamlMember(Alias = "schema", Order = 1)]
    public virtual SchemaDefinition? Schema { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to build the defined model using both input and scope data
    /// </summary>
    [DataMember(Name = "from", Order = 2), JsonPropertyName("from"), JsonPropertyOrder(2), JsonInclude, YamlMember(Alias = "from", Order = 2)]
    public virtual object? From { get; set; }

}
