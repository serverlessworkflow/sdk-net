namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OpenIDConnectAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Authority_And_GrantType()
    {
        var authority = new Uri("https://oidc.example.com");
        var grantType = "authorization_code";
        var scheme = new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .Build();
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
    }

    [Fact]
    public void Build_Should_Set_Issuers()
    {
        var authority = new Uri("https://oidc.example.com");
        var grantType = "authorization_code";
        var issuer = "https://oidc.example.com";
        var scheme = new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithIssuers(issuer)
            .Build();
        scheme.Issuers.Should().Contain(issuer);
    }

    [Fact]
    public void Build_Should_Throw_When_Authority_Missing()
    {
        var act = () => new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithGrantType("authorization_code")
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_GrantType_Missing()
    {
        var act = () => new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(new Uri("https://oidc.example.com"))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
