using ServerlessWorkflow.Sdk.Runtime.Models;
using RuntimeJsonSerializationContext = ServerlessWorkflow.Sdk.Runtime.Serialization.Json.JsonSerializationContext;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class SchemaValidationResultTests
{

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = SchemaValidationResult.Failed(new Dictionary<string, IReadOnlyList<string>>
        {
            ["$.name"] = ["Field 'name' is required"]
        });
        //act
        var json = JsonSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.SchemaValidationResult);
        var deserialized = JsonSerializer.Deserialize(json, RuntimeJsonSerializationContext.Default.SchemaValidationResult);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.IsValid.Should().BeFalse();
        deserialized.Errors.Should().ContainKey("$.name");
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = SchemaValidationResult.Failed(new Dictionary<string, IReadOnlyList<string>>
        {
            ["$.name"] = ["Field 'name' is required"]
        });
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<SchemaValidationResult>(yaml, RuntimeJsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Succeeded_Should_Create_Valid_Result()
    {
        //act
        var result = SchemaValidationResult.Succeeded();
        //assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeNull();
    }

    [Fact]
    public void Failed_With_Dictionary_Should_Create_Invalid_Result()
    {
        //arrange
        var errors = new Dictionary<string, IReadOnlyList<string>>
        {
            ["$.age"] = ["Must be a positive integer", "Must be less than 150"]
        };
        //act
        var result = SchemaValidationResult.Failed(errors);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeNull();
        result.Errors!["$.age"].Should().HaveCount(2);
    }

    [Fact]
    public void Failed_With_Enumerable_Should_Create_Invalid_Result()
    {
        //arrange
        var errors = new List<KeyValuePair<string, IEnumerable<string>>>
        {
            new("$.email", ["Invalid email format"])
        };
        //act
        var result = SchemaValidationResult.Failed(errors);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainKey("$.email");
    }

}
