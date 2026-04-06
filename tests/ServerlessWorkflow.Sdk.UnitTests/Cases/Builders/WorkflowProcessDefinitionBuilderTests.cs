namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class WorkflowProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Workflow_Process_With_All_Properties()
    {
        //arrange
        var ns = "my-namespace";
        var name = "sub-workflow";
        var version = "1.0.0";
        var inputKey = "key";
        var inputValue = "value";
        var inputData = new JsonObject { [inputKey] = inputValue };

        //act
        var process = new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithName(name)
            .WithVersion(version)
            .WithInput(inputData)
            .Build();

        //assert
        process.Namespace.Should().Be(ns);
        process.Name.Should().Be(name);
        process.Version.Should().Be(version);
        process.Input![inputKey]!.GetValue<string>().Should().Be(inputValue);
    }

    [Fact]
    public void Build_Should_Use_Default_Namespace_When_Not_Set()
    {
        //arrange
        var name = "sub-workflow";
        var version = "1.0.0";

        //act
        var process = new WorkflowProcessDefinitionBuilder()
            .WithName(name)
            .WithVersion(version)
            .Build();

        //assert
        process.Namespace.Should().Be(WorkflowDefinitionMetadata.DefaultNamespace);
    }

    [Fact]
    public void Build_Should_Use_Default_Version_When_Not_Set()
    {
        //arrange
        var name = "sub-workflow";
        var ns = "my-namespace";
        var expectedDefaultVersion = "latest";

        //act
        var process = new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithName(name)
            .Build();

        //assert
        process.Version.Should().Be(expectedDefaultVersion);
    }

    [Fact]
    public void Build_Should_Throw_When_Name_Missing()
    {
        //arrange
        var ns = "ns";
        var version = "1.0.0";

        //act
        var act = () => new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithVersion(version)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
