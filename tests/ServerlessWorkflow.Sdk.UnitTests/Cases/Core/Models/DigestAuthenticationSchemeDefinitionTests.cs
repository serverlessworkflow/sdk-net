namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class DigestAuthenticationSchemeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = DigestAuthenticationSchemeDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.DigestAuthenticationSchemeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.DigestAuthenticationSchemeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = DigestAuthenticationSchemeDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<DigestAuthenticationSchemeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Scheme_Should_Return_Digest()
    {
        //arrange
        var definition = DigestAuthenticationSchemeDefinitionFactory.Create();
        //act
        var scheme = definition.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.Digest);
    }
}
