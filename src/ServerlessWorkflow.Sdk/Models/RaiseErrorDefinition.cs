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
/// Represents the definition of the error to raise
/// </summary>
[Description("Represents the definition of the error to raise")]
[DataContract]
public sealed record RaiseErrorDefinition
{

    /// <summary>
    /// Gets/sets the error to raise
    /// </summary>
    [Description("The error to raise")]
    [Required]
    [DataMember(Order = 1, Name = "error"), JsonPropertyOrder(1), JsonPropertyName("error"), JsonConverter(typeof(OneOfJsonConverter<ErrorDefinition, string>))]
    public required OneOf<ErrorDefinition, string> Error { get; init; }

}
