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
/// Defines the fundamentals of a service used to build <see cref="ScriptProcessDefinition"/>s
/// </summary>
public interface IScriptProcessDefinitionBuilder
    : IProcessDefinitionBuilder<ScriptProcessDefinition>
{

    /// <summary>
    /// Sets the language of the script to run
    /// </summary>
    /// <param name="language">The language of the script to run</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithLanguage(string language);

    /// <summary>
    /// Sets the code of the script to run
    /// </summary>
    /// <param name="code">The script's code</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithCode(string code);

    /// <summary>
    /// Sets the source of the script to run
    /// </summary>
    /// <param name="source">A uri that reference the script's source</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithSource(Uri source);

    /// <summary>
    /// Sets the source of the script to run
    /// </summary>
    /// <param name="setup">An <see cref="Action{T}"/> used to setup the script's source</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithSource(Action<IExternalResourceDefinitionBuilder> setup);

    /// <summary>
    /// Adds a new argument to execute the script with
    /// </summary>
    /// <param name="name">The name of the argument to use</param>
    /// <param name="value">The value of the argument to use</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithArgument(string name, object value);

    /// <summary>
    /// Sets the arguments of the script to execute
    /// </summary>
    /// <param name="arguments">A name/value mapping of the arguments to use</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithArguments(IDictionary<string, object> arguments);

    /// <summary>
    /// Adds the specified environment variable to the process
    /// </summary>
    /// <param name="name">The environment variable's name</param>
    /// <param name="value">The environment variable's value</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithEnvironment(string name, string value);

    /// <summary>
    /// Sets the process's environment variables
    /// </summary>
    /// <param name="environment">A name/value mapping of the environment variables to use</param>
    /// <returns>The configured <see cref="IScriptProcessDefinitionBuilder"/></returns>
    IScriptProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment);

}
