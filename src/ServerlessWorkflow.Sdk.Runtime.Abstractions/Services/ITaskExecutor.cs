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
/// Defines the fundamentals of a service used to execute a task
/// </summary>
public interface ITaskExecutor
    : IObservable<ITaskLifeCycleEvent>, IDisposable, IAsyncDisposable
{

    /// <summary>
    /// Gets the <see cref="ITaskExecutionContext"/> to run
    /// </summary>
    ITaskExecutionContext Task { get; }

    /// <summary>
    /// Initializes the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task InitializeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Runs the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task ExecuteAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Suspends the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task SuspendAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retries to run the task
    /// </summary>
    /// <param name="cause">The <see cref="Error"/> that caused the retry attempt</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task RetryAsync(Error cause, CancellationToken cancellationToken = default);

    /// <summary>
    /// Faults the handled task
    /// </summary>
    /// <param name="error"></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task SetErrorAsync(Error error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets the task's result and transitions to '<see cref="TaskStatus.Completed"/>'.
    /// </summary>
    /// <param name="result">The task's result, if any</param>
    /// <param name="then">The <see cref="FlowDirective"/> to perform next</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task SetResultAsync(JsonNode? result = null, string? then = FlowDirective.Continue, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the task
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="System.Threading.Tasks.Task"/></returns>
    Task CancelAsync(CancellationToken cancellationToken = default);

}

/// <summary>
/// Defines the fundamentals of a service used to execute a task
/// </summary>
public interface ITaskExecutor<TDefinition>
    : ITaskExecutor
    where TDefinition : TaskDefinition
{

    /// <summary>
    /// Gets the <see cref="ITaskExecutionContext"/> to run
    /// </summary>
    new ITaskExecutionContext<TDefinition> Task { get; }

}
