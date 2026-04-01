namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class BackoffStrategyDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Constant_Json_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateConstant();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BackoffStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BackoffStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Constant_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateConstant();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<BackoffStrategyDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Exponential_Json_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateExponential();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BackoffStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BackoffStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Linear_Json_Should_Work()
    {
        //arrange
        var toSerialize = BackoffStrategyDefinitionFactory.CreateLinear();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BackoffStrategyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BackoffStrategyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }
}
