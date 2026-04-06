namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class AuthenticationPolicyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Basic_Authentication_Policy()
    {
        //arrange
        var username = "admin";
        var password = "s3cret";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Basic().WithUsername(username).WithPassword(password);

        //act
        var policy = builder.Build();

        //assert
        policy.Basic.Should().NotBeNull();
        policy.Basic!.Username.Should().Be(username);
        policy.Basic!.Password.Should().Be(password);
        policy.Bearer.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Bearer_Authentication_Policy()
    {
        //arrange
        var token = "eyJhbGciOi...";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Bearer().WithToken(token);

        //act
        var policy = builder.Build();

        //assert
        policy.Bearer.Should().NotBeNull();
        policy.Bearer!.Token.Should().Be(token);
        policy.Basic.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Digest_Authentication_Policy()
    {
        //arrange
        var username = "admin";
        var password = "digest-pass";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Digest().WithUsername(username).WithPassword(password);

        //act
        var policy = builder.Build();

        //assert
        policy.Basic.Should().BeNull();
        policy.Bearer.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Scheme_Configured()
    {
        //arrange
        var builder = new AuthenticationPolicyDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
