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
/// Defines the fundamentals of a service used to build <see cref="ForkTaskDefinition"/>s
/// </summary>
public interface IForkTaskDefinitionBuilder
    : ITaskDefinitionBuilder<IForkTaskDefinitionBuilder, ForkTaskDefinition>
{

    /// <summary>
    /// Configures the tasks to perform concurrently
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tasks to perform concurrently</param>
    /// <returns>The configured <see cref="IForkTaskDefinitionBuilder"/></returns>
    IForkTaskDefinitionBuilder Branch(Action<ITaskDefinitionMapBuilder> setup);

    /// <summary>
    /// Configures the concurrent tasks to race each other, with only one winner setting the task's output
    /// </summary>
    /// <returns>The configured <see cref="IForkTaskDefinitionBuilder"/></returns>
    IForkTaskDefinitionBuilder Compete();

}