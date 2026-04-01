namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class ExtensionTaskDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {       
        //arrange
        var toSerialize = ExtensionTaskDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ExtensionTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ExtensionTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ExtensionTaskDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ExtensionTaskDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
