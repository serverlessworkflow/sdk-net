namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of an object used to wrap a streamed <see cref="ICloudEvent"/>
/// </summary>
public interface IStreamedCloudEvent
{

    /// <summary>
    /// Gets the streamed <see cref="ICloudEvent"/>
    /// </summary>
    ICloudEvent Event { get; }

    /// <summary>
    /// Gets the position of the <see cref="ICloudEvent"/> within its originating stream
    /// </summary>
    uint Offset { get; }

    /// <summary>
    /// Acknowledges that the <see cref="ICloudEvent"/> has been successfully processed
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task AckAsync(CancellationToken cancellationToken = default);

}