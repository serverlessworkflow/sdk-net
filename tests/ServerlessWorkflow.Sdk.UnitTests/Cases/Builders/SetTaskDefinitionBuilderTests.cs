namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SetTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Named_Variables()
    {
        var greetingKey = "greeting";
        var greetingValue = "hello";
        var countKey = "count";
        var countValue = 42;
        var task = new SetTaskDefinitionBuilder()
            .Set(greetingKey, JsonValue.Create(greetingValue))
            .Set(countKey, JsonValue.Create(countValue))
            .Build();
        task.Set[greetingKey]!.GetValue<string>().Should().Be(greetingValue);
        task.Set[countKey]!.GetValue<int>().Should().Be(countValue);
    }

    [Fact]
    public void Build_Should_Accept_JsonObject()
    {
        var xKey = "x";
        var xValue = 1;
        var yKey = "y";
        var yValue = 2;
        var variables = new JsonObject { [xKey] = xValue, [yKey] = yValue };
        var task = new SetTaskDefinitionBuilder()
            .Set(variables)
            .Build();
        task.Set[xKey]!.GetValue<int>().Should().Be(xValue);
        task.Set[yKey]!.GetValue<int>().Should().Be(yValue);
    }

    [Fact]
    public void Build_Should_Configure_If_Condition()
    {
        var condition = "${ .enabled }";
        var key = "k";
        var value = "v";
        var task = new SetTaskDefinitionBuilder()
            .If(condition)
            .Set(key, JsonValue.Create(value))
            .Build();
        task.If.Should().Be(condition);
    }

    [Fact]
    public void Build_Should_Configure_Then_Directive()
    {
        var key = "k";
        var value = "v";
        var task = new SetTaskDefinitionBuilder()
            .Set(key, JsonValue.Create(value))
            .Then(FlowDirective.End)
            .Build();
        task.Then.Should().Be(FlowDirective.End);
    }

    [Fact]
    public void Build_Should_Configure_Timeout_Via_Builder()
    {
        var key = "k";
        var value = "v";
        var timeoutDuration = Duration.FromSeconds(10);
        var task = new SetTaskDefinitionBuilder()
            .Set(key, JsonValue.Create(value))
            .WithTimeout(t => t.After(timeoutDuration))
            .Build();
        task.Timeout.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Configure_Timeout_Via_Reference()
    {
        var key = "k";
        var value = "v";
        var timeoutRef = "my-timeout";
        var task = new SetTaskDefinitionBuilder()
            .Set(key, JsonValue.Create(value))
            .WithTimeout(timeoutRef)
            .Build();
        task.Timeout.Should().NotBeNull();
    }

}
