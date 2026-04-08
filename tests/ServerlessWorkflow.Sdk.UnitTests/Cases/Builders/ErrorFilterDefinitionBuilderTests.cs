namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ErrorFilterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Filter_With_Attributes()
    {
        //arrange
        var attrName = "status";
        var attrValue = "500";

        //act
        var filter = new ErrorFilterDefinitionBuilder()
            .With(attrName, attrValue)
            .Build();

        //assert
        filter.With?[attrName]?.GetValue<string>().Should().Be(attrValue);
    }

    [Fact]
    public void Build_Should_Create_Filter_With_Prebuilt_Attributes()
    {
        //arrange
        var typeKey = "type";
        var errorType = "https://errors.com/timeout";

        //act
        var filter = new ErrorFilterDefinitionBuilder()
            .With(typeKey, errorType)
            .Build();

        //assert
        filter.With?[typeKey]?.GetValue<string>().Should().Be(errorType);
    }

    [Fact]
    public void Build_Should_Create_Filter_From_Constructor()
    {
        //arrange
        var statusKey = "status";
        var status = "404";
        var attributes = new JsonObject { [statusKey] = status };

        //act
        var filter = new ErrorFilterDefinitionBuilder(attributes).Build();

        //assert
        filter.With?[statusKey]?.GetValue<string>().Should().Be(status);
    }

}
