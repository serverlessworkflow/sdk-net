namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of a certificate authentication scheme
/// </summary>
[Description("Represents the definition of a certificate authentication scheme")]
[DataContract]
public sealed record CertificateAuthenticationSchemeDefinition
    : AuthenticationSchemeDefinition
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Scheme => AuthenticationScheme.Certificate;

}
