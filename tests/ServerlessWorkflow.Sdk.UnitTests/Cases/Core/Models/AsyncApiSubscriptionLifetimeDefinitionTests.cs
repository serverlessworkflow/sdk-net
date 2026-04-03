namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class AsyncApiSubscriptionLifetimeDefinitionTests
{

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = AsyncApiSubscriptionLifetimeDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AsyncApiSubscriptionLifetimeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AsyncApiSubscriptionLifetimeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = AsyncApiSubscriptionLifetimeDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<AsyncApiSubscriptionLifetimeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

}
