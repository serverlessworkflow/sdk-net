namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class CallDefinitionTests
{

    [Fact]
    public void HttpCallDefinition_Should_Be_CallDefinition()
    {
        //arrange
        var method = "GET";
        var endpoint = new Uri("https://api.example.com");

        //act
        var definition = new HttpCallDefinition { Method = method, Endpoint = endpoint };

        //assert
        definition.Should().BeAssignableTo<CallDefinition>();
        definition.Method.Should().Be(method);
    }

}
