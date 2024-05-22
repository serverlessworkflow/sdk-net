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

    /// <summary>
    /// Gets/sets a list containing all the events that must be listened to, if any
    /// </summary>
    protected IEventFilterDefinitionCollectionBuilder? AllEvents { get; set; }

    /// <summary>
    /// Gets/sets a list containing any of the events to listen to, if any
    /// </summary>
    protected IEventFilterDefinitionCollectionBuilder? AnyEvents { get; set; }

    /// <summary>
    /// Gets/sets the single event to listen to
    /// </summary>
    protected IEventFilterDefinitionBuilder? SingleEvent { get; set; }

    /// <inheritdoc/>
    public virtual IEventFilterDefinitionCollectionBuilder All()
    {
        this.AllEvents = new EventFilterDefinitionCollectionBuilder();
        return this.AllEvents;
    }

    /// <inheritdoc/>
    public virtual IEventFilterDefinitionCollectionBuilder Any()
    {
        this.AnyEvents = new EventFilterDefinitionCollectionBuilder();
        return this.AnyEvents;
    }

    /// <inheritdoc/>
    public virtual IEventFilterDefinitionBuilder One()
    {
        this.SingleEvent = new EventFilterDefinitionBuilder();
        return this.SingleEvent;
    }

    /// <inheritdoc/>
    public virtual EventConsumptionStrategyDefinition Build()
    {
        if (this.AllEvents == null && this.AnyEvents == null && this.SingleEvent == null) throw new NullReferenceException("The target must be defined");
        return new()
        {
            All = this.AllEvents?.Build(),
            Any = this.AnyEvents?.Build(),
            One = this.SingleEvent?.Build()
        };
    }

}
