namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a Kubernetes <see cref="IContainer"/>
/// </summary>
/// <param name="pod">The <see cref="V1Pod"/> the <see cref="IContainer"/> belongs to</param>
/// <param name="logger">The service used to perform logging</param>
/// <param name="kubernetes">The service used to interact with the Docker API</param>
public sealed class KubernetesContainer(V1Pod pod, ILogger<KubernetesContainer> logger, IKubernetes kubernetes)
    : IContainer
{

    CancellationTokenSource cancellationTokenSource = new();

    /// <inheritdoc/>
    public StreamReader? StandardOutput { get; private set; }

    /// <inheritdoc/>
    public StreamReader? StandardError { get; private set; }

    /// <inheritdoc/>
    public long? ExitCode { get; private set; }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (logger.IsEnabled(LogLevel.Debug)) logger.LogDebug("Creating pod '{pod}'...", $"{pod.Name()}.{pod.Namespace()}");
            pod = await kubernetes.CoreV1.CreateNamespacedPodAsync(pod, pod.Namespace(), cancellationToken: cancellationToken);
            if (logger.IsEnabled(LogLevel.Debug)) logger.LogDebug("The pod '{pod}' has been successfully created", $"{pod.Name()}.{pod.Namespace()}");
        }
        catch (Exception ex)
        {
            logger.LogError("An error occurred while creating the specified pod '{pod}': {ex}", $"{pod.Name()}.{pod.Namespace()}", ex);
        }
        await ReadPodLogsAsync(cancellationToken).ConfigureAwait(false);
    }

    async Task ReadPodLogsAsync(CancellationToken cancellationToken)
    {
        await WaitForReadyAsync(cancellationToken);
        var logStream = await kubernetes.CoreV1.ReadNamespacedPodLogAsync(pod.Name(), pod.Namespace(), cancellationToken: cancellationToken).ConfigureAwait(false);
        StandardOutput = new StreamReader(logStream);
    }

    async Task WaitForReadyAsync(CancellationToken cancellationToken)
    {
        logger.LogDebug("Waiting for pod '{pod}'...", $"{pod.Name()}.{pod.Namespace()}");
        pod = await kubernetes.CoreV1.ReadNamespacedPodAsync(pod.Name(), pod.Namespace(), cancellationToken: cancellationToken);
        while (pod.Status.Phase == "Pending")
        {
            await Task.Delay(100, cancellationToken).ConfigureAwait(false);
            pod = await kubernetes.CoreV1.ReadNamespacedPodAsync(pod.Name(), pod.Namespace(), cancellationToken: cancellationToken);
        }
        logger.LogDebug("The pod '{pod}' is up and running", $"{pod.Name()}.{pod.Namespace()}");
    }

    /// <inheritdoc/>
    public async Task WaitForExitAsync(CancellationToken cancellationToken = default)
    {
        await foreach(var (_, item) in kubernetes.CoreV1.WatchListNamespacedPodAsync(pod.Namespace(), fieldSelector: $"metadata.name={pod.Name()}", cancellationToken: cancellationToken).WithCancellation(cancellationToken))
        {
            if (item.Status.Phase != "Succeeded" && item.Status.Phase != "Failed") continue;
            var containerStatus = item.Status.ContainerStatuses.FirstOrDefault();
            ExitCode = containerStatus?.State.Terminated?.ExitCode ?? -1;
            break;
        }
    }

    /// <inheritdoc/>
    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        await kubernetes.CoreV1.DeleteNamespacedPodAsync(pod.Name(), pod.Namespace(), cancellationToken: cancellationToken).ConfigureAwait(false);
        await cancellationTokenSource.CancelAsync().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync()
    {
        StandardOutput?.Dispose();
        StandardError?.Dispose();
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        StandardOutput?.Dispose();
        StandardError?.Dispose();
        GC.SuppressFinalize(this);
    }

}
