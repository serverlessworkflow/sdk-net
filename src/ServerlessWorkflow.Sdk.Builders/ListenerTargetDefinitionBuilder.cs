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

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IListenerTargetDefinitionBuilder"/> interface
/// </summary>
public class ListenerTargetDefinitionBuilder
    : IListenerTargetDefinitionBuilder
{

    IEventFilterDefinitionCollectionBuilder? allEvents;
    IEventFilterDefinitionCollectionBuilder? anyEvents;
    IEventFilterDefinitionBuilder? singleEvent;
    string? untilExpression;
    EventConsumptionStrategyDefinition? untilEvents;

    /// <inheritdoc/>
    public IEventFilterDefinitionCollectionBuilder All()
    {
        allEvents = new EventFilterDefinitionCollectionBuilder();
        return allEvents;
    }

    /// <inheritdoc/>
    public IEventFilterDefinitionCollectionBuilder Any()
    {
        anyEvents = new EventFilterDefinitionCollectionBuilder();
        return anyEvents;
    }

    /// <inheritdoc/>
    public IEventFilterDefinitionBuilder One()
    {
        singleEvent = new EventFilterDefinitionBuilder();
        return singleEvent;
    }

    /// <inheritdoc/>
    public void Until(string expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        if (anyEvents == null) throw new Exception("The until clause can only be specified when the strategy is used to consume any events");
        untilExpression = expression;
    }

    /// <inheritdoc/>
    public void Until(Action<IListenerTargetDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        if (anyEvents == null) throw new Exception("The until clause can only be specified when the strategy is used to consume any events");
        var builder = new ListenerTargetDefinitionBuilder();
        setup(builder);
        untilEvents = builder.Build();
    }

    /// <inheritdoc/>
    public EventConsumptionStrategyDefinition Build()
    {
        if (allEvents == null && anyEvents == null && singleEvent == null) throw new NullReferenceException("The target must be defined");
        OneOf<EventConsumptionStrategyDefinition, string>? until = null;
        if (untilExpression != null) until = untilExpression;
        else if (untilEvents != null) until = untilEvents;
        return new()
        {
            All = allEvents?.Build(),
            Any = anyEvents?.Build(),
            One = singleEvent?.Build(),
            Until = until
        };
    }

}
