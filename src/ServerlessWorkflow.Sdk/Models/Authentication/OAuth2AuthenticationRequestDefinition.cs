namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the configuration of an OAUTH2 authentication request
/// </summary>
[Description("Represents the configuration of an OAUTH2 authentication request")]
[DataContract]
public sealed record OAuth2AuthenticationRequestDefinition
{

    /// <summary>
    /// Gets/sets the encoding of the authentication request. Defaults to 'application/x-www-form-urlencoded'. See <see cref="OAuth2RequestEncoding"/>
    /// </summary>
    [Description("The encoding of the authentication request. Defaults to 'application/x-www-form-urlencoded'. See OAuth2RequestEncoding")]
    [Required, StringLength(int.MaxValue, MinimumLength = 1)]
    [DataMember(Order = 1, Name = "encoding"), JsonPropertyOrder(1), JsonPropertyName("encoding")]
    public string Encoding { get; init; } = OAuth2RequestEncoding.FormUrl;

}