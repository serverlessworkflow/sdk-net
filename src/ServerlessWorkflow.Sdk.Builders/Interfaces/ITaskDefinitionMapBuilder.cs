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
/// Defines the fundamentals of a service used to build a configure <see cref="TaskDefinition"/> collections
/// </summary>
/// <typeparam name="TBuilder">The type of the <see cref="ITaskDefinitionMapBuilder{TBuilder}"/></typeparam>
public interface ITaskDefinitionMapBuilder<TBuilder>
    where TBuilder : ITaskDefinitionMapBuilder<TBuilder>
{

    /// <summary>
    /// Adds a new task with the specified name and configuration setup to the builder.
    /// </summary>
    /// <param name="name">The name of the task to add.</param>
    /// <param name="setup">An action to configure the task definition.</param>
    /// <returns>The current instance of the task definition mapping builder.</returns>
    TBuilder Do(string name, Action<IGenericTaskDefinitionBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="TaskDefinition"/> collection
    /// </summary>
    /// <returns>A new mapping of <see cref="TaskDefinition"/>s by name</returns>
    Map<string, TaskDefinition> Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build a configure <see cref="TaskDefinition"/> collections
/// </summary>
public interface ITaskDefinitionMapBuilder
    : ITaskDefinitionMapBuilder<ITaskDefinitionMapBuilder>
{



}
