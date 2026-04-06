namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RetryPolicyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Policy_With_When_Expression()
    {
        var whenExpr = "${ .retryable }";
        var policy = new RetryPolicyDefinitionBuilder()
            .When(whenExpr)
            .Build();
        policy.When.Should().Be(whenExpr);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_ExceptWhen()
    {
        var exceptWhenExpr = "${ .fatal }";
        var policy = new RetryPolicyDefinitionBuilder()
            .ExceptWhen(exceptWhenExpr)
            .Build();
        policy.ExceptWhen.Should().Be(exceptWhenExpr);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Delay()
    {
        var delay = Duration.FromSeconds(5);
        var policy = new RetryPolicyDefinitionBuilder()
            .Delay(delay)
            .Build();
        policy.Delay.Should().Be(delay);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Backoff()
    {
        var policy = new RetryPolicyDefinitionBuilder()
            .Backoff(b => b.Exponential())
            .Build();
        policy.Backoff.Should().NotBeNull();
        policy.Backoff!.Exponential.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Jitter()
    {
        var from = Duration.FromMilliseconds(100);
        var to = Duration.FromMilliseconds(500);
        var policy = new RetryPolicyDefinitionBuilder()
            .Jitter(j => j.From(from).To(to))
            .Build();
        policy.Jitter.Should().NotBeNull();
        policy.Jitter!.From.Should().Be(from);
        policy.Jitter!.To.Should().Be(to);
    }

    [Fact]
    public void Build_Should_Create_Policy_With_Limit()
    {
        uint maxAttempts = 3;
        var policy = new RetryPolicyDefinitionBuilder()
            .Limit(l => l.Attempt().Count(maxAttempts))
            .Build();
        policy.Limit.Should().NotBeNull();
        policy.Limit!.Attempt.Should().NotBeNull();
        policy.Limit!.Attempt!.Count.Should().Be(maxAttempts);
    }

}
