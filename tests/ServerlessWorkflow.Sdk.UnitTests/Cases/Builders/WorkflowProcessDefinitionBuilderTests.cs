namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class WorkflowProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Workflow_Process_With_All_Properties()
    {
        var ns = "my-namespace";
        var name = "sub-workflow";
        var version = "1.0.0";
        var inputKey = "key";
        var inputValue = "value";
        var inputData = new JsonObject { [inputKey] = inputValue };
        var process = new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithName(name)
            .WithVersion(version)
            .WithInput(inputData)
            .Build();
        process.Namespace.Should().Be(ns);
        process.Name.Should().Be(name);
        process.Version.Should().Be(version);
        process.Input![inputKey]!.GetValue<string>().Should().Be(inputValue);
    }

    [Fact]
    public void Build_Should_Use_Default_Namespace_When_Not_Set()
    {
        var name = "sub-workflow";
        var version = "1.0.0";
        var process = new WorkflowProcessDefinitionBuilder()
            .WithName(name)
            .WithVersion(version)
            .Build();
        process.Namespace.Should().Be(WorkflowDefinitionMetadata.DefaultNamespace);
    }

    [Fact]
    public void Build_Should_Use_Default_Version_When_Not_Set()
    {
        var name = "sub-workflow";
        var ns = "my-namespace";
        var process = new WorkflowProcessDefinitionBuilder()
            .WithNamespace(ns)
            .WithName(name)
            .Build();
        process.Version.Should().Be("latest");
    }

    [Fact]
    public void Build_Should_Throw_When_Name_Missing()
    {
        var act = () => new WorkflowProcessDefinitionBuilder()
            .WithNamespace("ns")
            .WithVersion("1.0.0")
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
