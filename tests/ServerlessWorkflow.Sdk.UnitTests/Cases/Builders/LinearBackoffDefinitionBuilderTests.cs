namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class LinearBackoffDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Linear_Backoff_With_Increment()
    {
        var increment = Duration.FromSeconds(2);
        var definition = new LinearBackoffDefinitionBuilder(increment).Build();
        definition.Should().NotBeNull();
        definition.Increment.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Linear_Backoff_Without_Increment()
    {
        var definition = new LinearBackoffDefinitionBuilder().Build();
        definition.Should().NotBeNull();
    }

}
