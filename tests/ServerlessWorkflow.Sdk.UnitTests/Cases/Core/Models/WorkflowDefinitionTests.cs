namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class WorkflowDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<WorkflowDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Minimal_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.CreateMinimal();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Minimal_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.CreateMinimal();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<WorkflowDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
