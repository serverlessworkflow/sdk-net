namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class AuthenticationPolicyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Basic_Authentication_Policy()
    {
        var username = "admin";
        var password = "s3cret";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Basic().WithUsername(username).WithPassword(password);
        var policy = builder.Build();
        policy.Basic.Should().NotBeNull();
        policy.Basic!.Username.Should().Be(username);
        policy.Basic!.Password.Should().Be(password);
        policy.Bearer.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Bearer_Authentication_Policy()
    {
        var token = "eyJhbGciOi...";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Bearer().WithToken(token);
        var policy = builder.Build();
        policy.Bearer.Should().NotBeNull();
        policy.Bearer!.Token.Should().Be(token);
        policy.Basic.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Digest_Authentication_Policy()
    {
        var username = "admin";
        var password = "digest-pass";
        var builder = new AuthenticationPolicyDefinitionBuilder();
        builder.Digest().WithUsername(username).WithPassword(password);
        var policy = builder.Build();
        policy.Basic.Should().BeNull();
        policy.Bearer.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Scheme_Configured()
    {
        var act = () => new AuthenticationPolicyDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
