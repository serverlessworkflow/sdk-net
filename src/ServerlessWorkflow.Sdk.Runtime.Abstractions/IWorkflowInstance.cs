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
/// Defines the fundamentals of a workflow instance
/// </summary>
public interface IWorkflowInstance
{

    /// <summary>
    /// Gets the workflow's unique identifier
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets a reference to the workflow's definition
    /// </summary>
    WorkflowDefinitionReference Definition { get; }

    /// <summary>
    /// Gets the workflow's status
    /// </summary>
    string Status { get; }

    /// <summary>
    /// Gets the date and time at which the workflow has been created
    /// </summary>
    DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// Gets the date and time the workflow has been started at, if applicable
    /// </summary>
    DateTimeOffset? StartedAt { get; }

    /// <summary>
    /// Gets the date and time the workflow has ended, if applicable
    /// </summary>
    DateTimeOffset? EndedAt { get; }

    /// <summary>
    /// Gets the workflow's input data
    /// </summary>
    JsonObject? Input { get; }

    /// <summary>
    /// Gets the workflow's context data
    /// </summary>
    JsonObject ContextData { get; }

    /// <summary>
    /// Gets the workflow's output data, if any
    /// </summary>
    JsonNode? Output { get; }

    /// <summary>
    /// Gets the error, if any, that has occurred during the workflow's execution
    /// </summary>
    Error? Error { get; }

    /// <summary>
    /// Gets a value indicating whether the workflow is in an operative state
    /// </summary>
    bool IsOperative => Status == TaskStatus.Pending || Status == TaskStatus.Running || Status == TaskStatus.Suspended;

    /// <summary>
    /// Gets a collection containing the workflow's runs
    /// </summary>
    IReadOnlyCollection<IWorkflowRun>? Runs { get; }

    /// <summary>
    /// Starts the workflow
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Resumes the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ResumeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the workflow's output
    /// </summary>
    /// <param name="output">The workflow's output</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetOutputAsync(JsonNode? output, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the error that has occurred during the workflow's execution
    /// </summary>
    /// <param name="error">The error that has occurred during the workflow's execution</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the workflow's context data
    /// </summary>
    /// <param name="contextData">The workflow's context data</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task SetContextDataAsync(JsonObject contextData, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the workflow's execution
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}
