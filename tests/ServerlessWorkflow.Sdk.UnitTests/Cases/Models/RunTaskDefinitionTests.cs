namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class RunTaskDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Container_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateContainer();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Container_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateContainer();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<RunTaskDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Shell_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateShell();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Script_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateScript();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Workflow_Json_Should_Work()
    {
        //arrange
        var toSerialize = RunTaskDefinitionFactory.CreateWorkflow();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RunTaskDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RunTaskDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
