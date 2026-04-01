namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class ExponentialBackoffDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = ExponentialBackoffDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ExponentialBackoffDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ExponentialBackoffDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ExponentialBackoffDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ExponentialBackoffDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
    }
}
