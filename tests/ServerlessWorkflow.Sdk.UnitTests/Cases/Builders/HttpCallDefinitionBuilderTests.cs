namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class HttpCallDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Http_Call_With_Method_And_Endpoint()
    {
        //arrange
        var method = "GET";
        var uri = new Uri("https://api.example.com/items");

        //act
        var call = new HttpCallDefinitionBuilder()
            .WithMethod(method)
            .WithEndpoint(e => e.WithUri(uri))
            .Build();

        //assert
        call.Method.Should().Be(method);
        var endpoint = call.Endpoint.Match<EndpointDefinition?>(e => e, _ => null);
        endpoint.Should().NotBeNull();
        endpoint!.Uri.Should().Be(uri);
    }

    [Fact]
    public void Build_Should_Create_Http_Call_With_Headers_And_Body()
    {
        //arrange
        var method = "POST";
        var uri = new Uri("https://api.example.com/items");
        var headerName = "Content-Type";
        var headerValue = "application/json";
        var bodyKey = "name";
        var bodyValue = "test";
        var body = new JsonObject { [bodyKey] = bodyValue };

        //act
        var call = new HttpCallDefinitionBuilder()
            .WithMethod(method)
            .WithEndpoint(e => e.WithUri(uri))
            .WithHeader(headerName, headerValue)
            .WithBody(body)
            .Build();

        //assert
        call.Headers![headerName].Should().Be(headerValue);
        call.Body![bodyKey]!.GetValue<string>().Should().Be(bodyValue);
    }

    [Fact]
    public void Build_Should_Create_Http_Call_With_Output_Format()
    {
        //arrange
        var method = "GET";
        var uri = new Uri("https://api.example.com/items");
        var format = "json";

        //act
        var call = new HttpCallDefinitionBuilder()
            .WithMethod(method)
            .WithEndpoint(e => e.WithUri(uri))
            .WithOutputFormat(format)
            .Build();

        //assert
        call.Output.Should().Be(format);
    }

    [Fact]
    public void Build_Should_Throw_When_Method_Missing()
    {
        //arrange
        var uri = new Uri("https://api.example.com");

        //act
        var act = () => new HttpCallDefinitionBuilder()
            .WithEndpoint(e => e.WithUri(uri))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Endpoint_Missing()
    {
        //arrange
        var method = "GET";

        //act
        var act = () => new HttpCallDefinitionBuilder()
            .WithMethod(method)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
