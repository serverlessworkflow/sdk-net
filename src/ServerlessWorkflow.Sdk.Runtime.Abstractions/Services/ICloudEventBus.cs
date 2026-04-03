namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Defines the fundamentals of a service used to publish and subscribe to <see cref="ICloudEvent"/>s
/// </summary>
public interface ICloudEventBus
{

    /// <summary>
    /// Publishes the specified <see cref="ICloudEvent"/>
    /// </summary>
    /// <param name="e">The <see cref="ICloudEvent"/> to publish</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new awaitable <see cref="Task"/></returns>
    Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default);

    /// <summary>
    /// Subscribes to streamed <see cref="ICloudEvent"/>s
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="IObservable{T}"/> used to observe streamed <see cref="ICloudEvent"/>s</returns>
    Task<IObservable<ICloudEvent>> SubscribeAsync(CancellationToken cancellationToken = default);

}