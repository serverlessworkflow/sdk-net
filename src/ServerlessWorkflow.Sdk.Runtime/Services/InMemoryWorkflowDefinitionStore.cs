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
/// Represents an in-memory implementation of the <see cref="IWorkflowDefinitionStore"/> interface
/// </summary>
public sealed class InMemoryWorkflowDefinitionStore
    : IWorkflowDefinitionStore
{

    readonly List<WorkflowDefinition> definitions = [];

    /// <inheritdoc/>
    public Task AddAsync(WorkflowDefinition definition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        definitions.Add(definition);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<WorkflowDefinition?> GetAsync(string @namespace, string name, string? version = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        return Task.FromResult(definitions.FirstOrDefault(d => d.Document.Namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase) && d.Document.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && (version is null || d.Document.Version.Equals(version, StringComparison.OrdinalIgnoreCase))));
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<WorkflowDefinition> ListAsync(CancellationToken cancellationToken = default) => definitions.ToAsyncEnumerable();

    /// <inheritdoc/>
    public IAsyncEnumerable<WorkflowDefinition> ListAsync(string @namespace, CancellationToken cancellationToken = default) => definitions.Where(d => d.Document.Namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase)).ToAsyncEnumerable();

    /// <inheritdoc/>
    public IAsyncEnumerable<WorkflowDefinition> ListAsync(string @namespace, string name, CancellationToken cancellationToken = default) => definitions.Where(d => d.Document.Namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase) && d.Document.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToAsyncEnumerable();

}