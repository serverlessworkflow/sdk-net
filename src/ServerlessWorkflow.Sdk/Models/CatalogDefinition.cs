namespace ServerlessWorkflow.Sdk.Models;

/// <summary>
/// Represents the definition of a workflow component catalog
/// </summary>
[Description("Represents the definition of a workflow component catalog.")]
[DataContract]
public sealed record CatalogDefinition
{

    /// <summary>
    /// Gets the name of the default catalog
    /// </summary>
    public const string DefaultCatalogName = "default";

    /// <summary>
    /// Gets/sets the endpoint that defines the root URL at which the catalog is located
    /// </summary>
    [Description("The endpoint that defines the root URL at which the catalog is located.")]
    [Required]
    [DataMember(Order = 1, Name = "endpoint"), JsonPropertyOrder(1), JsonPropertyName("endpoint"), JsonConverter(typeof(OneOfJsonConverter<EndpointDefinition, Uri>))]
    public required OneOf<EndpointDefinition, Uri> EndpointValue { get; init; }

}