namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EndpointDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Endpoint_With_Uri()
    {
        //arrange
        var uri = new Uri("https://api.example.com/v1");

        //act
        var endpoint = new EndpointDefinitionBuilder()
            .WithUri(uri)
            .Build();

        //assert
        endpoint.Uri.Should().Be(uri);
    }

    [Fact]
    public void Build_Should_Create_Endpoint_With_Authentication()
    {
        //arrange
        var uri = new Uri("https://api.example.com/v1");
        var token = "my-token";

        //act
        var endpoint = new EndpointDefinitionBuilder()
            .WithUri(uri)
            .UseAuthentication(auth => auth.Bearer().WithToken(token))
            .Build();

        //assert
        endpoint.Uri.Should().Be(uri);
        endpoint.Authentication.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Uri_Missing()
    {
        //arrange
        var builder = new EndpointDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
