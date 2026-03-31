namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of an OAUTH2 authentication scheme
/// </summary>
[Description("Represents the definition of an OAUTH2 authentication scheme")]
[DataContract]
public sealed record OAuth2AuthenticationSchemeDefinition
    : OAuth2AuthenticationSchemeDefinitionBase
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Scheme => AuthenticationScheme.OAuth2;

    /// <summary>
    /// Gets/sets the configuration of the OAUTH2 endpoints to use
    /// </summary>
    [Description("The configuration of the OAUTH2 endpoints to use")]
    [DataMember(Order = 1, Name = "endpoints"), JsonPropertyOrder(1), JsonPropertyName("endpoints")]
    public OAuth2AuthenticationEndpointsDefinition? Endpoints { get; init; }

}
