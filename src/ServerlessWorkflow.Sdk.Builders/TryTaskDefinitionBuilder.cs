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
/// Represents the default implementation of the <see cref="ITryTaskDefinitionBuilder"/> interface
/// </summary>
public class TryTaskDefinitionBuilder
    : TaskDefinitionBuilder<ITryTaskDefinitionBuilder, TryTaskDefinition>, ITryTaskDefinitionBuilder
{

    /// <summary>
    /// Gets/sets the tasks to try
    /// </summary>
    protected Map<string, TaskDefinition>? TryTasks { get; set; }

    /// <summary>
    /// Gets/sets the definition of the error catcher to use
    /// </summary>
    protected ErrorCatcherDefinition? ErrorCatcher { get; set; }

    /// <inheritdoc/>
    public virtual ITryTaskDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        this.TryTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public virtual ITryTaskDefinitionBuilder Catch(Action<IErrorCatcherDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorCatcherDefinitionBuilder();
        this.ErrorCatcher = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public override TryTaskDefinition Build()
    {
        if (this.TryTasks == null || this.TryTasks.Count < 1) throw new NullReferenceException("The task to try must be set");
        if (this.ErrorCatcher == null) throw new NullReferenceException("The catch clause must be set");
        return this.Configure(new()
        {
            Try = this.TryTasks,
            Catch = this.ErrorCatcher
        });
    }

}
