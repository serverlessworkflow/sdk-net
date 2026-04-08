namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the Docker implementation of the <see cref="IContainerRuntime"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="options">The current <see cref="KubernetesContainerRuntimeOptions"/></param>
public sealed class KubernetesContainerRuntime(IServiceProvider serviceProvider, IOptions<KubernetesContainerRuntimeOptions> options)
    : IHostedService, IContainerRuntime, IDisposable, IAsyncDisposable
{

    Kubernetes? kubernetes;

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var kubeconfig = string.IsNullOrWhiteSpace(options.Value.Kubeconfig)
            ? KubernetesClientConfiguration.InClusterConfig()
            : await KubernetesClientConfiguration.BuildConfigFromConfigFileAsync(new FileInfo(options.Value.Kubeconfig)).ConfigureAwait(false);
        kubernetes = new Kubernetes(kubeconfig);
    }

    /// <inheritdoc/>
    public Task<IContainer> CreateAsync(ContainerProcessDefinition definition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        if (kubernetes is null) throw new NullReferenceException("The KubernetesContainerPlatform has not been properly initialized");
        var pod = new V1Pod()
        {
            Metadata = new()
            {
                NamespaceProperty = options.Value.Namespace,
                Name = $"{definition.Image}-{Guid.NewGuid().ToString("N")[..6].ToLowerInvariant()}"
            },
            Spec = new()
            {
                RestartPolicy = "Never",
                Containers =
                [
                    new()
                    {
                        Image = definition.Image,
                        ImagePullPolicy = options.Value.ImagePullPolicy,
                        Command = string.IsNullOrWhiteSpace(definition.Command) ? null : ["/bin/sh", "-c", definition.Command],
                        Env = definition.Environment?.Select(e => new V1EnvVar()
                        {
                            Name = e.Key,
                            Value = e.Value
                        }).ToList()
                    }
                ]
            }
        };
        return Task.FromResult((IContainer)ActivatorUtilities.CreateInstance<KubernetesContainer>(serviceProvider, pod, kubernetes));
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        kubernetes?.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        kubernetes?.Dispose();
        GC.SuppressFinalize(this);
    }

}
