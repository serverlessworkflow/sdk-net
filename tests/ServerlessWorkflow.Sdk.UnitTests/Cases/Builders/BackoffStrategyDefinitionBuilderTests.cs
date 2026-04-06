namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BackoffStrategyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Constant_Backoff()
    {
        //arrange
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Constant();

        //act
        var definition = builder.Build();

        //assert
        definition.Constant.Should().NotBeNull();
        definition.Exponential.Should().BeNull();
        definition.Linear.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Exponential_Backoff()
    {
        //arrange
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Exponential();

        //act
        var definition = builder.Build();

        //assert
        definition.Exponential.Should().NotBeNull();
        definition.Constant.Should().BeNull();
        definition.Linear.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Linear_Backoff_With_Increment()
    {
        //arrange
        var increment = Duration.FromSeconds(2);
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Linear(increment);

        //act
        var definition = builder.Build();

        //assert
        definition.Linear.Should().NotBeNull();
        definition.Linear!.Increment.Should().Be(increment);
        definition.Constant.Should().BeNull();
        definition.Exponential.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Strategy_Configured()
    {
        //arrange
        var builder = new BackoffStrategyDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
