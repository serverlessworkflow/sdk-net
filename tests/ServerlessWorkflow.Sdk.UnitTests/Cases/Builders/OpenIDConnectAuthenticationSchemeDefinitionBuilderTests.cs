namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OpenIDConnectAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Authority_And_GrantType()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");
        var grantType = "authorization_code";

        //act
        var scheme = new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .Build();

        //assert
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
    }

    [Fact]
    public void Build_Should_Set_Issuers()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");
        var grantType = "authorization_code";
        var issuer = "https://oidc.example.com";

        //act
        var scheme = new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithIssuers(issuer)
            .Build();

        //assert
        scheme.Issuers.Should().Contain(issuer);
    }

    [Fact]
    public void Build_Should_Throw_When_Authority_Missing()
    {
        //arrange
        var grantType = "authorization_code";

        //act
        var act = () => new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithGrantType(grantType)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_GrantType_Missing()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");

        //act
        var act = () => new OpenIDConnectAuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
