namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ConstantBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Constant_Backoff()
    {
        //arrange
        var builder = new ConstantBackoffDefinitionBuilder();

        //act
        var definition = builder.Build();

        //assert
        definition.Should().NotBeNull();
    }

}
