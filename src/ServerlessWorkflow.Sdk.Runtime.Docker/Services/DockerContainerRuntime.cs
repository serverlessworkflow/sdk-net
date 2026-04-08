using ServerlessWorkflow.Sdk.Models.Processes;

namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents a Docker implementation of the <see cref="IContainerRuntime"/> interface
/// </summary>
/// <param name="logger">The service used to perform logging</param>
/// <param name="environment">The current <see cref="IHostEnvironment"/></param>
/// <param name="options">The current <see cref="DockerContainerRuntimeOptions"/></param>
public sealed class DockerContainerRuntime(ILogger<DockerContainerRuntime> logger, IHostEnvironment environment, IOptions<DockerContainerRuntimeOptions> options)
    : IHostedService, IContainerRuntime, IDisposable, IAsyncDisposable
{

    IDockerClient? docker;

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var dockerConfiguration = new DockerClientConfiguration(options.Value.Api.Endpoint);
        docker = dockerConfiguration.CreateClient(string.IsNullOrWhiteSpace(options.Value.Api.Version) ? null : System.Version.Parse(options.Value.Api.Version!));
        if (!environment.RunsInDocker()) return;
        var containerShortId = Environment.MachineName;
        var containerId = (await docker.Containers.InspectContainerAsync(containerShortId, cancellationToken)).ID;
        var response = null as NetworkResponse;
        try
        {
            response = await docker.Networks.InspectNetworkAsync(options.Value.Network, cancellationToken);
        }
        catch (DockerNetworkNotFoundException)
        {
            await docker.Networks.CreateNetworkAsync(new() 
            { 
                Name = options.Value.Network 
            }, cancellationToken);
        }
        finally
        {
            if (response == null || !response!.Containers.ContainsKey(containerId)) await docker.Networks.ConnectNetworkAsync(options.Value.Network, new() 
            { 
                Container = containerId 
            }, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public async Task<IContainer> CreateAsync(ContainerProcessDefinition definition, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(definition);
        if (docker == null) throw new NullReferenceException("The DockerContainerPlatform has not been properly initialized");
        try
        {
            await docker.Images.InspectImageAsync(definition.Image, cancellationToken).ConfigureAwait(false);
        }
        catch (DockerApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            var downloadProgress = new Progress<JSONMessage>();
            var imageComponents = definition.Image.Split(':');
            var imageName = imageComponents[0];
            var imageTag = imageComponents.Length > 1 ? imageComponents[1] : null;
            await docker.Images.CreateImageAsync(new() 
            { 
                FromImage = imageName, 
                Tag = imageTag 
            }, new(), downloadProgress, cancellationToken).ConfigureAwait(false);
        }
        var parameters = new CreateContainerParameters()
        {
            Image = definition.Image,
            Cmd = string.IsNullOrWhiteSpace(definition.Command) ? null : ["/bin/sh", "-c", definition.Command],
            Env = definition.Environment?.Select(e => $"{e.Key}={e.Value}").ToList(),
            HostConfig = new()
            {
                PortBindings = definition.Ports?.ToDictionary(kvp => kvp.Value.ToString(), kvp => (IList<PortBinding>)[new PortBinding() { HostPort = kvp.Key.ToString() }]),
                Binds = definition.Volumes?.Select(e => $"{e.Key}:{e.Value}")?.ToList() ?? []
            }
        };
        var response = await docker.Containers.CreateContainerAsync(parameters, cancellationToken).ConfigureAwait(false);
        if (environment.RunsInDocker()) await docker.Networks.ConnectNetworkAsync(options.Value.Network, new() 
        { 
            Container = response.ID 
        }, cancellationToken);
        foreach (var warning in response.Warnings)
        {
            logger.LogWarning(warning);
        }
        return new DockerContainer(response.ID, docker);
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
