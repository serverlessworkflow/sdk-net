namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ForTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Each_In_At_And_Do()
    {
        var eachVar = "item";
        var inExpr = "${ .items }";
        var atVar = "index";
        var task = new ForTaskDefinitionBuilder()
            .Each(eachVar)
            .In(inExpr)
            .At(atVar)
            .Do(tasks => tasks.Do("process", t => t.Set("processed", "true")))
            .Build();
        task.For.Each.Should().Be(eachVar);
        task.For.In.Should().Be(inExpr);
        task.For.At.Should().Be(atVar);
        task.Do.Should().HaveCount(1);
        task.Do.Keys.Should().Contain("process");
    }

    [Fact]
    public void Build_Should_Throw_When_Each_Missing()
    {
        var act = () => new ForTaskDefinitionBuilder()
            .In("${ .items }")
            .Do(tasks => tasks.Do("step", t => t.Set("k", "v")))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_In_Missing()
    {
        var act = () => new ForTaskDefinitionBuilder()
            .Each("item")
            .Do(tasks => tasks.Do("step", t => t.Set("k", "v")))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Do_Missing()
    {
        var act = () => new ForTaskDefinitionBuilder()
            .Each("item")
            .In("${ .items }")
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
