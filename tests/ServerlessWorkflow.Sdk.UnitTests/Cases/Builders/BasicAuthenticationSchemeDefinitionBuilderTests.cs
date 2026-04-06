namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BasicAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Username_And_Password()
    {
        var username = "admin";
        var password = "secret";
        var scheme = new BasicAuthenticationSchemeDefinitionBuilder()
            .WithUsername(username)
            .WithPassword(password)
            .Build();
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
        scheme.Scheme.Should().Be(AuthenticationScheme.Basic);
    }

    [Fact]
    public void Build_Should_Set_Secret_Reference_Along_With_Credentials()
    {
        var secret = "my-basic-secret";
        var username = "admin";
        var password = "secret";
        var builder = new BasicAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);
        var scheme = builder
            .WithUsername(username)
            .WithPassword(password)
            .Build();
        scheme.Use.Should().Be(secret);
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
    }

    [Fact]
    public void Build_Should_Throw_When_Username_Missing()
    {
        var act = () => new BasicAuthenticationSchemeDefinitionBuilder()
            .WithPassword("pass")
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Password_Missing()
    {
        var act = () => new BasicAuthenticationSchemeDefinitionBuilder()
            .WithUsername("admin")
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
