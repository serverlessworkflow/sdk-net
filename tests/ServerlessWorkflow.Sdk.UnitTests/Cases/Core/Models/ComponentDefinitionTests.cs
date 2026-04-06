namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class ComponentDefinitionTests
{

    [Fact]
    public void TaskDefinition_Should_Be_ComponentDefinition()
    {
        //arrange & act
        var definition = new SetTaskDefinition { Set = new JsonObject { ["k"] = "v" } };

        //assert
        definition.Should().BeAssignableTo<ComponentDefinition>();
    }

    [Fact]
    public void ErrorDefinition_Should_Be_ReferenceableComponentDefinition()
    {
        //arrange
        var type = "https://errors.com/not-found";
        var title = "Not Found";
        var status = "404";

        //act
        var definition = new ErrorDefinition { Type = type, Title = title, Status = status };

        //assert
        definition.Should().BeAssignableTo<ReferenceableComponentDefinition>();
        definition.Should().BeAssignableTo<ComponentDefinition>();
    }

    [Fact]
    public void ReferenceableComponentDefinition_Should_Support_Ref()
    {
        //arrange
        var refUri = new Uri("https://schemas.example.com/error.json");

        //act
        var definition = new ErrorDefinition { Type = "t", Title = "t", Status = "400", Ref = refUri };

        //assert
        definition.Ref.Should().Be(refUri);
    }

}
