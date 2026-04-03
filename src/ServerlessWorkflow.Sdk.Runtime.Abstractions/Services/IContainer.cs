namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a container
/// </summary>
public interface IContainer
    : IDisposable, IAsyncDisposable
{

    /// <summary>
    /// Gets the container's standard output stream
    /// </summary>
    StreamReader? StandardOutput { get; }

    /// <summary>
    /// Gets the container's standard error stream
    /// </summary>
    StreamReader? StandardError { get; }

    /// <summary>
    /// Gets the container's exit code
    /// </summary>
    long? ExitCode { get; }

    /// <summary>
    /// Starts the container
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StartAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Waits for the container to exit
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task WaitForExitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the container
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task StopAsync(CancellationToken cancellationToken = default);

}
