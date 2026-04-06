namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RetryAttemptLimitDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Limit_With_Count()
    {
        uint count = 3;
        var limit = new RetryAttemptLimitDefinitionBuilder()
            .Count(count)
            .Build();
        limit.Count.Should().Be(count);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Duration()
    {
        var duration = Duration.FromSeconds(30);
        var limit = new RetryAttemptLimitDefinitionBuilder()
            .Duration(duration)
            .Build();
        limit.Duration.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Count_And_Duration()
    {
        uint count = 5;
        var duration = Duration.FromMinutes(1);
        var limit = new RetryAttemptLimitDefinitionBuilder()
            .Count(count)
            .Duration(duration)
            .Build();
        limit.Count.Should().Be(count);
        limit.Duration.Should().Be(duration);
    }

}
