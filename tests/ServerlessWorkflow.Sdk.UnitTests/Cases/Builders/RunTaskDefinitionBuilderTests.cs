namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RunTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Container_Run()
    {
        var image = "nginx:latest";
        var builder = new RunTaskDefinitionBuilder();
        builder.Container().WithImage(image);
        var task = builder.Build();
        task.Run.Container.Should().NotBeNull();
        task.Run.Container!.Image.Should().Be(image);
        task.Run.Shell.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Shell_Run()
    {
        var command = "echo hello";
        var argument = "--verbose";
        var builder = new RunTaskDefinitionBuilder();
        builder.Shell().WithCommand(command).WithArgument(argument);
        var task = builder.Build();
        task.Run.Shell.Should().NotBeNull();
        task.Run.Shell!.Command.Should().Be(command);
        task.Run.Shell!.Arguments.Should().Contain(argument);
        task.Run.Container.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Set_Await_Flag()
    {
        var builder = new RunTaskDefinitionBuilder();
        builder.Container().WithImage("alpine");
        var task = builder.Await(false).Build();
        task.Run.Await.Should().BeFalse();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Process()
    {
        var act = () => new RunTaskDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
