namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class DoTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Do_With_Subtasks()
    {
        var task = new DoTaskDefinitionBuilder()
            .Do(tasks =>
            {
                tasks.Do("step1", t => t.Set("a", "1"));
                tasks.Do("step2", t => t.Set("b", "2"));
            })
            .Build();
        task.Do.Should().HaveCount(2);
        task.Do.Keys.Should().Contain("step1");
        task.Do.Keys.Should().Contain("step2");
    }

    [Fact]
    public void Build_Should_Throw_When_Less_Than_Two_Tasks()
    {
        var act = () => new DoTaskDefinitionBuilder()
            .Do(tasks => tasks.Do("only-one", t => t.Set("k", "v")))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Do_Not_Set()
    {
        var act = () => new DoTaskDefinitionBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
