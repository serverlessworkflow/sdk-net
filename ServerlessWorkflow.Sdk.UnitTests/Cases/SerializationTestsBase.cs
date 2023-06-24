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

    [Fact]
    public void Serialize_And_Deserialize_CallbackState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_EventState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_ExtensionState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_ForEachState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_InjectState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_OperationState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_ParallelState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_SleepState_Should_Work()
    {

    }

    [Fact]
    public void Serialize_And_Deserialize_SwitchState_Should_Work()
    {

    }

}