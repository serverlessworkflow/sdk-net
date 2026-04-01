namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class TaskDescriptorTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = TaskDescriptorFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.TaskDescriptor);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.TaskDescriptor);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = TaskDescriptorFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<TaskDescriptor>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
