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
/// Represents an in-memory implementation of the <see cref="ITaskStore"/> interface
/// </summary>
public sealed class InMemoryTaskStateStore
    : ITaskStore
{

    readonly ConcurrentDictionary<string, ITaskInstance> tasks = [];

    /// <inheritdoc/>
    public Task<ITaskInstance> AddAsync(ITaskInstance instance, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(instance);
        tasks[GetCacheKey(instance.WorkflowId, instance.Id)] = instance;
        return Task.FromResult(instance);
    }

    /// <inheritdoc/>
    public Task<ITaskInstance> GetAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return tasks.TryGetValue(GetCacheKey(workflowId, taskId), out var state) && state is not null ? Task.FromResult(state) : throw new KeyNotFoundException($"Task with id '{taskId}' not found in workflow '{workflowId}'");
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskInstance> ListAsync(string workflowId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        return tasks.Values.Where(t => t.WorkflowId == workflowId).ToAsyncEnumerable();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<ITaskInstance> ListAsync(string workflowId, string taskId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workflowId);
        ArgumentException.ThrowIfNullOrWhiteSpace(taskId);
        return tasks.Values.Where(t => t.WorkflowId == workflowId && t.ParentId == taskId).ToAsyncEnumerable();
    }

    /// <inheritdoc/>
    public Task<ITaskInstance> UpdateAsync(ITaskInstance instance, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(instance);
        tasks[GetCacheKey(instance.WorkflowId, instance.Id)] = instance;
        return Task.FromResult(instance);
    }

    static string GetCacheKey(string workflowId, string taskId) => $"{workflowId}:{taskId}";

}
