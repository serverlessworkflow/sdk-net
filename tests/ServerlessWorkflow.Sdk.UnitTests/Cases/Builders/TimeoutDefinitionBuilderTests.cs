namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TimeoutDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Timeout_From_Duration()
    {
        var timeout = new TimeoutDefinitionBuilder()
            .After(Duration.FromSeconds(30))
            .Build();
        timeout.After.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Timeout_From_String()
    {
        var timeout = new TimeoutDefinitionBuilder()
            .After("PT30S")
            .Build();
        timeout.After.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_After_Missing()
    {
        var act = () => new TimeoutDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
