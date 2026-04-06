namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TaskDefinitionMapBuilderTests
{

    [Fact]
    public void Build_Should_Create_Map_With_Tasks()
    {
        var map = new TaskDefinitionMapBuilder()
            .Do("step1", task => task.Set("a", "1"))
            .Do("step2", task => task.Set("b", "2"))
            .Build();
        map.Should().HaveCount(2);
        map.Keys.Should().Contain("step1");
        map.Keys.Should().Contain("step2");
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Task()
    {
        var task = new SetTaskDefinition { Set = new JsonObject { ["k"] = "v" } };
        var map = new TaskDefinitionMapBuilder()
            .Do("step", task)
            .Build();
        map.Should().HaveCount(1);
    }

    [Fact]
    public void Build_Should_Throw_When_Empty()
    {
        var act = () => new TaskDefinitionMapBuilder().Build();
        act.Should().Throw<NullReferenceException>();
    }

}
