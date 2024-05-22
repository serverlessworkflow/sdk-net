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
/// Represents the definition of the parameters that control the randomness or variability of a delay, typically between retry attempts
/// </summary>
[DataContract]
public record JitterDefinition
{

    /// <summary>
    /// Gets/sets the minimum duration of the jitter range
    /// </summary>
    [DataMember(Name = "from", Order = 1), JsonPropertyName("from"), JsonPropertyOrder(1), YamlMember(Alias = "from", Order = 1)]
    public required virtual Duration From { get; set; }

    /// <summary>
    /// Gets/sets the maximum duration of the jitter range
    /// </summary>
    [DataMember(Name = "to", Order = 2), JsonPropertyName("to"), JsonPropertyOrder(2), YamlMember(Alias = "to", Order = 2)]
    public required virtual Duration To { get; set; }

}
