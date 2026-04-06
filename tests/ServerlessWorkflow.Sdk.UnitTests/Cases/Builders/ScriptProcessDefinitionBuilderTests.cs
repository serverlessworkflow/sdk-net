namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ScriptProcessDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Script_With_Language_And_Code()
    {
        //arrange
        var language = "javascript";
        var code = "console.log('hello')";

        //act
        var script = new ScriptProcessDefinitionBuilder()
            .WithLanguage(language)
            .WithCode(code)
            .Build();

        //assert
        script.Language.Should().Be(language);
        script.Code.Should().Be(code);
    }

    [Fact]
    public void Build_Should_Create_Script_With_Source()
    {
        //arrange
        var language = "python";
        var sourceUri = new Uri("https://scripts.example.com/run.py");

        //act
        var script = new ScriptProcessDefinitionBuilder()
            .WithLanguage(language)
            .WithSource(s => s.WithEndpoint(e => e.WithUri(sourceUri)))
            .Build();

        //assert
        script.Language.Should().Be(language);
        script.Source.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Create_Script_With_Arguments_And_Environment()
    {
        //arrange
        var language = "bash";
        var code = "echo $MSG";
        var argName = "verbose";
        var argValue = "true";
        var envName = "MSG";
        var envValue = "hello";

        //act
        var script = new ScriptProcessDefinitionBuilder()
            .WithLanguage(language)
            .WithCode(code)
            .WithArgument(argName, argValue)
            .WithEnvironment(envName, envValue)
            .Build();

        //assert
        script.Arguments.Should().ContainKey(argName);
        script.Environment.Should().ContainKey(envName);
    }

    [Fact]
    public void Build_Should_Throw_When_Language_Missing()
    {
        //arrange
        var code = "print('hi')";

        //act
        var act = () => new ScriptProcessDefinitionBuilder()
            .WithCode(code)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
