namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ExponentialBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Exponential_Backoff()
    {
        var definition = new ExponentialBackoffDefinitionBuilder().Build();
        definition.Should().NotBeNull();
    }

}
