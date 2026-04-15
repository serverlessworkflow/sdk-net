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
/// Represents the configuration of an event consumption strategy
/// </summary>
[Description("Represents the configuration of an event consumption strategy")]
[DataContract]
public sealed record EventConsumptionStrategyDefinition
{

    /// <summary>
    /// Gets/sets a list containing all the events that must be consumed, if any
    /// </summary>
    [Description("A list containing all the events that must be consumed, if any")]
    [DataMember(Order = 1, Name = "all"), JsonPropertyOrder(1), JsonPropertyName("all")]
    public EquatableList<EventFilterDefinition>? All { get; init; }

    /// <summary>
    /// Gets/sets a list containing any of the events to consume, if any.<para></para>
    /// If empty, listens to all incoming events, and requires <see cref="Until"/> to be set.
    /// </summary>
    [Description("A list containing any of the events to consume, if any. If empty, listens to all incoming events, and requires Until to be set.")]
    [DataMember(Order = 2, Name = "any"), JsonPropertyOrder(2), JsonPropertyName("any")]
    public EquatableList<EventFilterDefinition>? Any { get; init; }

    /// <summary>
    /// Gets/sets the single event to consume
    /// </summary>
    [Description("The single event to consume")]
    [DataMember(Order = 3, Name = "one"), JsonPropertyOrder(3), JsonPropertyName("one")]
    public EventFilterDefinition? One { get; init; }

    /// <summary>
    /// Gets/sets the condition or the consumption strategy that defines the events that must be consumed to stop listening
    /// </summary>
    [Description("The condition or the consumption strategy that defines the events that must be consumed to stop listening")]
    [DataMember(Order = 4, Name = "until"), JsonPropertyOrder(4), JsonPropertyName("until"), JsonConverter(typeof(OneOfJsonConverter<EventConsumptionStrategyDefinition, string>))]
    public OneOf<EventConsumptionStrategyDefinition, string>? Until { get; init; }

}
