namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class WorkflowDefinitionMetadataTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionMetadataFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowDefinitionMetadata);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowDefinitionMetadata);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionMetadataFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<WorkflowDefinitionMetadata>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Default_Namespace_Should_Be_Default()
    {
        //arrange
        var metadata = new WorkflowDefinitionMetadata
        {
            Dsl = "1.0.0",
            Name = "test",
            Version = "0.1.0"
        };
        //assert
        metadata.Namespace.Should().Be(WorkflowDefinitionMetadata.DefaultNamespace);
    }
}
