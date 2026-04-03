namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a GRPC service
/// </summary>
[Description("Represents the definition of a GRPC service")]
[DataContract]
public sealed record GrpcServiceDefinition
{

    /// <summary>
    /// Gets/sets the GRPC service name
    /// </summary>
    [Description("The GRPC service name")]
    [Required, MinLength(1)]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>
    /// Gets/sets the hostname of the GRPC service to call
    /// </summary>
    [Description("The hostname of the GRPC service to call")]
    [Required, MinLength(1)]
    [DataMember(Order = 2, Name = "host"), JsonPropertyOrder(2), JsonPropertyName("host")]
    public required string Host { get; init; }

    /// <summary>
    /// Gets/sets the port number of the GRPC service to call
    /// </summary>
    [Description("The port number of the GRPC service to call")]
    [DataMember(Order = 3, Name = "port"), JsonPropertyOrder(3), JsonPropertyName("port")]
    public int? Port { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's authentication policy, if any
    /// </summary>
    [Description("The endpoint's authentication policy, if any")]
    [DataMember(Order = 4, Name = "authentication"), JsonPropertyOrder(4), JsonPropertyName("authentication")]
    public AuthenticationPolicyDefinition? Authentication { get; init; }

}