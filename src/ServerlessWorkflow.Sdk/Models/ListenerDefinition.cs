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
/// Represents the configuration of an event listener
/// </summary>
[Description("Represents the configuration of an event listener")]
[DataContract]
public sealed record ListenerDefinition
{

    /// <summary>
    /// Gets/sets the listener's target
    /// </summary>
    [Description("The listener's target")]
    [Required]
    [DataMember(Order = 1, Name = "to"), JsonPropertyOrder(1), JsonPropertyName("to")]
    public required EventConsumptionStrategyDefinition To { get; init; }

    /// <summary>
    /// Gets/sets a string that specifies how events are read during the listen operation<para></para>
    /// See <see cref="EventReadMode"/>. Defaults to <see cref="EventReadMode.Data"/>
    /// </summary>
    [Description("A string that specifies how events are read during the listen operation. See EventReadMode. Defaults to EventReadMode.Data")]
    [DataMember(Order = 2, Name = "read"), JsonPropertyOrder(2), JsonPropertyName("read")]
    public string? Read { get; init; }

}
