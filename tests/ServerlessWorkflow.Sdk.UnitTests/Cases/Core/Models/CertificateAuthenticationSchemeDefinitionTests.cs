namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class CertificateAuthenticationSchemeDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = new CertificateAuthenticationSchemeDefinition();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.CertificateAuthenticationSchemeDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.CertificateAuthenticationSchemeDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = new CertificateAuthenticationSchemeDefinition();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<CertificateAuthenticationSchemeDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Scheme_Should_Return_Certificate()
    {
        //arrange
        var definition = new CertificateAuthenticationSchemeDefinition();
        //act
        var scheme = definition.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.Certificate);
    }
}
