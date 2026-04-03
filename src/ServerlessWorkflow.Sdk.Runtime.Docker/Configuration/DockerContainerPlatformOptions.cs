namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents the options used to configure the <see cref="DockerContainerRuntime"/>
/// </summary>
[Description("Represents the options used to configure the DockerContainerRuntime")]
[DataContract]
public sealed record DockerContainerPlatformOptions
{

    /// <summary>
    /// Gets the default network to connect containers to
    /// </summary>
    public const string DefaultNetwork = "synapse";

    /// <summary>
    /// Gets/sets the Docker API to use
    /// </summary>
    [DataMember(Order = 1, Name = "api"), JsonPropertyOrder(1), JsonPropertyName("api")]
    public DockerApiConfiguration Api { get; set; } = new();

    /// <summary>
    /// Gets/sets the network to connect containers to, if any
    /// </summary>
    [DataMember(Order = 2, Name = "network"), JsonPropertyOrder(2), JsonPropertyName("network")]
    public string? Network { get; set; } = DefaultNetwork;

}
