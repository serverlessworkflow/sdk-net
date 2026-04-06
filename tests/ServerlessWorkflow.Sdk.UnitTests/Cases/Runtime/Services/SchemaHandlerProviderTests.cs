using ServerlessWorkflow.Sdk.Runtime.Services;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Services;

public class SchemaHandlerProviderTests
{

    [Fact]
    public void GetHandler_Should_Return_Handler_That_Supports_Format()
    {
        //arrange
        var format = "json";
        var mockHandler = new Mock<ISchemaHandler>();
        mockHandler.Setup(h => h.Supports(format)).Returns(true);
        var provider = new SchemaHandlerProvider([mockHandler.Object]);

        //act
        var handler = provider.GetHandler(format);

        //assert
        handler.Should().Be(mockHandler.Object);
    }

    [Fact]
    public void GetHandler_Should_Return_Null_When_No_Handler_Supports_Format()
    {
        //arrange
        var format = "unsupported";
        var mockHandler = new Mock<ISchemaHandler>();
        mockHandler.Setup(h => h.Supports(It.IsAny<string>())).Returns(false);
        var provider = new SchemaHandlerProvider([mockHandler.Object]);

        //act
        var handler = provider.GetHandler(format);

        //assert
        handler.Should().BeNull();
    }

    [Fact]
    public void GetHandler_Should_Return_First_Matching_Handler()
    {
        //arrange
        var format = "json";
        var handler1 = new Mock<ISchemaHandler>();
        handler1.Setup(h => h.Supports(format)).Returns(true);
        var handler2 = new Mock<ISchemaHandler>();
        handler2.Setup(h => h.Supports(format)).Returns(true);
        var provider = new SchemaHandlerProvider([handler1.Object, handler2.Object]);

        //act
        var result = provider.GetHandler(format);

        //assert
        result.Should().Be(handler1.Object);
    }

}
