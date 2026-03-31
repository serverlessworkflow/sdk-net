namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an endpoint
/// </summary>
[Description("Represents the definition of an endpoint")]
[DataContract]
public sealed record EndpointDefinition
{

    /// <summary>
    /// Gets/sets the endpoint's uri
    /// </summary>
    [Description("The endpoint's uri")]
    [Required]
    [DataMember(Order = 1, Name = "uri"), JsonPropertyOrder(1), JsonPropertyName("uri")]
    public required Uri Uri { get; init; }

    /// <summary>
    /// Gets/sets the endpoint's authentication policy, if any
    /// </summary>
    [Description("The endpoint's authentication policy, if any")]
    [DataMember(Order = 2, Name = "authentication"), JsonPropertyOrder(2), JsonPropertyName("authentication")]
    public AuthenticationPolicyDefinition? Authentication { get; init; }

}
