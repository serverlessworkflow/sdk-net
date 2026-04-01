namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class ContainerLifetimeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = ContainerLifetimeDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ContainerLifetimeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ContainerLifetimeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ContainerLifetimeDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ContainerLifetimeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeEquivalentTo(toSerialize);
    }
}
