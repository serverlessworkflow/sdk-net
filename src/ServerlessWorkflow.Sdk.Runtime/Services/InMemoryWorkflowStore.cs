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

using Microsoft.Extensions.Caching.Memory;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="IWorkflowStore"/> interface
/// </summary>
/// <param name="cache">The <see cref="IMemoryCache"/> instance used to store workflow states</param>
public sealed class InMemoryWorkflowStore(IMemoryCache cache)
    : IWorkflowStore
{

    /// <inheritdoc/>
    public Task<IWorkflowInstance> AddAsync(WorkflowDefinition definition, JsonObject? input = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        var state = new WorkflowInstance()
        {
            Definition = definition.GetReference(),
            Input = input
        };
        cache.Set(state.Id, state);
        return Task.FromResult((IWorkflowInstance)state);
    }

    /// <inheritdoc/>
    public Task<IWorkflowInstance> GetAsync(string workflowId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        return cache.TryGetValue(workflowId, out IWorkflowInstance? state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Workflow with id '{workflowId}' not found");
    }

    /// <inheritdoc/>
    public Task<IWorkflowInstance> UpdateAsync(IWorkflowInstance state, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(state);
        return Task.FromResult(cache.Set(state.Id, state));
    }

}
