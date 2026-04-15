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
/// Represents the definition of an event correlation key
/// </summary>
[Description("Represents the definition of an event correlation key")]
[DataContract]
public sealed record CorrelationKeyDefinition
{

    /// <summary>
    /// Gets/sets a runtime expression used to extract the correlation key value from events.
    /// </summary>
    [Description("A runtime expression used to extract the correlation key value from events.")]
    [DataMember(Order = 1, Name = "from"), JsonPropertyOrder(1), JsonPropertyName("from")]
    public required string From { get; init; }

    /// <summary>
    /// Gets/sets a constant or a runtime expression, if any, used to determine whether or not the extracted correlation key value matches expectations and should be correlated. If not set, the first extracted value will be used as the correlation key's expectation.
    /// </summary>
    [Description("A constant or a runtime expression, if any, used to determine whether or not the extracted correlation key value matches expectations and should be correlated. If not set, the first extracted value will be used as the correlation key's expectation.")]
    [DataMember(Order = 2, Name = "expect"), JsonPropertyOrder(2), JsonPropertyName("expect")]
    public string? Expect { get; init; }

}
