namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class ErrorCatcherDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = ErrorCatcherDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.ErrorCatcherDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.ErrorCatcherDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = ErrorCatcherDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<ErrorCatcherDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
