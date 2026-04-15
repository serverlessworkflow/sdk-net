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
/// Represents the default implementation of the <see cref="ICallTaskDefinitionBuilder"/> interface
/// </summary>
/// <param name="functionName">The name of the function to call</param>
public sealed class CallTaskDefinitionBuilder(string? functionName = null)
    : TaskDefinitionBuilder<ICallTaskDefinitionBuilder, CallTaskDefinition>, ICallTaskDefinitionBuilder
{

    string? functionName = functionName;
    JsonObject? functionArguments;

    /// <inheritdoc/>
    public ICallTaskDefinitionBuilder Function(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        functionName = name;
        return this;
    }

    /// <inheritdoc/>
    public ICallTaskDefinitionBuilder With(string name, JsonNode value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        functionArguments ??= [];
        functionArguments[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public ICallTaskDefinitionBuilder With(JsonObject arguments)
    {
        ArgumentNullException.ThrowIfNull(arguments);
        functionArguments = arguments;
        return this;
    }

    /// <inheritdoc/>
    public override CallTaskDefinition Build() 
    {
        if (string.IsNullOrWhiteSpace(functionName)) throw new NullReferenceException("The function to call is required");
        return Configure(new()
        {
            Call = functionName,
            With = functionArguments,
        });
    }

}
