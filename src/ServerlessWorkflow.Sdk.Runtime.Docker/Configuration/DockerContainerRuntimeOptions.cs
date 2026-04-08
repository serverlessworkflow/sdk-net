namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure the <see cref="DockerContainerRuntime"/>
/// </summary>
public sealed record DockerContainerRuntimeOptions
{

    /// <summary>
    /// Gets the default network to connect containers to
    /// </summary>
    public const string DefaultNetwork = "synapse";

    /// <summary>
    /// Gets/sets the Docker API to use
    /// </summary>
    public DockerApiConfiguration Api { get; set; } = new();

    /// <summary>
    /// Gets/sets the network to connect containers to, if any
    /// </summary>
    public string? Network { get; set; } = DefaultNetwork;

}
