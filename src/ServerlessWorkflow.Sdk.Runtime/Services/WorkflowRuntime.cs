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
/// Represents the default implementation of the <see cref="IWorkflowRuntime"/> interface
/// </summary>
/// <param name="definitions">The service used to manage <see cref="WorkflowDefinition"/>s</param>
/// <param name="states">The service used to manage <see cref="IWorkflowInstance"/>s</param>
/// <param name="processFactory">The service used to create <see cref="IWorkflowProcess"/>es</param>
public sealed class WorkflowRuntime(IWorkflowDefinitionStore definitions, IWorkflowStore states, IWorkflowProcessFactory processFactory)
    : IWorkflowRuntime
{

    readonly ConcurrentDictionary<string, IWorkflowProcess> processes = [];

    /// <inheritdoc/>
    public RuntimeDescriptor Descriptor { get; } = new()
    {
        Name = "Serverless Workflow Runtime",
        Version = "1.0.0"
    };

    /// <inheritdoc/>
    public async Task<IWorkflowProcess> RunAsync(string @namespace, string name, string? version = null, JsonObject? input = null, WorkflowExecutionsOptions? executionOptions = null, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(@namespace);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var definition = await definitions.GetAsync(@namespace, name, version, cancellationToken).ConfigureAwait(false) ?? throw new NullReferenceException($"Failed to find the specified workflow definition '{@namespace}.{name}:{version ?? "latest"}'");
        return await RunAsync(definition, input, executionOptions, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<IWorkflowProcess> RunAsync(WorkflowDefinition definition, JsonObject? input = null, WorkflowExecutionsOptions? executionOptions = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        var state = await states.AddAsync(definition, input, cancellationToken).ConfigureAwait(false);
        var process = await processFactory.CreateAsync(definition, state, executionOptions ?? new(), cancellationToken).ConfigureAwait(false);
        processes[state.Id] = process;
        return process;
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        foreach (var processId in processes.Keys.ToList()) if (processes.TryRemove(processId, out var process) && process is not null) await process.DisposeAsync().ConfigureAwait(false);
    }


}
