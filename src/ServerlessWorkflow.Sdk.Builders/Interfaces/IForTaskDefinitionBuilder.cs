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
/// Defines the fundamentals of a service used to build <see cref="ForTaskDefinition"/>s
/// </summary>
public interface IForTaskDefinitionBuilder
    : ITaskDefinitionBuilder<IForTaskDefinitionBuilder, ForTaskDefinition>
{

    /// <summary>
    /// Sets the name of the variable to store the iteration item to
    /// </summary>
    /// <param name="variableName">The name of the variable to store the iteration item to</param>
    /// <returns>The configured <see cref="IForTaskDefinitionBuilder"/></returns>
    IForTaskDefinitionBuilder Each(string variableName);

    /// <summary>
    /// Sets the runtime expression used to resolve the collection to iterate
    /// </summary>
    /// <param name="expression">The runtime expression used to resolve the collection to iterate</param>
    /// <returns>The configured <see cref="IForTaskDefinitionBuilder"/></returns>
    IForTaskDefinitionBuilder In(string expression);

    /// <summary>
    /// Sets the name of the variable to store the iteration index to
    /// </summary>
    /// <param name="variableName">The name of the variable to store the iteration index to</param>
    /// <returns>The configured <see cref="IForTaskDefinitionBuilder"/></returns>
    IForTaskDefinitionBuilder At(string variableName);

    /// <summary>
    /// Configures the task to execute the specified tasks for each item in the specified collection
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the <see cref="TaskDefinition"/>s to execute</param>
    /// <returns>The configured <see cref="IForTaskDefinitionBuilder"/></returns>
    IForTaskDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup);

}
