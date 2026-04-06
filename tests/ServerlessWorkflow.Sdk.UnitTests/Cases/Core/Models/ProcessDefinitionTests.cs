namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Core.Models;

public class ProcessDefinitionTests
{

    [Fact]
    public void ContainerProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var image = "alpine:latest";

        //act
        var definition = new ContainerProcessDefinition { Image = image };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Image.Should().Be(image);
    }

    [Fact]
    public void ShellProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var command = "echo hello";

        //act
        var definition = new ShellProcessDefinition { Command = command };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Command.Should().Be(command);
    }

    [Fact]
    public void ScriptProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var language = "javascript";
        var code = "console.log('hello')";

        //act
        var definition = new ScriptProcessDefinition { Language = language, Code = code };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Language.Should().Be(language);
        definition.Code.Should().Be(code);
    }

    [Fact]
    public void WorkflowProcessDefinition_Should_Be_ProcessDefinition()
    {
        //arrange
        var ns = "default";
        var name = "sub-workflow";
        var version = "1.0.0";

        //act
        var definition = new WorkflowProcessDefinition { Namespace = ns, Name = name, Version = version };

        //assert
        definition.Should().BeAssignableTo<ProcessDefinition>();
        definition.Namespace.Should().Be(ns);
        definition.Name.Should().Be(name);
        definition.Version.Should().Be(version);
    }

}
