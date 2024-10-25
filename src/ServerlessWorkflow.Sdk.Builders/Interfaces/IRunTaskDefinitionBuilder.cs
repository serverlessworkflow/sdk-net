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
/// Defines the fundamentals of a service used to build <see cref="RunTaskDefinition"/>s
/// </summary>
public interface IRunTaskDefinitionBuilder
    : ITaskDefinitionBuilder<IRunTaskDefinitionBuilder, RunTaskDefinition>
{

    /// <summary>
    /// Configures the task to run the specified container
    /// </summary>
    /// <returns>A new <see cref="IContainerProcessDefinitionBuilder"/></returns>
    IContainerProcessDefinitionBuilder Container();

    /// <summary>
    /// Configures the task to run the specified script
    /// </summary>
    /// <returns>A new <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder Script();

    /// <summary>
    /// Configures the task to run the specified shell command
    /// </summary>
    /// <returns>A new <see cref="IShellProcessDefinitionBuilder"/></returns>
    IShellProcessDefinitionBuilder Shell();

    /// <summary>
    /// Configures the task to run the specified workflow
    /// </summary>
    /// <returns>A new <see cref="IWorkflowProcessDefinitionBuilder"/></returns>
    IWorkflowProcessDefinitionBuilder Workflow();

    /// <summary>
    /// Configures whether the task to build should await the execution of the defined process
    /// </summary>
    /// <param name="await">A boolean indicating whether or not the task to build should await the execution of the defined process</param>
    /// <returns>The configured <see cref="IRunTaskDefinitionBuilder"/></returns>
    IRunTaskDefinitionBuilder Await(bool await);

}
