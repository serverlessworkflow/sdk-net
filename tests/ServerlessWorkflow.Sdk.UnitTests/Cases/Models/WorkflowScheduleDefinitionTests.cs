namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class WorkflowScheduleDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Cron_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowScheduleDefinitionFactory.CreateWithCron();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Cron_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowScheduleDefinitionFactory.CreateWithCron();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<WorkflowScheduleDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Every_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowScheduleDefinitionFactory.CreateWithEvery();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_After_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowScheduleDefinitionFactory.CreateWithAfter();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Event_Json_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowScheduleDefinitionFactory.CreateWithEvent();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.WorkflowScheduleDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
