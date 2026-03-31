namespace ServerlessWorkflow.Sdk.UnitTests.Services;

internal static class AuthenticationPolicyDefinitionFactory
{
    internal static AuthenticationPolicyDefinition CreateBasic() => new()
    {
        Basic = BasicAuthenticationSchemeDefinitionFactory.Create()
    };

    internal static AuthenticationPolicyDefinition CreateBearer() => new()
    {
        Bearer = BearerAuthenticationSchemeDefinitionFactory.Create()
    };

    internal static AuthenticationPolicyDefinition CreateCertificate() => new()
    {
        Certificate = new CertificateAuthenticationSchemeDefinition()
    };

    internal static AuthenticationPolicyDefinition CreateDigest() => new()
    {
        Digest = DigestAuthenticationSchemeDefinitionFactory.Create()
    };

    internal static AuthenticationPolicyDefinition CreateOAuth2() => new()
    {
        OAuth2 = OAuth2AuthenticationSchemeDefinitionFactory.Create()
    };

    internal static AuthenticationPolicyDefinition CreateOidc() => new()
    {
        Oidc = OpenIDConnectSchemeDefinitionFactory.Create()
    };

    internal static AuthenticationPolicyDefinition CreateWithUse() => new()
    {
        Use = "my-auth-policy"
    };
}
