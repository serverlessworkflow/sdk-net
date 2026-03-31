namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of an OAUTH2 token
/// </summary>
[Description("Represents the definition of an OAUTH2 token")]
[DataContract]
public sealed record OAuth2TokenDefinition
{

    /// <summary>
    /// Gets/sets the security token to use
    /// </summary>
    [Description("The security token to use")]
    [DataMember(Order = 1, Name = "token"), JsonPropertyOrder(1), JsonPropertyName("token")]
    public required string Token { get; init; }

    /// <summary>
    /// Gets/sets the type of security token to use
    /// </summary>
    [Description("The type of security token to use")]
    [DataMember(Order = 2, Name = "type"), JsonPropertyOrder(2), JsonPropertyName("type")]
    public required string Type { get; init; }

}