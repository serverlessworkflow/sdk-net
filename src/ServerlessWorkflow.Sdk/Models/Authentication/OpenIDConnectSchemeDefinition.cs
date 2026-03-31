namespace ServerlessWorkflow.Sdk.Models.Authentication;

/// <summary>
/// Represents the definition of an OpenIDConnect authentication scheme
/// </summary>
[Description("Represents the definition of an OpenIDConnect authentication scheme")]
[DataContract]
public sealed record OpenIDConnectSchemeDefinition
    : OAuth2AuthenticationSchemeDefinitionBase
{

    /// <inheritdoc/>
    [IgnoreDataMember, JsonIgnore]
    public override string Scheme => AuthenticationScheme.OpenIDConnect;

}