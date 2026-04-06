namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EventDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Event_With_Attributes()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.example.test";
        var sourceKey = "source";
        var sourceValue = "https://example.com";

        //act
        var e = new EventDefinitionBuilder()
            .With(typeKey, JsonValue.Create(typeValue))
            .With(sourceKey, JsonValue.Create(sourceValue))
            .Build();

        //assert
        e.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
        e.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Attributes()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.test";
        var sourceKey = "source";
        var sourceValue = "https://test.com";
        var attrs = new JsonObject { [typeKey] = typeValue, [sourceKey] = sourceValue };

        //act
        var e = new EventDefinitionBuilder().With(attrs).Build();

        //assert
        e.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
        e.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

}
