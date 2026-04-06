namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class OAuth2AuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Authority_And_GrantType()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .Build();

        //assert
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
    }

    [Fact]
    public void Build_Should_Set_Client_Via_Builder()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var clientId = "my-client";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithClient(c => c.WithId(clientId))
            .Build();

        //assert
        scheme.Client.Should().NotBeNull();
        scheme.Client!.Id.Should().Be(clientId);
    }

    [Fact]
    public void Build_Should_Set_Scopes_And_Audiences()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var scope = "read";
        var audience = "api";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithScopes(scope)
            .WithAudiences(audience)
            .Build();

        //assert
        scheme.Scopes.Should().Contain(scope);
        scheme.Audiences.Should().Contain(audience);
    }

    [Fact]
    public void Build_Should_Set_Password_Grant_Credentials()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "password";
        var username = "user";
        var password = "pass";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .WithGrantType(grantType)
            .WithUsername(username)
            .WithPassword(password)
            .Build();

        //assert
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
    }

    [Fact]
    public void Build_Should_Throw_When_Authority_Missing()
    {
        //arrange
        var grantType = "client_credentials";

        //act
        var act = () => new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithGrantType(grantType)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_GrantType_Missing()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");

        //act
        var act = () => new OAuth2AuthenticationSchemeDefinitionBuilder()
            .WithAuthority(authority)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
