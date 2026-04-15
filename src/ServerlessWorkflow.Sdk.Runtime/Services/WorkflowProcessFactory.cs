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
/// Represents the default implementation of the <see cref="IWorkflowProcessFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="executionContextFactory">The service used to create <see cref="IWorkflowExecutionContext"/>s</param>
public sealed class WorkflowProcessFactory(IServiceProvider serviceProvider, IWorkflowExecutionContextFactory executionContextFactory)
    : IWorkflowProcessFactory
{

    /// <inheritdoc/>
    public async Task<IWorkflowProcess> CreateAsync(WorkflowDefinition definition, IWorkflowInstance state, WorkflowExecutionsOptions executionsOptions, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(state);
        var executionContext = executionContextFactory.Create(definition, state, executionsOptions);
        var process = ActivatorUtilities.CreateInstance<WorkflowProcess>(serviceProvider, executionContext);
        await process.RunAsync().ConfigureAwait(false);
        return process;
    }

}
