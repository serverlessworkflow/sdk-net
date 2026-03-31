namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Models;

public class BasicAuthenticationSchemeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = BasicAuthenticationSchemeDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BasicAuthenticationSchemeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BasicAuthenticationSchemeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = BasicAuthenticationSchemeDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<BasicAuthenticationSchemeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Scheme_Should_Return_Basic()
    {
        //arrange
        var definition = BasicAuthenticationSchemeDefinitionFactory.Create();
        //act
        var scheme = definition.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.Basic);
    }
}
