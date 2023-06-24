using ServerlessWorkflow.Sdk.Serialization;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Serialization;

public class YamlSerializationTests
    : SerializationTestsBase
{

    protected override T Deserialize<T>(string input) => Serializer.Yaml.Deserialize<T>(input)!;

    protected override string Serialize<T>(T graph) => Serializer.Yaml.Serialize(graph);

}
