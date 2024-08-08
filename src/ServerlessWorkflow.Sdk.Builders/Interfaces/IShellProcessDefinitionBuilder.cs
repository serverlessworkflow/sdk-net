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

using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Defines the fundamentals of a service used to build shell-based <see cref="RunTaskDefinition"/>s
/// </summary>
public interface IShellProcessDefinitionBuilder
    : IProcessDefinitionBuilder<ShellProcessDefinition>
{

    /// <summary>
    /// Configures the task to execute the specified shell command
    /// </summary>
    /// <param name="command">The shell command to execute. Supports runtime expressions</param>
    /// <returns>The configured <see cref="IShellProcessDefinitionBuilder"/></returns>
    IShellProcessDefinitionBuilder WithCommand(string command);

    /// <summary>
    /// Adds a new argument to execute the shell command with
    /// </summary>
    /// <param name="argument">The argument to use</param>
    /// <returns>The configured <see cref="IShellProcessDefinitionBuilder"/></returns>
    IShellProcessDefinitionBuilder WithArgument(string argument);

    /// <summary>
    /// Sets the arguments of the shell command to execute
    /// </summary>
    /// <param name="arguments">A list of the arguments to use</param>
    /// <returns>The configured <see cref="IShellProcessDefinitionBuilder"/></returns>
    IShellProcessDefinitionBuilder WithArguments(IEnumerable<string> arguments);

    /// <summary>
    /// Adds the specified environment variable to the process
    /// </summary>
    /// <param name="name">The environment variable's name</param>
    /// <param name="value">The environment variable's value</param>
    /// <returns>The configured <see cref="IShellProcessDefinitionBuilder"/></returns>
    IShellProcessDefinitionBuilder WithEnvironment(string name, string value);

    /// <summary>
    /// Sets the process's environment variables
    /// </summary>
    /// <param name="environment">A name/value mapping of the environment variables to use</param>
    /// <returns>The configured <see cref="IShellProcessDefinitionBuilder"/></returns>
    IShellProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment);

}
