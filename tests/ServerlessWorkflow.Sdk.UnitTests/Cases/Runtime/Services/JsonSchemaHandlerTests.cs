using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class JsonSchemaHandlerTests
{

    [Fact]
    public void Supports_Should_Return_True_For_Json()
    {
        //arrange
        var format = "json";
        var handler = new JsonSchemaHandler(Mock.Of<IExternalResourceReader>());

        //act
        var result = handler.Supports(format);

        //assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Supports_Should_Return_False_For_Other_Formats()
    {
        //arrange
        var format = "xml";
        var handler = new JsonSchemaHandler(Mock.Of<IExternalResourceReader>());

        //act
        var result = handler.Supports(format);

        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ValidateAsync_Should_Pass_For_Valid_Document()
    {
        //arrange
        var schemaJson = """{"type":"object","properties":{"name":{"type":"string"}},"required":["name"]}""";
        var schema = new SchemaDefinition { Document = new OneOf<JsonObject, string>(schemaJson) };
        var nameKey = "name";
        var nameValue = "test";
        var input = new JsonObject { [nameKey] = nameValue };
        var handler = new JsonSchemaHandler(Mock.Of<IExternalResourceReader>());

        //act
        var result = await handler.ValidateAsync(input, schema, TestContext.Current.CancellationToken);

        //assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateAsync_Should_Fail_For_Invalid_Document()
    {
        //arrange
        var schemaJson = """{"type":"object","properties":{"name":{"type":"string"}},"required":["name"]}""";
        var schema = new SchemaDefinition { Document = new OneOf<JsonObject, string>(schemaJson) };
        var wrongKey = "wrong";
        var wrongValue = 123;
        var input = new JsonObject { [wrongKey] = wrongValue };
        var handler = new JsonSchemaHandler(Mock.Of<IExternalResourceReader>());

        //act
        var result = await handler.ValidateAsync(input, schema, TestContext.Current.CancellationToken);

        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeNull();
    }

}
