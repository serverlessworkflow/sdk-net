namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EventFilterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Filter_With_Attributes()
    {
        //arrange
        var attrName = "type";
        var attrValue = "com.example.test";

        //act
        var filter = new EventFilterDefinitionBuilder()
            .With(attrName, JsonValue.Create(attrValue))
            .Build();

        //assert
        filter.With?[attrName]?.GetValue<string>().Should().Be(attrValue);
    }

    [Fact]
    public void Build_Should_Create_Filter_With_Prebuilt_Attributes()
    {
        //arrange
        var sourceKey = "source";
        var sourceValue = "https://example.com";
        var attributes = new JsonObject { [sourceKey] = sourceValue };

        //act
        var filter = new EventFilterDefinitionBuilder()
            .With(attributes)
            .Build();

        //assert
        filter.With?[sourceKey]?.GetValue<string>().Should().Be(sourceValue);
    }

}
