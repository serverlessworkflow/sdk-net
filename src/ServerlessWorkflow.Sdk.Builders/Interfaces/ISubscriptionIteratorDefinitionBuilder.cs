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
/// Defines the fundamentals of a service used to build <see cref="SubscriptionIteratorDefinition"/>s
/// </summary>
public interface ISubscriptionIteratorDefinitionBuilder
{

    /// <summary>
    /// Sets the name of the variable used to store the item being enumerated
    /// </summary>
    /// <param name="item">The name of the variable used to store the item being enumerated</param>
    /// <returns>The configured <see cref="ISubscriptionIteratorDefinitionBuilder"/></returns>
    ISubscriptionIteratorDefinitionBuilder Item(string item);

    /// <summary>
    /// Sets the name of the variable used to store the index of the item being enumerated
    /// </summary>
    /// <param name="at">The name of the variable used to store the index of the item being enumerated</param>
    /// <returns>The configured <see cref="ISubscriptionIteratorDefinitionBuilder"/></returns>
    ISubscriptionIteratorDefinitionBuilder At(string at);

    /// <summary>
    /// Sets the tasks to execute for each event or message consumed
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the tasks to execute for each event or message consumed</param>
    /// <returns>The configured <see cref="ISubscriptionIteratorDefinitionBuilder"/></returns>
    ISubscriptionIteratorDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup);

    /// <summary>
    /// Configures the output data of each item
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the output data</param>
    /// <returns>The configured <see cref="ISubscriptionIteratorDefinitionBuilder"/></returns>
    ISubscriptionIteratorDefinitionBuilder Output(Action<IOutputDataModelDefinitionBuilder> setup);

    /// <summary>
    /// Configures the data exported by each item
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to configure the exported data</param>
    /// <returns>The configured <see cref="ISubscriptionIteratorDefinitionBuilder"/></returns>
    ISubscriptionIteratorDefinitionBuilder Export(Action<IOutputDataModelDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="SubscriptionIteratorDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="SubscriptionIteratorDefinition"/></returns>
    SubscriptionIteratorDefinition Build();

}
