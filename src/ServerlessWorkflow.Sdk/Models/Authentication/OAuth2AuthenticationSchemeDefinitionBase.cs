namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the base class for all authentication schemes based on OAUTH2
/// </summary>
[Description("Represents the base class for all authentication schemes based on OAUTH2")]
[DataContract]
public abstract record OAuth2AuthenticationSchemeDefinitionBase
    : AuthenticationSchemeDefinition
{

    /// <summary>
    /// Gets/sets the uri that references the OAUTH2 authority to use
    /// </summary>
    [Description("The uri that references the OAUTH2 authority to use")]
    [DataMember(Order = 1, Name = "authority"), JsonPropertyOrder(1), JsonPropertyName("authority")]
    public Uri? Authority { get; init; }

    /// <summary>
    /// Gets/sets the grant type to use. See <see cref="OAuth2GrantType"/>
    /// </summary>
    [Description("The grant type to use. See OAuth2GrantType")]
    [DataMember(Order = 2, Name = "grant"), JsonPropertyOrder(2), JsonPropertyName("grant")]
    public string? Grant { get; init; }

    /// <summary>
    /// Gets/sets the definition of the client to use
    /// </summary>
    [Description("The definition of the client to use")]
    [DataMember(Order = 3, Name = "client"), JsonPropertyOrder(3), JsonPropertyName("client")]
    public OAuth2AuthenticationClientDefinition? Client { get; init; }

    /// <summary>
    /// Gets/sets the configuration of the authentication request to perform
    /// </summary>
    [Description("The configuration of the authentication request to perform")]
    [DataMember(Order = 4, Name = "request"), JsonPropertyOrder(4), JsonPropertyName("request")]
    public OAuth2AuthenticationRequestDefinition? Request { get; init; }

    /// <summary>
    /// Gets/sets a list, if any, that contains valid issuers that will be used to check against the issuer of generated tokens
    /// </summary>
    [Description("A list, if any, that contains valid issuers that will be used to check against the issuer of generated tokens")]
    [DataMember(Order = 5, Name = "issuers"), JsonPropertyOrder(5), JsonPropertyName("issuers")]
    public EquatableList<string>? Issuers { get; init; }

    /// <summary>
    /// Gets/sets the scopes, if any, to request the token for
    /// </summary>
    [Description("The scopes, if any, to request the token for")]
    [DataMember(Order = 6, Name = "scopes"), JsonPropertyOrder(6), JsonPropertyName("scopes")]
    public EquatableList<string>? Scopes { get; init; }

    /// <summary>
    /// Gets/sets the audiences, if any, to request the token for
    /// </summary>
    [Description("The audiences, if any, to request the token for")]
    [DataMember(Order = 7, Name = "audiences"), JsonPropertyOrder(7), JsonPropertyName("audiences")]
    public EquatableList<string>? Audiences { get; init; }

    /// <summary>
    /// Gets/sets the username to use. Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    [Description("The username to use. Used only if Grant is 'Password'")]
    [DataMember(Order = 8, Name = "username"), JsonPropertyOrder(8), JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <summary>
    /// Gets/sets the password to use. Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.Password"/>
    /// </summary>
    [Description("The password to use. Used only if Grant is 'Password'")]
    [DataMember(Order = 9, Name = "password"), JsonPropertyOrder(9), JsonPropertyName("password")]
    public string? Password { get; init; }

    /// <summary>
    /// Gets/sets the security token that represents the identity of the party on behalf of whom the request is being made. Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    [Description("The security token that represents the identity of the party on behalf of whom the request is being made. Used only if Grant is 'TokenExchange', in which case it is required")]
    [DataMember(Order = 10, Name = "subject"), JsonPropertyOrder(10), JsonPropertyName("subject")]
    public OAuth2TokenDefinition? Subject { get; init; }

    /// <summary>
    /// Gets/sets the security token that represents the identity of the acting party. Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject.
    /// Used only if <see cref="Grant"/> is <see cref="OAuth2GrantType.TokenExchange"/>, in which case it is required
    /// </summary>
    [Description("The security token that represents the identity of the acting party. Typically, this will be the party that is authorized to use the requested security token and act on behalf of the subject. Used only if Grant is 'TokenExchange', in which case it is required")]
    [DataMember(Order = 11, Name = "actor"), JsonPropertyOrder(11), JsonPropertyName("actor")]
    public OAuth2TokenDefinition? Actor { get; init; }

}
