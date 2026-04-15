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
/// Represents the default implementation of the <see cref="ITaskExecutionContextFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
public sealed class TaskExecutionContextFactory(IServiceProvider serviceProvider)
    : ITaskExecutionContextFactory
{

    /// <inheritdoc/>
    public ITaskExecutionContext Create(IWorkflowExecutionContext workflow, TaskDefinition definition, ITaskInstance state, JsonObject? arguments = null)
    {
        ArgumentNullException.ThrowIfNull(workflow);
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(definition);
        var contextType = typeof(TaskExecutionContext<>).MakeGenericType(definition.GetType());
        return (ITaskExecutionContext)ActivatorUtilities.CreateInstance(serviceProvider, contextType, workflow, definition, state, arguments ?? new JsonObject());
    }

}