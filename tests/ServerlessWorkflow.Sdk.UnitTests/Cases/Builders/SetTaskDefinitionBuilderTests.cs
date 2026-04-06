namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SetTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Named_Variables()
    {
        //arrange
        var greetingKey = "greeting";
        var greetingValue = "hello";
        var countKey = "count";
        var countValue = 42;

        //act
        var task = new SetTaskDefinitionBuilder()
            .Set(greetingKey, JsonValue.Create(greetingValue))
            .Set(countKey, JsonValue.Create(countValue))
            .Build();

        //assert
        task.Set[greetingKey]!.GetValue<string>().Should().Be(greetingValue);
        task.Set[countKey]!.GetValue<int>().Should().Be(countValue);
    }

    [Fact]
    public void Build_Should_Accept_JsonObject()
    {
        //arrange
        var xKey = "x";
        var xValue = 1;
        var yKey = "y";
        var yValue = 2;
        var variables = new JsonObject { [xKey] = xValue, [yKey] = yValue };

        //act
        var task = new SetTaskDefinitionBuilder()
            .Set(variables)
            .Build();

        //assert
        task.Set[xKey]!.GetValue<int>().Should().Be(xValue);
        task.Set[yKey]!.GetValue<int>().Should().Be(yValue);
    }

    [Fact]
    public void Build_Should_Configure_If_Condition()
    {
        //arrange
        var condition = "${ .enabled }";
        var key = "k";
        var value = "v";

        //act
        var task = new SetTaskDefinitionBuilder()
            .If(condition)
            .Set(key, JsonValue.Create(value))
            .Build();

        //assert
        task.If.Should().Be(condition);
    }

    [Fact]
    public void Build_Should_Configure_Then_Directive()
    {
        //arrange
        var key = "k";
        var value = "v";

        //act
        var task = new SetTaskDefinitionBuilder()
            .Set(key, JsonValue.Create(value))
            .Then(FlowDirective.End)
            .Build();

        //assert
        task.Then.Should().Be(FlowDirective.End);
    }

    [Fact]
    public void Build_Should_Configure_Timeout_Via_Builder()
    {
        //arrange
        var key = "k";
        var value = "v";
        var timeoutDuration = Duration.FromSeconds(10);

        //act
        var task = new SetTaskDefinitionBuilder()
            .Set(key, JsonValue.Create(value))
            .WithTimeout(t => t.After(timeoutDuration))
            .Build();

        //assert
        task.Timeout.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Configure_Timeout_Via_Reference()
    {
        //arrange
        var key = "k";
        var value = "v";
        var timeoutRef = "my-timeout";

        //act
        var task = new SetTaskDefinitionBuilder()
            .Set(key, JsonValue.Create(value))
            .WithTimeout(timeoutRef)
            .Build();

        //assert
        task.Timeout.Should().NotBeNull();
    }

}
