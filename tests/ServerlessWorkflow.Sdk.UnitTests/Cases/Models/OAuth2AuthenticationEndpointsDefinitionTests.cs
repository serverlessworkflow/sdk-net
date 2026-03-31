namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class OAuth2AuthenticationEndpointsDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = OAuth2AuthenticationEndpointsDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.OAuth2AuthenticationEndpointsDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.OAuth2AuthenticationEndpointsDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = OAuth2AuthenticationEndpointsDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<OAuth2AuthenticationEndpointsDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }
}
