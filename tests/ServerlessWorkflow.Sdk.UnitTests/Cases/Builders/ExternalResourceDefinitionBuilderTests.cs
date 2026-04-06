namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ExternalResourceDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Resource_With_Endpoint()
    {
        //arrange
        var uri = new Uri("https://resources.example.com/schema.json");
        var resourceName = "my-schema";

        //act
        var resource = new ExternalResourceDefinitionBuilder()
            .WithName(resourceName)
            .WithEndpoint(e => e.WithUri(uri))
            .Build();

        //assert
        resource.Name.Should().Be(resourceName);
        resource.Endpoint.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Endpoint_Missing()
    {
        //arrange
        var builder = new ExternalResourceDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
