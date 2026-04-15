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
/// Defines the fundamentals of a service used to manage <see cref="WorkflowDefinition"/>s
/// </summary>
public interface IWorkflowDefinitionStore
{

    /// <summary>
    /// Adds the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="definition">The <see cref="WorkflowDefinition"/> to add</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task AddAsync(WorkflowDefinition definition, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> with the specified namespace, name and version
    /// </summary>
    /// <param name="namespace">The namespace the <see cref="WorkflowDefinition"/> to get belongs to</param>
    /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to get</param>
    /// <param name="version">The version, if any, of the <see cref="WorkflowDefinition"/> to get. If not set, defaults to latest version</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>The <see cref="WorkflowDefinition"/> with the specified namespace, name and version, or <c>null</c> if not found</returns>
    Task<WorkflowDefinition?> GetAsync(string @namespace, string name, string? version = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all <see cref="WorkflowDefinition"/>s
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate <see cref="WorkflowDefinition"/>s</returns>
    IAsyncEnumerable<WorkflowDefinition> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all <see cref="WorkflowDefinition"/>s belonging to the specified namespace
    /// </summary>
    /// <param name="namespace">The namespace to list <see cref="WorkflowDefinition"/>s from</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate <see cref="WorkflowDefinition"/>s</returns>
    IAsyncEnumerable<WorkflowDefinition> ListAsync(string @namespace, CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all versions of the specified <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="namespace">The namespace the <see cref="WorkflowDefinition"/> to list versions of belongs to</param>
    /// <param name="name">The name of the <see cref="WorkflowDefinition"/> to list versions of</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IAsyncEnumerable{T}"/> used to enumerate <see cref="WorkflowDefinition"/>s</returns>
    IAsyncEnumerable<WorkflowDefinition> ListAsync(string @namespace, string name, CancellationToken cancellationToken = default);

}