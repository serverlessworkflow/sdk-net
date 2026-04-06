namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BearerAuthenticationSchemeDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Token()
    {
        var token = "jwt-token-value";
        var scheme = new BearerAuthenticationSchemeDefinitionBuilder()
            .WithToken(token)
            .Build();
        scheme.Token.Should().Be(token);
        scheme.Scheme.Should().Be(AuthenticationScheme.Bearer);
    }

    [Fact]
    public void Build_Should_Set_Secret_Reference_Along_With_Token()
    {
        var secret = "my-bearer-secret";
        var token = "jwt-token";
        var builder = new BearerAuthenticationSchemeDefinitionBuilder();
        builder.Use(secret);
        var scheme = builder
            .WithToken(token)
            .Build();
        scheme.Use.Should().Be(secret);
        scheme.Token.Should().Be(token);
    }

    [Fact]
    public void Build_Should_Throw_When_Token_Missing()
    {
        var act = () => new BearerAuthenticationSchemeDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
