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
/// Represents an object used to lock asynchronous processes
/// </summary>
/// <remarks>Code based on <see href="https://medium.com/swlh/async-lock-mechanism-on-asynchronous-programing-d43f15ad0b3"/></remarks>
public class AsyncLock
{

    readonly SemaphoreSlim semaphore = new(1, 2);
    readonly Task<IDisposable> releaser;

    /// <summary>
    /// Initializes a new <see cref="AsyncLock"/>
    /// </summary>
    public AsyncLock()
    {
        releaser = Task.FromResult((IDisposable)new Releaser(this));
    }

    /// <summary>
    /// Locks asynchronously
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new object which releases the lock upon disposal</returns>
    public Task<IDisposable> LockAsync(CancellationToken cancellationToken = default)
    {
        var waitTask = semaphore.WaitAsync(cancellationToken);
        return waitTask.IsCompleted
            ? releaser
            : waitTask.ContinueWith((_, state) => (IDisposable)state!, releaser.Result, cancellationToken, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
    }

    class Releaser(AsyncLock toRelease)
        : IDisposable
    {

        public void Dispose() => toRelease.semaphore.Release();

    }

}
