namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ConstantBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Constant_Backoff()
    {
        var definition = new ConstantBackoffDefinitionBuilder().Build();
        definition.Should().NotBeNull();
    }

}
