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
/// Defines the fundamentals of a service used to build <see cref="TaskDefinition"/>s
/// </summary>
public interface ITaskDefinitionBuilder
{

    /// <summary>
    /// Builds the configured <see cref="TaskDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="TaskDefinition"/></returns>
    TaskDefinition Build();

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="TaskDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of the implementing <see cref="ITaskDefinitionBuilder{TBuilder}"/></typeparam>
public interface ITaskDefinitionBuilder<TBuilder>
    : ITaskDefinitionBuilder
    where TBuilder : ITaskDefinitionBuilder<TBuilder>
{

    /// <summary>
    /// Configures the task to build to run only if the specified condition matches
    /// </summary>
    /// <param name="condition">A runtime expression that represents the condition to match for the task to run</param>
    /// <returns>The configured <see cref="ITaskDefinitionBuilder{TBuilder}"/></returns>
    TBuilder If(string condition);

    /// <summary>
    /// Sets the workflow's timeout
    /// </summary>
    /// <param name="name">The name of the workflow's timeout</param>
    /// <returns>The configured <see cref="ITaskDefinitionBuilder{TBuilder}"/></returns>
    TBuilder WithTimeout(string name);

    /// <summary>
    /// Sets the workflow's timeout
    /// </summary>
    /// <param name="timeout">The workflow's timeout</param>
    /// <returns>The configured <see cref="ITaskDefinitionBuilder{TBuilder}"/></returns>
    TBuilder WithTimeout(TimeoutDefinition timeout);

    /// <summary>
    /// Sets the workflow's timeout
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the workflow's timeout</param>
    /// <returns>The configured <see cref="ITaskDefinitionBuilder{TBuilder}"/></returns>
    TBuilder WithTimeout(Action<ITimeoutDefinitionBuilder> setup);

    /// <summary>
    /// Configures the task to build to then execute the specified flow directive
    /// </summary>
    /// <param name="directive">The flow directive to then execute</param>
    /// <returns>The configured <see cref="ITaskDefinitionBuilder{TBuilder}"/></returns>
    TBuilder Then(string directive);

}

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="TaskDefinition"/>s
/// </summary>
/// <typeparam name="TBuilder">The type of the implementing <see cref="ITaskDefinitionBuilder{TBuilder}"/></typeparam>
/// <typeparam name="TDefinition">The type of <see cref="TaskDefinition"/> to build and configure</typeparam>
public interface ITaskDefinitionBuilder<TBuilder, TDefinition>
    : ITaskDefinitionBuilder<TBuilder>
    where TBuilder : ITaskDefinitionBuilder<TBuilder>
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Builds the <see cref="TaskDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="TaskDefinition"/></returns>
    new TDefinition Build();

}
