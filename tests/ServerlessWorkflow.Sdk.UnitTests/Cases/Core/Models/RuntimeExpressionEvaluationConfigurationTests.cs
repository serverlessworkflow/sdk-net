namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class RuntimeExpressionEvaluationConfigurationTests
{
    [Fact]
    public void Serialize_And_Deserialize_Basic_Json_Should_Work()
    {
        //arrange
        var toSerialize = RuntimeExpressionEvaluationConfigurationFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, JsonSerializationContext.Default.RuntimeExpressionEvaluationConfiguration);
        var deserialized = JsonSerializer.Deserialize(json, JsonSerializationContext.Default.RuntimeExpressionEvaluationConfiguration);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Serialize_And_Deserialize_Basic_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = RuntimeExpressionEvaluationConfigurationFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, JsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<RuntimeExpressionEvaluationConfiguration>(yaml, JsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().BeJsonEquivalentTo(toSerialize);
    }

    [Fact]
    public void Default_Language_Should_Be_JQ()
    {
        //arrange & act
        var config = new RuntimeExpressionEvaluationConfiguration();
        //assert
        config.Language.Should().Be(RuntimeExpressions.Languages.JQ);
    }
}
