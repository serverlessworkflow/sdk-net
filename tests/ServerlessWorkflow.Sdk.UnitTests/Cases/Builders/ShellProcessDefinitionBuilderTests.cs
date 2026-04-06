namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ShellProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Shell_With_All_Properties()
    {
        var shell = new ShellProcessDefinitionBuilder()
            .WithCommand("echo hello")
            .WithArgument("--verbose")
            .WithEnvironment("PATH", "/usr/bin")
            .Build();
        shell.Command.Should().Be("echo hello");
        shell.Arguments.Should().Contain("--verbose");
        shell.Environment.Should().ContainKey("PATH");
    }

    [Fact]
    public void Build_Should_Accept_Bulk_Arguments_And_Environment()
    {
        var command = "ls";
        var arg1 = "-l";
        var arg2 = "-a";
        var envKey = "HOME";
        var envValue = "/root";
        var shell = new ShellProcessDefinitionBuilder()
            .WithCommand(command)
            .WithArguments([arg1, arg2])
            .WithEnvironment(new Dictionary<string, string> { [envKey] = envValue })
            .Build();
        shell.Command.Should().Be(command);
        shell.Arguments.Should().HaveCount(2);
        shell.Arguments.Should().Contain(arg1);
        shell.Arguments.Should().Contain(arg2);
        shell.Environment![envKey].Should().Be(envValue);
    }

    [Fact]
    public void Build_Should_Throw_When_Command_Missing()
    {
        var act = () => new ShellProcessDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
