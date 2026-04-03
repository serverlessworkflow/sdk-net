namespace ServerlessWorkflow.Sdk.Runtime.Configuration;

/// <summary>
/// Represents an object used to configure the Docker API to use
/// </summary>
[Description("Represents an object used to configure the Docker API to use")]
[DataContract]
public sealed record DockerApiConfiguration
{

    /// <summary>
    /// Gets/sets the endpoint of the Docker API to use
    /// </summary>
    [DataMember(Order = 1, Name = "endpoint"), JsonPropertyOrder(1), JsonPropertyName("endpoint")]
    public Uri Endpoint { get; set; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? new("npipe://./pipe/docker_engine") : new("unix:/var/run/docker.sock");

    /// <summary>
    /// Gets/sets the version of the Docker API to use
    /// </summary>
    [DataMember(Order = 2, Name = "version"), JsonPropertyOrder(2), JsonPropertyName("version")]
    public string? Version { get; set; }

}