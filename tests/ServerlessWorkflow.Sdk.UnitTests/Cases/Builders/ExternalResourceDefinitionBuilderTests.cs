namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ExternalResourceDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Resource_With_Endpoint()
    {
        var uri = new Uri("https://resources.example.com/schema.json");
        var resourceName = "my-schema";
        var resource = new ExternalResourceDefinitionBuilder()
            .WithName(resourceName)
            .WithEndpoint(e => e.WithUri(uri))
            .Build();
        resource.Name.Should().Be(resourceName);
        resource.Endpoint.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Endpoint_Missing()
    {
        var act = () => new ExternalResourceDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
