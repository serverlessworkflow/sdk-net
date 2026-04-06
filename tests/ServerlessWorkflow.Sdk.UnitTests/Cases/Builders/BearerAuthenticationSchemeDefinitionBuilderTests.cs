namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BearerAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Token()
    {
        //arrange
        var token = "jwt-token-value";

        //act
        var scheme = new BearerAuthenticationSchemeDefinitionBuilder()
            .WithToken(token)
            .Build();

        //assert
        scheme.Token.Should().Be(token);
        scheme.Scheme.Should().Be(AuthenticationScheme.Bearer);
    }

    [Fact]
    public void Build_Should_Set_Secret_Reference_Along_With_Token()
    {
        //arrange
        var secret = "my-bearer-secret";
        var token = "jwt-token";
        var builder = new BearerAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);

        //act
        var scheme = builder
            .WithToken(token)
            .Build();

        //assert
        scheme.Use.Should().Be(secret);
        scheme.Token.Should().Be(token);
    }

    [Fact]
    public void Build_Should_Throw_When_Token_Missing()
    {
        //arrange
        var builder = new BearerAuthenticationSchemeDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
