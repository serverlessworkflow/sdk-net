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
public class ExtensionDefinitionBuilder
    : IExtensionDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the type of the extended task
    /// </summary>
    protected string? TaskType { get; set; }

    /// <summary>
    /// Gets/sets the expression used to evaluate whether or not the extension applies
    /// </summary>
    protected string? WhenExpression { get; set; }

    /// <summary>
    /// Gets/sets the definition of the task to run before the extended one
    /// </summary>
    protected Map<string, TaskDefinition>? BeforeTasks { get; set; }

    /// <summary>
    /// Gets/sets the definition of the task to run after the extended one
    /// </summary>
    protected Map<string, TaskDefinition>? AfterTasks { get; set; }

    /// <inheritdoc/>
    public virtual IExtensionDefinitionBuilder Extend(string taskType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(taskType);
        this.TaskType = taskType;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExtensionDefinitionBuilder When(string when)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(when);
        this.WhenExpression = when;
        return this;
    }

    /// <inheritdoc/>
    public virtual IExtensionDefinitionBuilder Before(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        this.BeforeTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IExtensionDefinitionBuilder After(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        this.AfterTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ExtensionDefinition Build()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(this.TaskType);
        return new()
        {
            Extend = this.TaskType,
            When = this.WhenExpression,
            Before = this.BeforeTasks,
            After = this.AfterTasks
        };
    }

}