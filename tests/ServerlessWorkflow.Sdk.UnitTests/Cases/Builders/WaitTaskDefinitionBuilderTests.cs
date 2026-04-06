namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class WaitTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Duration()
    {
        var duration = Duration.FromSeconds(5);
        var task = new WaitTaskDefinitionBuilder()
            .For(duration)
            .Build();
        task.Wait.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Accept_Duration_Via_Constructor()
    {
        var duration = Duration.FromMilliseconds(500);
        var task = new WaitTaskDefinitionBuilder(duration).Build();
        task.Wait.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Throw_When_Duration_Missing()
    {
        var act = () => new WaitTaskDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
