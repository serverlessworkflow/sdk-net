namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RetryPolicyLimitDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Limit_With_Attempt()
    {
        //arrange
        uint maxAttempts = 5;
        var builder = new RetryPolicyLimitDefinitionBuilder();
        builder.Attempt().Count(maxAttempts);

        //act
        var limit = builder.Build();

        //assert
        limit.Attempt.Should().NotBeNull();
        limit.Attempt!.Count.Should().Be(maxAttempts);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Duration()
    {
        //arrange
        var maxDuration = Duration.FromMinutes(5);

        //act
        var limit = new RetryPolicyLimitDefinitionBuilder()
            .Duration(maxDuration)
            .Build();

        //assert
        limit.Duration.Should().Be(maxDuration);
    }

}
