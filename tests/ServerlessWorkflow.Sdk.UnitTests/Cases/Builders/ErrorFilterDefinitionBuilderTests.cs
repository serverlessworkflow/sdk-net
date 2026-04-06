namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ErrorFilterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Filter_With_Attributes()
    {
        var attrName = "status";
        var attrValue = "500";
        var filter = new ErrorFilterDefinitionBuilder()
            .With(attrName, attrValue)
            .Build();
        filter.With[attrName]!.GetValue<string>().Should().Be(attrValue);
    }

    [Fact]
    public void Build_Should_Create_Filter_With_Prebuilt_Attributes()
    {
        var errorType = "https://errors.com/timeout";
        var filter = new ErrorFilterDefinitionBuilder()
            .With("type", errorType)
            .Build();
        filter.With["type"]!.GetValue<string>().Should().Be(errorType);
    }

    [Fact]
    public void Build_Should_Create_Filter_From_Constructor()
    {
        var status = "404";
        var attributes = new JsonObject { ["status"] = status };
        var filter = new ErrorFilterDefinitionBuilder(attributes).Build();
        filter.With["status"]!.GetValue<string>().Should().Be(status);
    }

}
