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
/// Defines the fundamentals of a service used to manage <see cref="IWorkflowInstance"/>s
/// </summary>
public interface IWorkflowStore
{

    /// <summary>
    /// Adds a the specified <see cref="IWorkflowInstance"/>
    /// </summary>
    /// <param name="definition">The definition of the workflow to add</param>
    /// <param name="input">The input of the workflow to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The added <see cref="IWorkflowInstance"/></returns>
    Task<IWorkflowInstance> AddAsync(WorkflowDefinition definition, JsonObject? input = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="IWorkflowInstance"/> with the specified unique identifier, belonging to the specified workflow
    /// </summary>
    /// <param name="id">The unique identifier of the workflow to get the state of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="IWorkflowInstance"/> with the specified unique identifier</returns>
    Task<IWorkflowInstance> GetAsync(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the specified <see cref="IWorkflowInstance"/>
    /// </summary>
    /// <param name="instance">The <see cref="IWorkflowInstance"/> to update</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The updated <see cref="IWorkflowInstance"/></returns>
    Task<IWorkflowInstance> UpdateAsync(IWorkflowInstance instance, CancellationToken cancellationToken = default);

}