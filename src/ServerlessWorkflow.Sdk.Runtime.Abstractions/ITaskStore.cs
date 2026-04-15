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

namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of a service used to manage <see cref="ITaskInstance"/>s
/// </summary>
public interface ITaskStore
{

    /// <summary>
    /// Adds a the specified <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="instance">The <see cref="ITaskInstance"/> to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The added <see cref="ITaskInstance"/></returns>
    Task<ITaskInstance> AddAsync(ITaskInstance instance, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="ITaskInstance"/> with the specified unique identifier, belonging to the specified workflow
    /// </summary>
    /// <param name="workflowId">The unique identifier of the workflow the task to get the <see cref="ITaskInstance"/> of belongs to</param>
    /// <param name="taskId">The unique identifier of the task to get the <see cref="ITaskInstance"/> of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="ITaskInstance"/> with the specified unique identifier</returns>
    Task<ITaskInstance> GetAsync(string workflowId, string taskId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists the <see cref="ITaskInstance"/>s belonging to the specified workflow
    /// </summary>
    /// <param name="workflowId">The unique identifier of the workflow to list the <see cref="ITaskInstance"/>s of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate the <see cref="ITaskInstance"/>s belonging to the specified workflow</returns>
    IAsyncEnumerable<ITaskInstance> ListAsync(string workflowId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists the <see cref="ITaskInstance"/>s belonging to the specified task of the specified workflow
    /// </summary>
    /// <param name="workflowId">The unique identifier of the workflow to list the <see cref="ITaskInstance"/>s of</param>
    /// <param name="taskId">The unique identifier of the task to list the <see cref="ITaskInstance"/>s of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate the <see cref="ITaskInstance"/>s belonging to the specified task of the specified workflow</returns>
    IAsyncEnumerable<ITaskInstance> ListAsync(string workflowId, string taskId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified <see cref="ITaskInstance"/>
    /// </summary>
    /// <param name="instance">The <see cref="ITaskInstance"/> to update</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="ITaskInstance"/></returns>
    Task<ITaskInstance> UpdateAsync(ITaskInstance instance, CancellationToken cancellationToken = default);

}
