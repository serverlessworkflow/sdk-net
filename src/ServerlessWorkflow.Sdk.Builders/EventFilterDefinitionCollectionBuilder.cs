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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IEventFilterDefinitionCollectionBuilder"/> interface
/// </summary>
public class EventFilterDefinitionCollectionBuilder
    : IEventFilterDefinitionCollectionBuilder
{

    /// <summary>
    /// Gets/sets the filters the collection to build is made out of
    /// </summary>
    protected EquatableList<EventFilterDefinition>? Filters { get; set; }

    /// <inheritdoc/>
    public virtual IEventFilterDefinitionCollectionBuilder Event(EventFilterDefinition filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        this.Filters ??= [];
        this.Filters.Add(filter);
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventFilterDefinitionCollectionBuilder Event(Action<IEventFilterDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new EventFilterDefinitionBuilder();
        setup(builder);
        var filter = builder.Build();
        this.Filters ??= [];
        this.Filters.Add(filter);
        return this;
    }

    /// <inheritdoc/>
    public virtual EquatableList<EventFilterDefinition> Build()
    {
        if (this.Filters == null || this.Filters.Count < 1) throw new NullReferenceException("The collection must contain at least one event filter");
        return this.Filters;
    }

}
