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

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="ITaskExecutorFactory"/> interface.
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="registry">The <see cref="TaskExecutorRegistry"/> used to resolve executor types</param>
/// <param name="callRegistry">The <see cref="CallTaskExecutorRegistry"/> used to resolve call-type-specific executor types</param>
/// <param name="runRegistry">The <see cref="RunTaskExecutorRegistry"/> used to resolve process-type-specific executor types</param>
public sealed class TaskExecutorFactory(IServiceProvider serviceProvider, TaskExecutorRegistry registry, CallTaskExecutorRegistry callRegistry, RunTaskExecutorRegistry runRegistry)
    : ITaskExecutorFactory
{

    /// <inheritdoc/>
    public ITaskExecutor Create(ITaskExecutionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (context.Definition is CallTaskDefinition callDefinition)
        {
            var callExecutorType = callRegistry.Resolve(callDefinition.Call) ?? typeof(CustomFunctionCallTaskExecutor);
            return (ITaskExecutor)ActivatorUtilities.CreateInstance(serviceProvider, callExecutorType, context);
        }
        if (context.Definition is RunTaskDefinition runDefinition)
        {
            var runExecutorType = runRegistry.Resolve(runDefinition.Run.ProcessType) ?? throw new NotSupportedException($"The process type '{runDefinition.Run.ProcessType}' is not supported");
            return (ITaskExecutor)ActivatorUtilities.CreateInstance(serviceProvider, runExecutorType, context);
        }
        var executorType = registry.Resolve(context.Definition.Type) ?? throw new InvalidOperationException($"No task executor registered for task definition type '{context.Definition.GetType().Name}'");
        return (ITaskExecutor)ActivatorUtilities.CreateInstance(serviceProvider, executorType, context);
    }

    /// <inheritdoc/>
    public ITaskExecutor<TDefinition> Create<TDefinition>(ITaskExecutionContext<TDefinition> context)
        where TDefinition : TaskDefinition
    {
        ArgumentNullException.ThrowIfNull(context);
        if (context.Definition is CallTaskDefinition callDefinition)
        {
            var callExecutorType = callRegistry.Resolve(callDefinition.Call) ?? typeof(CustomFunctionCallTaskExecutor);
            return (ITaskExecutor<TDefinition>)ActivatorUtilities.CreateInstance(serviceProvider, callExecutorType, context);
        }
        if (context.Definition is RunTaskDefinition runDefinition)
        {
            var runExecutorType = runRegistry.Resolve(runDefinition.Run.ProcessType) ?? throw new NotSupportedException($"The process type '{runDefinition.Run.ProcessType}' is not supported");
            return (ITaskExecutor<TDefinition>)ActivatorUtilities.CreateInstance(serviceProvider, runExecutorType, context);
        }
        var executorType = registry.Resolve(context.Definition.Type) ?? throw new InvalidOperationException($"No task executor registered for task type '{context.Definition.Type}'");
        return (ITaskExecutor<TDefinition>)ActivatorUtilities.CreateInstance(serviceProvider, executorType, context);
    }

}
