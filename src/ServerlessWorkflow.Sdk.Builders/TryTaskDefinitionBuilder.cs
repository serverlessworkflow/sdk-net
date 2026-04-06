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
public sealed class TryTaskDefinitionBuilder
    : TaskDefinitionBuilder<ITryTaskDefinitionBuilder, TryTaskDefinition>, ITryTaskDefinitionBuilder
{

    Map<string, TaskDefinition>? tryTasks;
    ErrorCatcherDefinition? errorCatcher;

    /// <inheritdoc/>
    public ITryTaskDefinitionBuilder Do(Action<ITaskDefinitionMapBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new TaskDefinitionMapBuilder();
        setup(builder);
        tryTasks = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public ITryTaskDefinitionBuilder Catch(Action<IErrorCatcherDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new ErrorCatcherDefinitionBuilder();
        errorCatcher = builder.Build();
        return this;
    }

    /// <inheritdoc/>
    public override TryTaskDefinition Build()
    {
        if (tryTasks == null || tryTasks.Count < 1) throw new NullReferenceException("The task to try must be set");
        if (errorCatcher == null) throw new NullReferenceException("The catch clause must be set");
        return this.Configure(new()
        {
            Try = tryTasks,
            Catch = errorCatcher
        });
    }

}
