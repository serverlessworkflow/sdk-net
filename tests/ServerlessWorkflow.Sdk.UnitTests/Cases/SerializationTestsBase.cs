namespace ServerlessWorkflow.Sdk.UnitTests.Cases;

public abstract class SerializationTestsBase
{

    protected abstract string Serialize<T>(T graph);

    protected abstract T Deserialize<T>(string input);

    [Fact]
    public void Serialize_And_Deserialize_WorkflowDefinition_Should_Work()
    {
        //arrange
        var toSerialize = WorkflowDefinitionFactory.Create();

        //act
        var text = Serialize(toSerialize);
        var deserialized = Deserialize<WorkflowDefinition>(text);

        //assert
        deserialized.Should().BeEquivalentTo(toSerialize);
    }

}