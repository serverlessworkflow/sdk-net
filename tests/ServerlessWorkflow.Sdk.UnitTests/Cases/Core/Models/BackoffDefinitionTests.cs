namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class BackoffDefinitionTests
{

    [Fact]
    public void ConstantBackoffDefinition_Should_Be_BackoffDefinition()
    {
        //arrange & act
        var definition = new ConstantBackoffDefinition();

        //assert
        definition.Should().BeAssignableTo<BackoffDefinition>();
    }

    [Fact]
    public void ExponentialBackoffDefinition_Should_Be_BackoffDefinition()
    {
        //arrange & act
        var definition = new ExponentialBackoffDefinition();

        //assert
        definition.Should().BeAssignableTo<BackoffDefinition>();
    }

    [Fact]
    public void LinearBackoffDefinition_Should_Set_Increment()
    {
        //arrange
        var increment = Duration.FromSeconds(2);

        //act
        var definition = new LinearBackoffDefinition { Increment = increment };

        //assert
        definition.Should().BeAssignableTo<BackoffDefinition>();
        definition.Increment.Should().Be(increment);
    }

}
