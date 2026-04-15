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
/// Represents the default implementation of the <see cref="IExtensionDefinitionBuilder"/> interface
/// </summary>
public sealed class ExtensionDefinitionBuilder
    : IExtensionDefinitionBuilder
{

    string? taskType;
    string? whenExpression;
    Map<string, TaskDefinition>? beforeTasks;
    Map<string, TaskDefinition>? afterTasks;

    /// <inheritdoc/>
    public IExtensionDefinitionBuilder Extend(string taskType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(taskType);
        this.taskType = taskType;
        return this;
    }

    /// <inheritdoc/>
    public IExtensionDefinitionBuilder When(string when)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(when);
        whenExpression = when;
        return this;
    }

    /// <inheritdoc/>
    public IExtensionDefinitionBuilder Before(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        beforeTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public IExtensionDefinitionBuilder After(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        afterTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ExtensionDefinition Build()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(taskType);
        return new()
        {
            Extend = taskType,
            When = whenExpression,
            Before = beforeTasks,
            After = afterTasks
        };
    }

}