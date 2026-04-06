namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ExponentialBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Exponential_Backoff()
    {
        //arrange
        var builder = new ExponentialBackoffDefinitionBuilder();

        //act
        var definition = builder.Build();

        //assert
        definition.Should().NotBeNull();
    }

}
