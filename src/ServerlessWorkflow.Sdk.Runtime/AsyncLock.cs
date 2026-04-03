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