namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class EventConsumptionStrategyDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_One_Json_Should_Work()
    {
        //arrange
        var toSerialize = EventConsumptionStrategyDefinitionFactory.CreateOne();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.EventConsumptionStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.EventConsumptionStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_One_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = EventConsumptionStrategyDefinitionFactory.CreateOne();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<EventConsumptionStrategyDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_All_Json_Should_Work()
    {
        //arrange
        var toSerialize = EventConsumptionStrategyDefinitionFactory.CreateAll();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.EventConsumptionStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.EventConsumptionStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Any_Json_Should_Work()
    {
        //arrange
        var toSerialize = EventConsumptionStrategyDefinitionFactory.CreateAny();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.EventConsumptionStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.EventConsumptionStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
