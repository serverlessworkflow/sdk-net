namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class WaitTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Duration()
    {
        //arrange
        var duration = Duration.FromSeconds(5);

        //act
        var task = new WaitTaskDefinitionBuilder()
            .For(duration)
            .Build();

        //assert
        task.Wait.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Accept_Duration_Via_Constructor()
    {
        //arrange
        var duration = Duration.FromMilliseconds(500);

        //act
        var task = new WaitTaskDefinitionBuilder(duration).Build();

        //assert
        task.Wait.Should().Be(duration);
    }

    [Fact]
    public void Build_Should_Throw_When_Duration_Missing()
    {
        //arrange
        var builder = new WaitTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
