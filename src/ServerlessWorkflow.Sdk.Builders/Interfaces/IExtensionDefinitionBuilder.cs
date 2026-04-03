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
/// Defines the fundamentals of a service used to build <see cref="ExtensionDefinition"/>s
/// </summary>
public interface IExtensionDefinitionBuilder
{

    /// <summary>
    /// Configures the extension to build to extend the specified task type
    /// </summary>
    /// <param name="taskType">The type of task to extend</param>
    /// <returns>The configured <see cref="IExtensionDefinitionBuilder"/></returns>
    IExtensionDefinitionBuilder Extend(string taskType);

    /// <summary>
    /// Configures the extension to build to extend the specified task type
    /// </summary>
    /// <param name="when">A runtime expression used to determine whether or not the extension applies</param>
    /// <returns>The configured <see cref="IExtensionDefinitionBuilder"/></returns>
    IExtensionDefinitionBuilder When(string when);

    /// <summary>
    /// Configures the tasks to run before the extended task type
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tasks to run before the extended task type</param>
    /// <returns>The configured <see cref="IExtensionDefinitionBuilder"/></returns>
    IExtensionDefinitionBuilder Before(Action<ITaskDefinitionMapBuilder> setup);

    /// <summary>
    /// Configures the tasks to run after the extended task type
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the tasks to run after the extended task type</param>
    /// <returns>The configured <see cref="IExtensionDefinitionBuilder"/></returns>
    IExtensionDefinitionBuilder After(Action<ITaskDefinitionMapBuilder> setup);

    /// <summary>
    /// Builds the configured <see cref="ExtensionDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="ExtensionDefinition"/></returns>
    ExtensionDefinition Build();

}

/// <summary>
/// Defines extensions for <see cref="IExtensionDefinitionBuilder"/>s
/// </summary>
public static class IExtensionDefinitionBuilderExtensions
{

    /// <summary>
    /// Configure the extension to build to extend all tasks
    /// </summary>
    /// <param name="builder">The <see cref="IExtensionDefinitionBuilder"/> to configure</param>
    /// <returns>The configured <see cref="IExtensionDefinitionBuilder"/></returns>
    public static IExtensionDefinitionBuilder ExtendAll(this IExtensionDefinitionBuilder builder) => builder.Extend("all");

    /// <summary>
    /// Configure the extension to build to extend call tasks
    /// </summary>
    /// <param name="builder">The <see cref="IExtensionDefinitionBuilder"/> to configure</param>
    /// <returns>The configured <see cref="IExtensionDefinitionBuilder"/></returns>
    public static IExtensionDefinitionBuilder ExtendCallTasks(this IExtensionDefinitionBuilder builder) => builder.Extend(TaskType.Call);

}