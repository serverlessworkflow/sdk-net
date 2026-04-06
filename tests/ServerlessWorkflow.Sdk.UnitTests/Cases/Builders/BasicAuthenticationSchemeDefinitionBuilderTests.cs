namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BasicAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Username_And_Password()
    {
        //arrange
        var username = "admin";
        var password = "secret";

        //act
        var scheme = new BasicAuthenticationSchemeDefinitionBuilder()
            .WithUsername(username)
            .WithPassword(password)
            .Build();

        //assert
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
        scheme.Scheme.Should().Be(AuthenticationScheme.Basic);
    }

    [Fact]
    public void Build_Should_Set_Secret_Reference_Along_With_Credentials()
    {
        //arrange
        var secret = "my-basic-secret";
        var username = "admin";
        var password = "secret";
        var builder = new BasicAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);

        //act
        var scheme = builder
            .WithUsername(username)
            .WithPassword(password)
            .Build();

        //assert
        scheme.Use.Should().Be(secret);
        scheme.Username.Should().Be(username);
        scheme.Password.Should().Be(password);
    }

    [Fact]
    public void Build_Should_Throw_When_Username_Missing()
    {
        //arrange
        var password = "pass";

        //act
        var act = () => new BasicAuthenticationSchemeDefinitionBuilder()
            .WithPassword(password)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Password_Missing()
    {
        //arrange
        var username = "admin";

        //act
        var act = () => new BasicAuthenticationSchemeDefinitionBuilder()
            .WithUsername(username)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
