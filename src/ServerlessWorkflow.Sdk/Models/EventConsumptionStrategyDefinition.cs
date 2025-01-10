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
[DataContract]
public record EventConsumptionStrategyDefinition
{

    /// <summary>
    /// Gets/sets a list containing all the events that must be consumed, if any
    /// </summary>
    [DataMember(Name = "all", Order = 1), JsonPropertyName("all"), JsonPropertyOrder(1), YamlMember(Alias = "all", Order = 1)]
    public virtual EquatableList<EventFilterDefinition>? All { get; set; }

    /// <summary>
    /// Gets/sets a list containing any of the events to consume, if any.<para></para>
    /// If empty, listens to all incoming events, and requires <see cref="Until"/> to be set.
    /// </summary>
    [DataMember(Name = "any", Order = 2), JsonPropertyName("any"), JsonPropertyOrder(2), YamlMember(Alias = "any", Order = 2)]
    public virtual EquatableList<EventFilterDefinition>? Any { get; set; }

    /// <summary>
    /// Gets/sets the single event to consume
    /// </summary>
    [DataMember(Name = "one", Order = 3), JsonPropertyName("one"), JsonPropertyOrder(3), YamlMember(Alias = "one", Order = 3)]
    public virtual EventFilterDefinition? One { get; set; }

    /// <summary>
    /// Gets/sets the condition or the consumption strategy that defines the events that must be consumed to stop listening
    /// </summary>
    [DataMember(Name = "until", Order = 4), JsonInclude, JsonPropertyName("until"), JsonPropertyOrder(4), YamlMember(Alias = "until", Order = 4)]
    protected virtual OneOf<EventConsumptionStrategyDefinition, string>? UntilValue { get; set; }

    /// <summary>
    /// Gets/sets the consumption strategy, if any, that defines the events that must be consumed to stop listening
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual EventConsumptionStrategyDefinition? Until
    {
        get => this.UntilValue?.T1Value;
        set => this.UntilValue = value!;
    }

    /// <summary>
    /// Gets/sets a runtime expression, if any, that represents the condition that must be met to stop listening
    /// </summary>
    [IgnoreDataMember, JsonIgnore, YamlIgnore]
    public virtual string? UntilExpression
    {
        get => this.UntilValue?.T2Value;
        set => this.UntilValue = value!;
    }

}
