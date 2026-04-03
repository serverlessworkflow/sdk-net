using ServerlessWorkflow.Sdk.Runtime.Models;
using RuntimeJsonSerializationContext = ServerlessWorkflow.Sdk.Runtime.Serialization.Json.JsonSerializationContext;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class OAuth2TokenTests
{

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = OAuth2TokenFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.OAuth2Token);
        var deserialized = JsonSerializer.Deserialize(json, RuntimeJsonSerializationContext.Default.OAuth2Token);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.AccessToken.Should().Be(toSerialize.AccessToken);
        deserialized.TokenType.Should().Be(toSerialize.TokenType);
        deserialized.RefreshToken.Should().Be(toSerialize.RefreshToken);
        deserialized.Ttl.Should().Be(toSerialize.Ttl);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = OAuth2TokenFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<OAuth2Token>(yaml, RuntimeJsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.AccessToken.Should().Be(toSerialize.AccessToken);
        deserialized.TokenType.Should().Be(toSerialize.TokenType);
    }

    [Fact]
    public void HasExpired_Should_Return_False_When_ExpiresAt_Is_In_Future()
    {
        //arrange
        var token = new OAuth2Token
        {
            ExpiresAt = DateTime.UtcNow.AddHours(1)
        };
        //act & assert
        token.HasExpired.Should().BeFalse();
    }

    [Fact]
    public void HasExpired_Should_Return_True_When_ExpiresAt_Is_In_Past()
    {
        //arrange
        var token = new OAuth2Token
        {
            ExpiresAt = DateTime.UtcNow.AddHours(-1)
        };
        //act & assert
        token.HasExpired.Should().BeTrue();
    }

    [Fact]
    public void HasExpired_Should_Use_Ttl_When_ExpiresAt_Is_Not_Set()
    {
        //arrange
        var token = new OAuth2Token
        {
            CreatedAt = DateTime.UtcNow.AddSeconds(-10),
            Ttl = 5
        };
        //act & assert
        token.HasExpired.Should().BeTrue();
    }

    [Fact]
    public void HasExpired_Should_Return_False_When_Within_Ttl()
    {
        //arrange
        var token = new OAuth2Token
        {
            CreatedAt = DateTime.UtcNow,
            Ttl = 3600
        };
        //act & assert
        token.HasExpired.Should().BeFalse();
    }

}
