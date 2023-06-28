// Copyright © 2023-Present The Serverless Workflow Specification Authors
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

namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="ForEachStateDefinition"/>s
/// </summary>
public interface IForEachStateBuilder
    : IStateBuilder<ForEachStateDefinition>,
    IActionCollectionBuilder<IForEachStateBuilder>
{

    /// <summary>
    /// Configures the <see cref="ForEachStateDefinition"/> to use the specified expression when resolving the input collection
    /// </summary>
    /// <param name="expression">The expression to use when resolving the input collection</param>
    /// <returns>The configured <see cref="IForEachStateBuilder"/></returns>
    IForEachStateBuilder UseInputCollection(string expression);

    /// <summary>
    /// Configures the <see cref="ForEachStateDefinition"/> to use the specified expression when resolving the iteration parameter
    /// </summary>
    /// <param name="expression">The expression to use when resolving the iteration parameter</param>
    /// <returns>The configured <see cref="IForEachStateBuilder"/></returns>
    IForEachStateBuilder UseIterationParameter(string expression);

    /// <summary>
    /// Configures the <see cref="ForEachStateDefinition"/> to use the specified expression when resolving the output collection
    /// </summary>
    /// <param name="expression">The expression to use when resolving the output collection</param>
    /// <returns>The configured <see cref="IForEachStateBuilder"/></returns>
    IForEachStateBuilder UseOutputCollection(string expression);

    /// <summary>
    /// Configures how many iterations may run in parallel at the same time. Used if '<see cref="ActionExecutionMode"/>' has been set to '<see cref="ActionExecutionMode.Parallel"/>'
    /// </summary>
    /// <param name="batchSize">The maximum amount of iterations allowed</param>
    /// <returns>The configured <see cref="IForEachStateBuilder"/></returns>
    IForEachStateBuilder WithBatchSize(int? batchSize);

}
