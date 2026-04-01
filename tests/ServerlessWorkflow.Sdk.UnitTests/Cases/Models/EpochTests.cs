namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class EpochTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = EpochFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Epoch);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.Epoch);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = EpochFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<Epoch>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}