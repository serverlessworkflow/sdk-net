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
/// Defines the fundamentals of a service used to execute workflows
/// </summary>
public interface IWorkflowRuntime
    : IAsyncDisposable
{

    /// <summary>
    /// Gets an object used to describe the current runtime environment
    /// </summary>
    RuntimeDescriptor Descriptor { get; }

    /// <summary>
    /// Runs a workflow with the specified name and version, using the provided input
    /// </summary>
    /// <param name="namespace">The namespace the workflow to run belongs to</param>
    /// <param name="name">The name of the workflow to run</param>
    /// <param name="version">The version, if any, of the workflow to run. If not specified, the latest version will be used</param>
    /// <param name="input">The input to run the workflow with</param>
    /// <param name="executionOptions">The options used to configure the workflow's execution</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowProcess"/></returns>
    Task<IWorkflowProcess> RunAsync(string @namespace, string name, string? version = null, JsonObject? input = null, WorkflowExecutionsOptions? executionOptions = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Runs the specified workflow
    /// </summary>
    /// <param name="definition">The definition of the workflow to run</param>
    /// <param name="input">The input to run the workflow with</param>
    /// <param name="executionOptions">The options used to configure the workflow's execution</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IWorkflowProcess"/></returns>
    Task<IWorkflowProcess> RunAsync(WorkflowDefinition definition, JsonObject? input = null, WorkflowExecutionsOptions? executionOptions = null, CancellationToken cancellationToken = default);

}