namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class DigestAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Username_And_Password()
    {
        var username = "admin";
        var password = "secret";
        var scheme = new DigestAuthenticationSchemeDefinitionBuilder()
            .WithUsername(username)
            .WithPassword(password)
            .Build();
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
        scheme.Scheme.Should().Be(AuthenticationScheme.Digest);
    }

}
