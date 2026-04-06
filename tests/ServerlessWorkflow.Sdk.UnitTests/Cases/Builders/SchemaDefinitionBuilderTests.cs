namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SchemaDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Schema_With_Format()
    {
        //arrange
        var format = "json";

        //act
        var schema = new SchemaDefinitionBuilder()
            .WithFormat(format)
            .Build();

        //assert
        schema.Format.Should().Be(format);
    }

    [Fact]
    public void Build_Should_Create_Schema_With_Document()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "object";
        var document = new JsonObject { [typeKey] = typeValue };

        //act
        var schema = new SchemaDefinitionBuilder()
            .WithDocument(document)
            .Build();

        //assert
        schema.Document.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Schema_With_Resource()
    {
        //arrange
        var uri = new Uri("https://schemas.example.com/schema.json");

        //act
        var schema = new SchemaDefinitionBuilder()
            .WithResource(r => r.WithEndpoint(e => e.WithUri(uri)))
            .Build();

        //assert
        schema.Resource.Should().NotBeNull();
    }

}
