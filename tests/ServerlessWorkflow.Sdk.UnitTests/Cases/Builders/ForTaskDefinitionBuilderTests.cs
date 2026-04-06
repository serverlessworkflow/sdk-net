namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ForTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Each_In_At_And_Do()
    {
        //arrange
        var eachVar = "item";
        var inExpr = "${ .items }";
        var atVar = "index";
        var taskName = "process";
        var key = "processed";
        var value = "true";
        var expectedCount = 1;

        //act
        var task = new ForTaskDefinitionBuilder()
            .Each(eachVar)
            .In(inExpr)
            .At(atVar)
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        task.For.Each.Should().Be(eachVar);
        task.For.In.Should().Be(inExpr);
        task.For.At.Should().Be(atVar);
        task.Do.Should().HaveCount(expectedCount);
        task.Do.Keys.Should().Contain(taskName);
    }

    [Fact]
    public void Build_Should_Throw_When_Each_Missing()
    {
        //arrange
        var inExpr = "${ .items }";
        var taskName = "step";
        var key = "k";
        var value = "v";

        //act
        var act = () => new ForTaskDefinitionBuilder()
            .In(inExpr)
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_In_Missing()
    {
        //arrange
        var eachVar = "item";
        var taskName = "step";
        var key = "k";
        var value = "v";

        //act
        var act = () => new ForTaskDefinitionBuilder()
            .Each(eachVar)
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Do_Missing()
    {
        //arrange
        var eachVar = "item";
        var inExpr = "${ .items }";

        //act
        var act = () => new ForTaskDefinitionBuilder()
            .Each(eachVar)
            .In(inExpr)
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
