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
/// Represents the definition of an input data model
/// </summary>
[Description("Represents the definition of an input data model")]
[DataContract]
public sealed record InputDataModelDefinition
{

    /// <summary>
    /// Gets/sets the schema, if any, that defines and describes the input data of a workflow or task
    /// </summary>
    [Description("The schema, if any, that defines and describes the input data of a workflow or task")]
    [DataMember(Order = 1, Name = "schema"), JsonPropertyOrder(1), JsonPropertyName("schema")]
    public SchemaDefinition? Schema { get; init; }

    /// <summary>
    /// Gets/sets a runtime expression, if any, used to build the workflow or task input data based on both input and scope data
    /// </summary>
    [Description("A runtime expression, if any, used to build the workflow or task input data based on both input and scope data")]
    [DataMember(Order = 2, Name = "from"), JsonPropertyOrder(2), JsonPropertyName("from"), JsonConverter(typeof(OneOfJsonConverter<JsonObject, string>))]
    public OneOf<JsonObject, string>? From { get; init; }

}
