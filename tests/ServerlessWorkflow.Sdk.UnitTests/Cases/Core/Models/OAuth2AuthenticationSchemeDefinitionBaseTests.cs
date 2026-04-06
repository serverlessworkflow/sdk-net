namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class OAuth2AuthenticationSchemeDefinitionBaseTests
{

    [Fact]
    public void Should_Set_All_Properties()
    {
        //arrange
        var authority = new Uri("https://auth.example.com");
        var grantType = "client_credentials";
        var username = "user";
        var password = "pass";
        var scope = "read";
        var audience = "api";
        var issuer = "https://issuer.example.com";

        //act
        var scheme = new OAuth2AuthenticationSchemeDefinition
        {
            Authority = authority,
            Grant = grantType,
            Username = username,
            Password = password,
            Scopes = [scope],
            Audiences = [audience],
            Issuers = [issuer]
        };

        //assert
        scheme.Authority.Should().Be(authority);
        scheme.Grant.Should().Be(grantType);
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
        scheme.Scopes.Should().Contain(scope);
        scheme.Audiences.Should().Contain(audience);
        scheme.Issuers.Should().Contain(issuer);
        scheme.Scheme.Should().Be(AuthenticationScheme.OAuth2);
    }

    [Fact]
    public void OpenIDConnect_Should_Have_Correct_Scheme()
    {
        //arrange
        var authority = new Uri("https://oidc.example.com");

        //act
        var scheme = new OpenIDConnectSchemeDefinition { Authority = authority, Grant = "authorization_code" };

        //assert
        scheme.Scheme.Should().Be(AuthenticationScheme.OpenIDConnect);
    }

}
