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
/// Represents the default implementation of the <see cref="IShellProcessDefinitionBuilder"/> interface
/// </summary>
public sealed class ShellProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ShellProcessDefinition>, IShellProcessDefinitionBuilder
{

    string? command;
    EquatableList<string>? arguments;
    EquatableDictionary<string, string>? environment;

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        this.command = command;
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithArgument(string argument)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(argument);
        this.arguments ??= [];
        this.arguments.Add(argument);
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithArguments(IEnumerable<string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.arguments = [.. arguments];
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.environment ??= [];
        this.environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public IShellProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.environment = [.. environment];
        return this;
    }

    /// <inheritdoc/>
    public override ShellProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.command)) throw new NullReferenceException("The shell command to execute must be set");
        return new()
        {
            Command = this.command,
            Arguments = this.arguments,
            Environment = this.environment
        };
    }

}
