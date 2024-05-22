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
/// Defines the fundamentals of a service used to build <see cref="EventConsumptionStrategyDefinition"/>s
/// </summary>
public interface IListenerTargetDefinitionBuilder
{

    /// <summary>
    /// Configures the task to listen for all of the defined events
    /// </summary>
    /// <returns>A new <see cref="IEventFilterDefinitionCollectionBuilder"/></returns>
    IEventFilterDefinitionCollectionBuilder All();

    /// <summary>
    /// Configures the task to listen for any of the defined events
    /// </summary>
    /// <returns>A new <see cref="IEventFilterDefinitionCollectionBuilder"/></returns>
    IEventFilterDefinitionCollectionBuilder Any();

    /// <summary>
    /// Configures the task to listen for one single event
    /// </summary>
    /// <returns>A new <see cref="IEventFilterDefinitionBuilder"/></returns>
    IEventFilterDefinitionBuilder One();

    /// <summary>
    /// Builds the configured <see cref="EventConsumptionStrategyDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="EventConsumptionStrategyDefinition"/></returns>
    EventConsumptionStrategyDefinition Build();

}
