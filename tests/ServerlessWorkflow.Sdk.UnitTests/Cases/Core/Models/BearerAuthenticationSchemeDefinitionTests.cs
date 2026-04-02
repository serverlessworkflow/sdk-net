namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class BearerAuthenticationSchemeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = BearerAuthenticationSchemeDefinitionFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.BearerAuthenticationSchemeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.BearerAuthenticationSchemeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = BearerAuthenticationSchemeDefinitionFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<BearerAuthenticationSchemeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Scheme_Should_Return_Bearer()
    {
        //arrange
        var definition = BearerAuthenticationSchemeDefinitionFactory.Create();
        //act
        var scheme = definition.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.Bearer);
    }
}
