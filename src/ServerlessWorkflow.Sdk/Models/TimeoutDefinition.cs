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
/// Represents the definition of a timeout
/// </summary>
[Description("Represents the definition of a timeout")]
[DataContract]
public sealed record TimeoutDefinition
{

    /// <summary>
    /// Gets/sets the duration after which to timeout
    /// </summary>
    [Description("The duration after which to timeout")]
    [Required]
    [DataMember(Order = 1, Name = "after"), JsonPropertyOrder(1), JsonPropertyName("after"), JsonConverter(typeof(OneOfJsonConverter<Duration, string>))]
    public required OneOf<Duration, string> After { get; init; }

}