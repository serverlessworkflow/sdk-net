using ServerlessWorkflow.Sdk.Runtime.Models;
using RuntimeJsonSerializationContext = ServerlessWorkflow.Sdk.Runtime.Serialization.Json.JsonSerializationContext;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class TaskLifeCycleEventTests
{

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = new TaskLifeCycleEvent(TaskLifeCycleEventType.Completed, new JsonObject { ["duration"] = 1234 });
        //act
        var json = JsonSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.TaskLifeCycleEvent);
        var deserialized = JsonSerializer.Deserialize(json, RuntimeJsonSerializationContext.Default.TaskLifeCycleEvent);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.Type.Should().Be(TaskLifeCycleEventType.Completed);
        deserialized.Data.Should().NotBeNull();
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = new TaskLifeCycleEvent(TaskLifeCycleEventType.Completed, new JsonObject { ["duration"] = 1234 });
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<TaskLifeCycleEvent>(yaml, RuntimeJsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.Type.Should().Be(TaskLifeCycleEventType.Completed);
    }

    [Fact]
    public void Constructor_Should_Set_Properties()
    {
        //act
        var evt = new TaskLifeCycleEvent(TaskLifeCycleEventType.Running);
        //assert
        evt.Type.Should().Be(TaskLifeCycleEventType.Running);
        evt.Data.Should().BeNull();
    }

    [Fact]
    public void Constructor_With_Data_Should_Set_Both_Properties()
    {
        //arrange
        var data = new JsonObject { ["key"] = "value" };
        //act
        var evt = new TaskLifeCycleEvent(TaskLifeCycleEventType.Faulted, data);
        //assert
        evt.Type.Should().Be(TaskLifeCycleEventType.Faulted);
        evt.Data.Should().NotBeNull();
    }

}
