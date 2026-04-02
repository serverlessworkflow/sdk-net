namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class ConstantBackoffDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = ConstantBackoffDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ConstantBackoffDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ConstantBackoffDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ConstantBackoffDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ConstantBackoffDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
    }
}
