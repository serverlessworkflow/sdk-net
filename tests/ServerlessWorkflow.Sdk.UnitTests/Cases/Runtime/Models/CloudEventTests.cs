using ServerlessWorkflow.Sdk.Runtime.Models;
using RuntimeJsonSerializationContext = ServerlessWorkflow.Sdk.Runtime.Serialization.Json.JsonSerializationContext;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Runtime.Models;

public class CloudEventTests
{

    [Fact]
    public void Serialize_And_Deserialize_Json_Should_Work()
    {
        //arrange
        var toSerialize = CloudEventFactory.Create();
        //act
        var json = JsonSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.CloudEvent);
        var deserialized = JsonSerializer.Deserialize(json, RuntimeJsonSerializationContext.Default.CloudEvent);
        //assert
        json.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(toSerialize.Id);
        deserialized.SpecVersion.Should().Be(toSerialize.SpecVersion);
        deserialized.Source.Should().Be(toSerialize.Source);
        deserialized.Type.Should().Be(toSerialize.Type);
        deserialized.Subject.Should().Be(toSerialize.Subject);
    }

    [Fact]
    public void Serialize_And_Deserialize_Yaml_Should_Work()
    {
        //arrange
        var toSerialize = CloudEventFactory.Create();
        //act
        var yaml = YamlSerializer.Serialize(toSerialize, RuntimeJsonSerializationContext.Default.Options);
        var deserialized = YamlSerializer.Deserialize<CloudEvent>(yaml, RuntimeJsonSerializationContext.Default.Options);
        //assert
        yaml.Should().NotBeNullOrWhiteSpace();
        deserialized.Should().NotBeNull();
        deserialized!.Id.Should().Be(toSerialize.Id);
        deserialized.Source.Should().Be(toSerialize.Source);
        deserialized.Type.Should().Be(toSerialize.Type);
    }

    [Fact]
    public void GetAttribute_Should_Return_Known_Attributes()
    {
        //arrange
        var cloudEvent = CloudEventFactory.Create();
        //act & assert
        cloudEvent.GetAttribute(CloudEventAttributes.Id).Should().Be(cloudEvent.Id);
        cloudEvent.GetAttribute(CloudEventAttributes.SpecVersion).Should().Be(cloudEvent.SpecVersion);
        cloudEvent.GetAttribute(CloudEventAttributes.Source).Should().Be(cloudEvent.Source);
        cloudEvent.GetAttribute(CloudEventAttributes.Type).Should().Be(cloudEvent.Type);
        cloudEvent.GetAttribute(CloudEventAttributes.Subject).Should().Be(cloudEvent.Subject);
        cloudEvent.GetAttribute(CloudEventAttributes.DataContentType).Should().Be(cloudEvent.DataContentType);
        cloudEvent.GetAttribute(CloudEventAttributes.DataSchema).Should().Be(cloudEvent.DataSchema);
        cloudEvent.GetAttribute(CloudEventAttributes.Data).Should().Be(cloudEvent.Data);
    }

    [Fact]
    public void GetAttribute_Should_Return_Null_For_Unknown_Attributes()
    {
        //arrange
        var cloudEvent = CloudEventFactory.Create();
        //act
        var value = cloudEvent.GetAttribute("nonexistent");
        //assert
        value.Should().BeNull();
    }

    [Fact]
    public void ToString_Should_Return_Id()
    {
        //arrange
        var cloudEvent = CloudEventFactory.Create();
        //act
        var result = cloudEvent.ToString();
        //assert
        result.Should().Be(cloudEvent.Id);
    }

    [Fact]
    public void Default_Values_Should_Be_Set()
    {
        //arrange & act
        var cloudEvent = new CloudEvent { Source = new Uri("https://example.com"), Type = "test" };
        //assert
        cloudEvent.Id.Should().NotBeNullOrWhiteSpace();
        cloudEvent.SpecVersion.Should().Be(CloudEvent.DefaultVersion);
        cloudEvent.DataContentType.Should().Be("application/json");
    }

}
