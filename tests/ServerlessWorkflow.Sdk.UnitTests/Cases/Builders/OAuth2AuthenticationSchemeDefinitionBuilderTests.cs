namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Authority_And_GrantType()
    {
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .Build();
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
    }

    [Fact]
    public void Build_Should_Set_Client_Via_Builder()
    {
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var clientId = "my-client";
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithClient(c => c.WithId(clientId))
            .Build();
        scheme.Client.Should().NotBeNull();
        scheme.Client!.Id.Should().Be(clientId);
    }

    [Fact]
    public void Build_Should_Set_Scopes_And_Audiences()
    {
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var scope = "read";
        var audience = "api";
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithScopes(scope)
            .WithAudiences(audience)
            .Build();
        scheme.Scopes.Should().Contain(scope);
        scheme.Audiences.Should().Contain(audience);
    }

    [Fact]
    public void Build_Should_Set_Password_Grant_Credentials()
    {
        var authority = new Uri("https://auth.example.com");
        var grantType = "password";
        var username = "user";
        var password = "pass";
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithUsername(username)
            .WithPassword(password)
            .Build();
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
    }

    [Fact]
    public void Build_Should_Throw_When_Authority_Missing()
    {
        var act = () => new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithGrantType("client_credentials")
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_GrantType_Missing()
    {
        var act = () => new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(new Uri("https://auth.example.com"))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
