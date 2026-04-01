namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class WorkflowProcessDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowProcessDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowProcessDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowProcessDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowProcessDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<WorkflowProcessDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
