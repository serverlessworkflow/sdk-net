namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of a bearer authentication scheme
/// </summary>
[Description("Represents the definition of a bearer authentication scheme")]
[DataContract]
public sealed record BearerAuthenticationSchemeDefinition
    : AuthenticationSchemeDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Scheme => AuthenticationScheme.Bearer;

    /// <summary>
    /// Gets/sets the bearer token used for authentication
    /// </summary>
    [Description("The bearer token used for authentication")]
    [DataMember(Order = 1, Name = "token"), JsonPropertyOrder(1), JsonPropertyName("token")]
    public string? Token { get; init; }

}
