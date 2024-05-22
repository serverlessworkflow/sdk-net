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
/// Defines the fundamentals of a service used to build collections of <see cref="EventFilterDefinition"/>s
/// </summary>
public interface IEventFilterDefinitionCollectionBuilder
{

    /// <summary>
    /// Adds the specified event filter to the collection
    /// </summary>
    /// <param name="filter">The filter to add</param>
    /// <returns>The configured <see cref="IEventFilterDefinitionCollectionBuilder"/></returns>
    IEventFilterDefinitionCollectionBuilder Event(EventFilterDefinition filter);

    /// <summary>
    /// Adds the specified event filter to the collection
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the filter to add</param>
    /// <returns>The configured <see cref="IEventFilterDefinitionCollectionBuilder"/></returns>
    IEventFilterDefinitionCollectionBuilder Event(Action<IEventFilterDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured collection of <see cref="EventFilterDefinition"/>s
    /// </summary>
    /// <returns>A new collection of <see cref="EventFilterDefinition"/>s</returns>
    EquatableList<EventFilterDefinition> Build();

}
