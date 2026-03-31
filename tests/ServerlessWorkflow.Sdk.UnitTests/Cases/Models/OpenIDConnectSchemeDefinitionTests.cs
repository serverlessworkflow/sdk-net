namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class OpenIDConnectSchemeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = OpenIDConnectSchemeDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.OpenIDConnectSchemeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.OpenIDConnectSchemeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = OpenIDConnectSchemeDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<OpenIDConnectSchemeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Scheme_Should_Return_OpenIDConnect()
    {
        //arrange
        var definition = OpenIDConnectSchemeDefinitionFactory.Create();
        //act
        var scheme = definition.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.OpenIDConnect);
    }
}
