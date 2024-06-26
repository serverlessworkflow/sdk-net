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

using Neuroglia;

namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="ICallTaskDefinitionBuilder"/> interface
/// </summary>
/// <param name="functionName">The name of the function to call</param>
public class CallTaskDefinitionBuilder(string? functionName = null)
    : TaskDefinitionBuilder<ICallTaskDefinitionBuilder, CallTaskDefinition>, ICallTaskDefinitionBuilder
{

    /// <summary>
    /// Gets the name of the function to call
    /// </summary>
    protected virtual string? FunctionName { get; set; } = functionName;

    /// <summary>
    /// Gets a name/value mapping of the function's arguments, if any
    /// </summary>
    protected virtual EquatableDictionary<string, object>? FunctionArguments { get; set; }

    /// <inheritdoc/>
    public virtual ICallTaskDefinitionBuilder Function(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.FunctionName = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallTaskDefinitionBuilder With(string name, object value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        this.FunctionArguments ??= [];
        this.FunctionArguments[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICallTaskDefinitionBuilder With(IDictionary<string, object> arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        this.FunctionArguments = new(arguments);
        return this;
    }

    /// <inheritdoc/>
    public override CallTaskDefinition Build() 
    {
        if (string.IsNullOrWhiteSpace(this.FunctionName)) throw new NullReferenceException("The function to call is required");
        return this.Configure(new()
        {
            Call = this.FunctionName,
            With = this.FunctionArguments,
        });
    }

}
