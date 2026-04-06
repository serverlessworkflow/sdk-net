namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class BackoffStrategyDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Constant_Backoff()
    {
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Constant();
        var definition = builder.Build();
        definition.Constant.Should().NotBeNull();
        definition.Exponential.Should().BeNull();
        definition.Linear.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Exponential_Backoff()
    {
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Exponential();
        var definition = builder.Build();
        definition.Exponential.Should().NotBeNull();
        definition.Constant.Should().BeNull();
        definition.Linear.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Linear_Backoff_With_Increment()
    {
        var increment = Duration.FromSeconds(2);
        var builder = new BackoffStrategyDefinitionBuilder();
        builder.Linear(increment);
        var definition = builder.Build();
        definition.Linear.Should().NotBeNull();
        definition.Linear!.Increment.Should().Be(increment);
        definition.Constant.Should().BeNull();
        definition.Exponential.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Strategy_Configured()
    {
        var act = () => new BackoffStrategyDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
