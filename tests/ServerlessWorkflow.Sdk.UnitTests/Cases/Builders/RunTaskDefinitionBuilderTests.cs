namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class RunTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Container_Run()
    {
        //arrange
        var image = "nginx:latest";
        var builder = new RunTaskDefinitionBuilder();
        builder.Container().WithImage(image);

        //act
        var task = builder.Build();

        //assert
        task.Run.Container.Should().NotBeNull();
        task.Run.Container!.Image.Should().Be(image);
        task.Run.Shell.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Create_Shell_Run()
    {
        //arrange
        var command = "echo hello";
        var argument = "--verbose";
        var builder = new RunTaskDefinitionBuilder();
        builder.Shell().WithCommand(command).WithArgument(argument);

        //act
        var task = builder.Build();

        //assert
        task.Run.Shell.Should().NotBeNull();
        task.Run.Shell!.Command.Should().Be(command);
        task.Run.Shell!.Arguments.Should().Contain(argument);
        task.Run.Container.Should().BeNull();
    }

    [Fact]
    public void Build_Should_Set_Await_Flag()
    {
        //arrange
        var image = "alpine";
        var builder = new RunTaskDefinitionBuilder();
        builder.Container().WithImage(image);

        //act
        var task = builder.Await(false).Build();

        //assert
        task.Run.Await.Should().BeFalse();
    }

    [Fact]
    public void Build_Should_Throw_When_No_Process()
    {
        //arrange
        var builder = new RunTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
