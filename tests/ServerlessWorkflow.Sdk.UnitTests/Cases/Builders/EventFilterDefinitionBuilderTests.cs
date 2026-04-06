namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EventFilterDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Filter_With_Attributes()
    {
        var attrName = "type";
        var attrValue = "com.example.test";
        var filter = new EventFilterDefinitionBuilder()
            .With(attrName, JsonValue.Create(attrValue))
            .Build();
        filter.With[attrName]!.GetValue<string>().Should().Be(attrValue);
    }

    [Fact]
    public void Build_Should_Create_Filter_With_Prebuilt_Attributes()
    {
        var sourceKey = "source";
        var sourceValue = "https://example.com";
        var attributes = new JsonObject { [sourceKey] = sourceValue };
        var filter = new EventFilterDefinitionBuilder()
            .With(attributes)
            .Build();
        filter.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

}
