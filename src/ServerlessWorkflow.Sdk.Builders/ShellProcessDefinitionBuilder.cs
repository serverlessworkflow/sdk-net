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
using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IShellProcessDefinitionBuilder"/> interface
/// </summary>
public class ShellProcessDefinitionBuilder
    : ProcessDefinitionBuilder<ShellProcessDefinition>, IShellProcessDefinitionBuilder
{

    /// <summary>
    /// Gets the command to execute
    /// </summary>
    protected virtual string? Command { get; set; }

    /// <summary>
    /// Gets the arguments, if any, of the command to execute
    /// </summary>
    protected virtual EquatableList<string>? Arguments { get; set; }

    /// <summary>
    /// Gets/sets the environment variables, if any, of the shell command to execute
    /// </summary>
    protected virtual EquatableDictionary<string, string>? Environment { get; set; }

    /// <inheritdoc/>
    public virtual IShellProcessDefinitionBuilder WithCommand(string command)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(command);
        this.Command = command;
        return this;
    }

    /// <inheritdoc/>
    public virtual IShellProcessDefinitionBuilder WithArgument(string argument)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(argument);
        this.Arguments ??= [];
        this.Arguments.Add(argument);
        return this;
    }

    /// <inheritdoc/>
    public virtual IShellProcessDefinitionBuilder WithArguments(IEnumerable<string> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.Arguments = new(arguments);
        return this;
    }

    /// <inheritdoc/>
    public virtual IShellProcessDefinitionBuilder WithEnvironment(string name, string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.Environment ??= [];
        this.Environment[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IShellProcessDefinitionBuilder WithEnvironment(IDictionary<string, string> environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        this.Environment = new(environment);
        return this;
    }

    /// <inheritdoc/>
    public override ShellProcessDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(this.Command)) throw new NullReferenceException("The shell command to execute must be set");
        return new() 
        { 
            Command = this.Command,
            Arguments = this.Arguments,
            Environment = this.Environment
        };
    }

}
