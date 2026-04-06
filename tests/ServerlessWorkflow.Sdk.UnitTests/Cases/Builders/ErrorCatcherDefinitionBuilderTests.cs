namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ErrorCatcherDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Catcher_With_All_Properties()
    {
        var asVar = "error";
        var whenExpr = "${ .status == 404 }";
        var exceptWhenExpr = "${ .ignore }";
        var catcher = new ErrorCatcherDefinitionBuilder()
            .As(asVar)
            .When(whenExpr)
            .ExceptWhen(exceptWhenExpr)
            .Build();
        catcher.As.Should().Be(asVar);
        catcher.When.Should().Be(whenExpr);
        catcher.ExceptWhen.Should().Be(exceptWhenExpr);
    }

    [Fact]
    public void Build_Should_Configure_Do_Tasks()
    {
        var taskName = "log";
        var catcher = new ErrorCatcherDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set("logged", "true")))
            .Build();
        catcher.Do.Should().HaveCount(1);
        catcher.Do!.Keys.Should().Contain(taskName);
    }

    [Fact]
    public void Build_Should_Create_Empty_Catcher_With_Null_Properties()
    {
        var catcher = new ErrorCatcherDefinitionBuilder().Build();
        catcher.As.Should().BeNull();
        catcher.When.Should().BeNull();
        catcher.ExceptWhen.Should().BeNull();
        catcher.Do.Should().BeNull();
        catcher.Errors.Should().BeNull();
    }

}
