namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of an external resource
/// </summary>
[Description("Represents the definition of an external resource")]
[DataContract]
public sealed record ExternalResourceDefinition
{

    /// <summary>
    /// Gets/sets the external resource's name, if any
    /// </summary>
    [Description("The external resource's name, if any")]
    [DataMember(Order = 1, Name = "name"), JsonPropertyOrder(1), JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets/sets the endpoint at which to get the defined resource
    /// </summary>
    [Description("The endpoint at which to get the defined resource")]
    [Required]
    [DataMember(Order = 2, Name = "endpoint"), JsonPropertyOrder(2), JsonPropertyName("endpoint"), JsonConverter(typeof(OneOfJsonConverter<EndpointDefinition, Uri>))]
    public required OneOf<EndpointDefinition, Uri> Endpoint { get; init; }

}