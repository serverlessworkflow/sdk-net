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
/// Represents the default implementation of the <see cref="IForkTaskDefinitionBuilder"/> interface
/// </summary>
public class ForkTaskDefinitionBuilder
    : TaskDefinitionBuilder<IForkTaskDefinitionBuilder, ForkTaskDefinition>, IForkTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets a name/definition mapping of the tasks to execute concurrently, if any
    /// </summary>
    protected Map<string, TaskDefinition>? Tasks { get; set; }

    /// <summary>
    /// Gets/sets a boolean indicating whether or not the task to execute concurrently should compete each other
    /// </summary>
    protected bool ShouldCompete { get; set; }

    /// <inheritdoc/>
    public virtual IForkTaskDefinitionBuilder Branch(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        this.Tasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual IForkTaskDefinitionBuilder Compete()
    {
        this.ShouldCompete = true;
        return this;
    }

    /// <inheritdoc/>
    public override ForkTaskDefinition Build()
    {
        if (this.Tasks == null || this.Tasks.Count < 2) throw new NullReferenceException("The execution strategy must define at least two subtasks");
        return this.Configure(new()
        {
            Fork = new() 
            {
                Branches = this.Tasks,
                Compete = this.ShouldCompete
            }
        });
    }

}