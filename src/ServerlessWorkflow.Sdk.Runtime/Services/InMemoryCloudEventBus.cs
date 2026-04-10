namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents an in-memory implementation of the <see cref="ICloudEventBus"/> interface
/// </summary>
public sealed class InMemoryCloudEventBus 
    : ICloudEventBus, IDisposable
{

    readonly Subject<ICloudEvent> subject = new();

    /// <inheritdoc/>
    public Task PublishAsync(ICloudEvent e, CancellationToken cancellationToken = default)
    {
        subject.OnNext(e);
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public Task<IObservable<ICloudEvent>> SubscribeAsync(CancellationToken cancellationToken = default) => Task.FromResult(subject.AsObservable());

    /// <inheritdoc/>
    public void Dispose() => subject.Dispose();

}
