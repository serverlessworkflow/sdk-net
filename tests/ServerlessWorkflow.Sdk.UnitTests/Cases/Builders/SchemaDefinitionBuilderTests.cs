namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SchemaDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Schema_With_Format()
    {
        var format = "json";
        var schema = new SchemaDefinitionBuilder()
            .WithFormat(format)
            .Build();
        schema.Format.Should().Be(format);
    }

    [Fact]
    public void Build_Should_Create_Schema_With_Document()
    {
        var document = new JsonObject { ["type"] = "object" };
        var schema = new SchemaDefinitionBuilder()
            .WithDocument(document)
            .Build();
        schema.Document.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Schema_With_Resource()
    {
        var uri = new Uri("https://schemas.example.com/schema.json");
        var schema = new SchemaDefinitionBuilder()
            .WithResource(r => r.WithEndpoint(e => e.WithUri(uri)))
            .Build();
        schema.Resource.Should().NotBeNull();
    }

}
