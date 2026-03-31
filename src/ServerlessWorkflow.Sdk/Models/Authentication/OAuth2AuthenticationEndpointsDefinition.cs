namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the configuration of OAUTH2 endpoints
/// </summary>
[Description("Represents the configuration of OAUTH2 endpoints")]
[DataContract]
public sealed record OAuth2AuthenticationEndpointsDefinition
{

    /// <summary>
    /// Gets/sets the relative path to the token endpoint. Defaults to `/oauth2/token`
    /// </summary>
    [Description("The relative path to the token endpoint. Defaults to `/oauth2/token`")]
    [Required]
    [DataMember(Order = 1, Name = "token"), JsonPropertyOrder(1), JsonPropertyName("token")]
    public Uri Token { get; init; } = new("/oauth2/token", UriKind.RelativeOrAbsolute);

    /// <summary>
    /// Gets/sets the relative path to the revocation endpoint. Defaults to `/oauth2/revoke`
    /// </summary>
    [Description("The relative path to the revocation endpoint. Defaults to `/oauth2/revoke`")]
    [Required]
    [DataMember(Order = 2, Name = "revocation"), JsonPropertyOrder(2), JsonPropertyName("revocation")]
    public Uri Revocation { get; init; } = new("/oauth2/revoke", UriKind.RelativeOrAbsolute);

    /// <summary>
    /// Gets/sets the relative path to the introspection endpoint. Defaults to `/oauth2/introspect`
    /// </summary>
    [Description("The relative path to the introspection endpoint. Defaults to `/oauth2/introspect`")]
    [Required]
    [DataMember(Order = 3, Name = "introspection"), JsonPropertyOrder(3), JsonPropertyName("introspection")]
    public Uri Introspection { get; init; } = new("/oauth2/introspect", UriKind.RelativeOrAbsolute);

}
