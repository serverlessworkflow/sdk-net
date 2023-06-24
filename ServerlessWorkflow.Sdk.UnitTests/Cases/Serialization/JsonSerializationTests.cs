using ServerlessWorkflow.Sdk.Serialization;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Serialization;

public class JsonSerializationTests
    : SerializationTestsBase
{

    protected override T Deserialize<T>(string input) => Serializer.Json.Deserialize<T>(input)!;

    protected override string Serialize<T>(T graph) => Serializer.Json.Serialize(graph);

}
