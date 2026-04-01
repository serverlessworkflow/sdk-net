namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class DoTaskDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {        //arrange
        var toSerialize = DoTaskDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.DoTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.DoTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = DoTaskDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<DoTaskDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
