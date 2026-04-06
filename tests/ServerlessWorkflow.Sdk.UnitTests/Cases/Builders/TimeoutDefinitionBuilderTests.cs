namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TimeoutDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Timeout_From_Duration()
    {
        //arrange
        var duration = Duration.FromSeconds(30);

        //act
        var timeout = new TimeoutDefinitionBuilder()
            .After(duration)
            .Build();

        //assert
        timeout.After.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Timeout_From_String()
    {
        //arrange
        var durationString = "PT30S";

        //act
        var timeout = new TimeoutDefinitionBuilder()
            .After(durationString)
            .Build();

        //assert
        timeout.After.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_After_Missing()
    {
        //arrange
        var builder = new TimeoutDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
