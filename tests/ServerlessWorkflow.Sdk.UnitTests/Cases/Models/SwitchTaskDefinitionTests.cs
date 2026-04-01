namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class SwitchTaskDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {        //arrange
        var toSerialize = SwitchTaskDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.SwitchTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.SwitchTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = SwitchTaskDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<SwitchTaskDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
