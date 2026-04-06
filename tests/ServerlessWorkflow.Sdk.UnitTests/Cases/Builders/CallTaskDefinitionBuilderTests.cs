namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class CallTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Function_And_Arguments()
    {
        var functionName = "myFunction";
        var argName = "arg1";
        var argValue = "value1";
        var task = new CallTaskDefinitionBuilder()
            .Function(functionName)
            .With(argName, JsonValue.Create(argValue))
            .Build();
        task.Call.Should().Be(functionName);
        task.With![argName]!.GetValue<string>().Should().Be(argValue);
    }

    [Fact]
    public void Build_Should_Accept_Function_Via_Constructor()
    {
        var functionName = "presetFunc";
        var task = new CallTaskDefinitionBuilder(functionName).Build();
        task.Call.Should().Be(functionName);
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Arguments()
    {
        var functionName = "fn";
        var key = "key";
        var value = "val";
        var args = new JsonObject { [key] = value };
        var task = new CallTaskDefinitionBuilder()
            .Function(functionName)
            .With(args)
            .Build();
        task.With![key]!.GetValue<string>().Should().Be(value);
    }

    [Fact]
    public void Build_Should_Throw_When_Function_Missing()
    {
        var act = () => new CallTaskDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
