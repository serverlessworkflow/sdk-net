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
/// Represents the default implementation of the <see cref="ICompositeTaskDefinitionBuilder"/> interface
/// </summary>
public class CompositeTaskDefinitionBuilder
    : TaskDefinitionBuilder<CompositeTaskDefinition>, ICompositeTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets a name/definition mapping of the tasks to execute sequentially, if any
    /// </summary>
    protected EquatableDictionary<string, TaskDefinition>? SequentialTasks { get; set; }

    /// <summary>
    /// Gets/sets a name/definition mapping of the tasks to execute concurrently, if any
    /// </summary>
    protected EquatableDictionary<string, TaskDefinition>? ConcurrentTasks { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the concurrent tasks should race each other
    /// </summary>
    protected bool? ShouldCompete { get; set; }

    /// <inheritdoc/>
    public virtual ICompositeTaskDefinitionBuilder Sequentially(Action<ITaskDefinitionMappingBuilder> setup)
    {
        var builder = new TaskDefinitionMappingBuilder();
        setup(builder);
        this.SequentialTasks = builder.Build();
        this.ConcurrentTasks = null!;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICompositeTaskDefinitionBuilder Concurrently(Action<ITaskDefinitionMappingBuilder> setup)
    {
        var builder = new TaskDefinitionMappingBuilder();
        setup(builder);
        this.ConcurrentTasks = builder.Build();
        this.SequentialTasks = null;
        return this;
    }

    /// <inheritdoc/>
    public virtual ICompositeTaskDefinitionBuilder Compete()
    {
        if (this.ConcurrentTasks?.Count < 1) throw new Exception("Racing is only possible when executing tasks concurrently");
        this.ShouldCompete = true;
        return this;
    }

    /// <inheritdoc/>
    public override CompositeTaskDefinition Build()
    {
        if (this.SequentialTasks?.Count < 2 && this.ConcurrentTasks?.Count < 2) throw new NullReferenceException("The execution strategy must define at least two subtasks");
        return new()
        {
            Execute = new()
            {
                Sequentially = this.SequentialTasks,
                Concurrently = this.ConcurrentTasks,
                Compete = this.ShouldCompete
            }
        };
    }

    TaskExecutionStrategyDefinition ITaskExecutionStrategyDefinitionBuilder<ICompositeTaskDefinitionBuilder>.Build() => this.Build().Execute;

}