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
/// Represents the default implementation of the <see cref="IForTaskDefinitionBuilder"/> interface
/// </summary>
public sealed class ForTaskDefinitionBuilder
    : TaskDefinitionBuilder<IForTaskDefinitionBuilder, ForTaskDefinition>, IForTaskDefinitionBuilder
{

    string? eachVariableName;
    string? inExpression;
    string? atVariableName;
    Map<string, TaskDefinition>? tasks;

    /// <inheritdoc/>
    public IForTaskDefinitionBuilder Each(string variableName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(variableName);
        eachVariableName = variableName;
        return this;
    }

    /// <inheritdoc/>
    public IForTaskDefinitionBuilder In(string expression)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(expression);
        inExpression = expression;
        return this;
    }

    /// <inheritdoc/>
    public IForTaskDefinitionBuilder At(string variableName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(variableName);
        atVariableName = variableName;
        return this;
    }

    /// <inheritdoc/>
    public IForTaskDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        tasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public override ForTaskDefinition Build()
    {
        if (string.IsNullOrWhiteSpace(eachVariableName)) throw new NullReferenceException("The variable name used to store the iterated items must be set");
        if (string.IsNullOrWhiteSpace(inExpression)) throw new NullReferenceException("The runtime expression used to resolve the collection to iterate must be set");
        if (tasks == null || tasks.Count < 1) throw new NullReferenceException("The task to perform at each iteration must be set");
        return Configure(new()
        {
            For = new()
            {
                Each = eachVariableName,
                In = inExpression,
                At = atVariableName
            },
            Do = tasks
        });
    }

}
