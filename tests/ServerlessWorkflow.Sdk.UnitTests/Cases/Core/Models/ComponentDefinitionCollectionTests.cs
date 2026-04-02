namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class ComponentDefinitionCollectionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {
        //arrange
        var toSerialize = ComponentDefinitionCollectionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ComponentDefinitionCollection);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ComponentDefinitionCollection);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ComponentDefinitionCollectionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ComponentDefinitionCollection>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
