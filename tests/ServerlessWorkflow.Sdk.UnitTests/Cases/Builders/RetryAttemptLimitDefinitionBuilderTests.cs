namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RetryAttemptLimitDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Limit_With_Count()
    {
        //arrange
        uint count = 3;

        //act
        var limit = new RetryAttemptLimitDefinitionBuilder()
            .Count(count)
            .Build();

        //assert
        limit.Count.Should().Be(count);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Duration()
    {
        //arrange
        var duration = Duration.FromSeconds(30);

        //act
        var limit = new RetryAttemptLimitDefinitionBuilder()
            .Duration(duration)
            .Build();

        //assert
        limit.Duration.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Create_Limit_With_Count_And_Duration()
    {
        //arrange
        uint count = 5;
        var duration = Duration.FromMinutes(1);

        //act
        var limit = new RetryAttemptLimitDefinitionBuilder()
            .Count(count)
            .Duration(duration)
            .Build();

        //assert
        limit.Count.Should().Be(count);
        limit.Duration.Should().Be(duration);
    }

}
