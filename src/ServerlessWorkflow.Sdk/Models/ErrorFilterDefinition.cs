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
[Description("Represents the definition an an error filter")]
[DataContract]
public sealed record ErrorFilterDefinition
{

    /// <summary>
    /// Gets/sets a key/value mapping of the properties errors to filter must define
    /// </summary>
    [Description("A key/value mapping of the properties errors to filter must define")]
    [DataMember(Order = 1, Name = "with"), JsonPropertyOrder(1), JsonPropertyName("with")]
    public JsonObject? With { get; init; }

}
