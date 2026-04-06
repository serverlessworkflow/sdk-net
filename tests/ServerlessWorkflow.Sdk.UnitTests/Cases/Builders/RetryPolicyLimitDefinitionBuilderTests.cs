namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RetryPolicyLimitDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Limit_With_Attempt()
    {
        uint maxAttempts = 5;
        var builder = new RetryPolicyLimitDefinitionBuilder();
        builder.Attempt().Count(maxAttempts);
        var limit = builder.Build();
        limit.Attempt.Should().NotBeNull();
        limit.Attempt!.Count.Should().Be(maxAttempts);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Duration()
    {
        var maxDuration = Duration.FromMinutes(5);
        var limit = new RetryPolicyLimitDefinitionBuilder()
            .Duration(maxDuration)
            .Build();
        limit.Duration.Should().Be(maxDuration);
    }

}
