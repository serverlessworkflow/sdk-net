namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the configuration of OAUTH2 endpoints
/// </summary>
[Description("Represents the configuration of OAUTH2 endpoints")]
[DataContract]
public sealed record OAuth2AuthenticationEndpointsDefinition
{

    /// <summary>
    /// Gets the relative path to the token endpoint
    /// </summary>
    public static readonly Uri TokenEndpoint = new Uri("/oauth2/token", UriKind.RelativeOrAbsolute);
    /// <summary>
    /// Gets the relative path to the revocation endpoint
    /// </summary>
    public static readonly Uri RevocationEndpoint = new Uri("/oauth2/revoke", UriKind.RelativeOrAbsolute);
    /// <summary>
    /// Gets the  relative path to the introspection endpoint
    /// </summary>
    public static readonly Uri IntrospectionEndpoint = new Uri("/oauth2/introspect", UriKind.RelativeOrAbsolute);

    /// <summary>
    /// Gets/sets the relative path to the token endpoint. Defaults to `/oauth2/token`
    /// </summary>
    [Description("The relative path to the token endpoint. Defaults to `/oauth2/token`")]
    [Required]
    [DataMember(Order = 1, Name = "token"), JsonPropertyOrder(1), JsonPropertyName("token")]
    public Uri Token { get; init; } = TokenEndpoint;

    /// <summary>
    /// Gets/sets the relative path to the revocation endpoint. Defaults to `/oauth2/revoke`
    /// </summary>
    [Description("The relative path to the revocation endpoint. Defaults to `/oauth2/revoke`")]
    [Required]
    [DataMember(Order = 2, Name = "revocation"), JsonPropertyOrder(2), JsonPropertyName("revocation")]
    public Uri Revocation { get; init; } = RevocationEndpoint;

    /// <summary>
    /// Gets/sets the relative path to the introspection endpoint. Defaults to `/oauth2/introspect`
    /// </summary>
    [Description("The relative path to the introspection endpoint. Defaults to `/oauth2/introspect`")]
    [Required]
    [DataMember(Order = 3, Name = "introspection"), JsonPropertyOrder(3), JsonPropertyName("introspection")]
    public Uri Introspection { get; init; } = IntrospectionEndpoint;

}
