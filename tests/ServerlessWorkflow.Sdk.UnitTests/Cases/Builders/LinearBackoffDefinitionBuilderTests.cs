namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class LinearBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Linear_Backoff_With_Increment()
    {
        //arrange
        var increment = Duration.FromSeconds(2);

        //act
        var definition = new LinearBackoffDefinitionBuilder(increment).Build();

        //assert
        definition.Should().NotBeNull();
        definition.Increment.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Linear_Backoff_Without_Increment()
    {
        //arrange
        var builder = new LinearBackoffDefinitionBuilder();

        //act
        var definition = builder.Build();

        //assert
        definition.Should().NotBeNull();
    }

}
