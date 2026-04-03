namespace ServerlessWorkflow.Sdk.Runtime.Models;

/// <summary>
/// Represents an OAUTH2 token
/// </summary>
[Description("Represents an OAUTH2 token")]
[DataContract]
public sealed record OAuth2Token
    : IOAuth2Token
{

    /// <summary>
    /// Gets the UTC date and time at which the <see cref="OAuth2Token"/> has been created
    /// </summary>
    [Description("The UTC date and time at which the OAuth2Token has been created")]
    [DataMember(Order = 1, Name = "createdAt"), JsonPropertyOrder(1), JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the OAUTH2 token type
    /// </summary>
    [Description("The OAUTH2 token type")]
    [DataMember(Order = 2, Name = "tokenType"), JsonPropertyOrder(2), JsonPropertyName("tokenType")]
    public string? TokenType { get; init; }

    /// <summary>
    /// Gets the OAUTH2 token id
    /// </summary>
    [Description("The OAUTH2 token id")]
    [DataMember(Order = 3, Name = "tokenId"), JsonPropertyOrder(3), JsonPropertyName("tokenId")]
    public string? TokenId { get; init; }

    /// <summary>
    /// Gets the OAUTH2 access token
    /// </summary>
    [Description("The OAUTH2 access token")]
    [DataMember(Order = 4, Name = "accessToken"), JsonPropertyOrder(4), JsonPropertyName("accessToken")]
    public string? AccessToken { get; init; }

    /// <summary>
    /// Gets the OAUTH2 refresh token
    /// </summary>
    [Description("The OAUTH2 refresh token")]
    [DataMember(Order = 5, Name = "refreshToken"), JsonPropertyOrder(5), JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; init; }

    /// <summary>
    /// Gets the <see cref="OAuth2Token"/> Time To Live, in seconds
    /// </summary>
    [Description("The OAuth2Token Time To Live, in seconds")]
    [DataMember(Order = 6, Name = "ttl"), JsonPropertyOrder(6), JsonPropertyName("ttl")]
    public int Ttl { get; init; }

    /// <summary>
    /// Gets the UTC date and time at which the <see cref="OAuth2Token"/> expires
    /// </summary>
    [Description("The UTC date and time at which the OAuth2Token expires")]
    [DataMember(Order = 7, Name = "expiresAt"), JsonPropertyOrder(7), JsonPropertyName("expiresAt")]
    public DateTime? ExpiresAt { get; init; }

    /// <summary>
    /// Gets a boolean indicating whether or not the <see cref="OAuth2Token"/> has expired
    /// </summary>
    [IgnoreDataMember, JsonIgnore]
    public bool HasExpired => ExpiresAt.HasValue ? DateTime.UtcNow > ExpiresAt : DateTime.UtcNow > CreatedAt.Add(TimeSpan.FromSeconds(Ttl));

}
