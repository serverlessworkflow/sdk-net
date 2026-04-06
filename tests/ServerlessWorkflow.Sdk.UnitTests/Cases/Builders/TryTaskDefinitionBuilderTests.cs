namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TryTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Try_With_Tasks_And_Catch()
    {
        var task = new TryTaskDefinitionBuilder()
            .Do(tasks => tasks.Do("risky", t => t.Set("k", "v")))
            .Catch(c => { })
            .Build();
        task.Try.Should().HaveCount(1);
        task.Try.Keys.Should().Contain("risky");
        task.Catch.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Try_Tasks_Missing()
    {
        var act = () => new TryTaskDefinitionBuilder()
            .Catch(c => { })
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Catch_Missing()
    {
        var act = () => new TryTaskDefinitionBuilder()
            .Do(tasks => tasks.Do("step", t => t.Set("k", "v")))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
