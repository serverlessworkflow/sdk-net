using System.Xml.Serialization;

namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of a basic authentication scheme
/// </summary>
[Description("Represents the definition of a basic authentication scheme")]
[DataContract]
public sealed record BasicAuthenticationSchemeDefinition
    : AuthenticationSchemeDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Scheme => AuthenticationScheme.Basic;

    /// <summary>
    /// Gets/sets the username used for authentication
    /// </summary>
    [Description("The username used for authentication")]
    [DataMember(Order = 1, Name = "username"), JsonPropertyOrder(1), JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <summary>
    /// Gets/sets the password used for authentication
    /// </summary>
    [Description("The password used for authentication")]
    [DataMember(Order = 2, Name = "password"), JsonPropertyOrder(2), JsonPropertyName("password")]
    public string? Password { get; init; }

}
