namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of an OAUTH2 client
/// </summary>
[Description("Represents the definition of an OAUTH2 client")]
[DataContract]
public sealed record OAuth2AuthenticationClientDefinition
{

    /// <summary>
    /// Gets/sets the OAUTH2 `client_id` to use. Required if 'Authentication' has NOT been set to 'none'.
    /// </summary>
    [Description("The OAUTH2 `client_id` to use. Required if 'Authentication' has NOT been set to 'none'.")]
    [DataMember(Order = 1, Name = "id"), JsonPropertyOrder(1), JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Gets/sets the OAUTH2 `client_secret` to use, if any
    /// </summary>
    [Description("The OAUTH2 `client_secret` to use, if any")]
    [DataMember(Order = 2, Name = "secret"), JsonPropertyOrder(2), JsonPropertyName("secret")]
    public string? Secret { get; init; }

    /// <summary>
    /// Gets/sets a JWT containing a signed assertion with the application credentials
    /// </summary>
    [Description("A JWT containing a signed assertion with the application credentials")]
    [DataMember(Order = 3, Name = "assertion"), JsonPropertyOrder(3), JsonPropertyName("assertion")]
    public string? Assertion { get; init; }

    /// <summary>
    /// Gets/sets the authentication method to use to authenticate the client. Defaults to 'client_secret_post'. See <see cref="OAuth2ClientAuthenticationMethod"/>
    /// </summary>
    [Description("The authentication method to use to authenticate the client. Defaults to 'client_secret_post'. See OAuth2ClientAuthenticationMethod")]
    [DataMember(Order = 4, Name = "authentication"), JsonPropertyOrder(4), JsonPropertyName("authentication")]
    public string? Authentication { get; init; }

}