namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class AuthenticationPolicyDefinitionTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateBasic();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateBasic();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<AuthenticationPolicyDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Bearer_Json_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateBearer();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Bearer_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateBearer();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<AuthenticationPolicyDefinition>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Certificate_Json_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateCertificate();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Digest_Json_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateDigest();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_OAuth2_Json_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateOAuth2();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Oidc_Json_Should_Work()
    {
        //arrange
        var toSerialize = AuthenticationPolicyDefinitionFactory.CreateOidc();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.AuthenticationPolicyDefinition);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Scheme_Should_Return_Basic_When_Basic_Is_Set()
    {
        //arrange
        var policy = AuthenticationPolicyDefinitionFactory.CreateBasic();
        //act
        var scheme = policy.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.Basic);
    }

    [Fact]
    public void Scheme_Should_Return_Bearer_When_Bearer_Is_Set()
    {
        //arrange
        var policy = AuthenticationPolicyDefinitionFactory.CreateBearer();
        //act
        var scheme = policy.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.Bearer);
    }

    [Fact]
    public void Scheme_Should_Return_OAuth2_When_OAuth2_Is_Set()
    {
        //arrange
        var policy = AuthenticationPolicyDefinitionFactory.CreateOAuth2();
        //act
        var scheme = policy.Scheme;
        //assert
        scheme.Should().Be(AuthenticationScheme.OAuth2);
    }
}
