namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class EmitTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Event_Via_Builder()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.test";
        var sourceKey = "source";
        var sourceValue = "https://test.com";

        //act
        var task = new EmitTaskDefinitionBuilder()
            .Event(e => e.With(typeKey, JsonValue.Create(typeValue)).With(sourceKey, JsonValue.Create(sourceValue)))
            .Build();

        //assert
        task.Emit.Event.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
        task.Emit.Event.With[sourceKey]!.GetValue<string>().Should().Be(sourceValue);
    }

    [Fact]
    public void Build_Should_Set_Event_Via_Definition()
    {
        //arrange
        var typeKey = "type";
        var typeValue = "com.direct";
        var eventDef = new EventDefinition { With = new JsonObject { [typeKey] = typeValue } };

        //act
        var task = new EmitTaskDefinitionBuilder()
            .Event(eventDef)
            .Build();

        //assert
        task.Emit.Event.With[typeKey]!.GetValue<string>().Should().Be(typeValue);
    }

    [Fact]
    public void Build_Should_Throw_When_Event_Missing()
    {
        //arrange
        var builder = new EmitTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
