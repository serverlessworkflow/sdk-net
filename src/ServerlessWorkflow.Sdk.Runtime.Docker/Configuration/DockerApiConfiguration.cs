namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents an object used to configure the Docker API to use
/// </summary>
public sealed record DockerApiConfiguration
{

    /// <summary>
    /// Gets/sets the endpoint of the Docker API to use
    /// </summary>
    public Uri Endpoint { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new("npipe://./pipe/docker_engine") : new("unix:/var/run/docker.sock");

    /// <summary>
    /// Gets/sets the version of the Docker API to use
    /// </summary>
    public string? Version { get; set; }

}