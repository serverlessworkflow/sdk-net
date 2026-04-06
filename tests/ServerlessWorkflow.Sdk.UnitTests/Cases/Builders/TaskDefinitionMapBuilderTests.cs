namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TaskDefinitionMapBuilderTests
{

    [Fact]
    public void Build_Should_Create_Map_With_Tasks()
    {
        //arrange
        var step1Name = "step1";
        var step1Key = "a";
        var step1Value = "1";
        var step2Name = "step2";
        var step2Key = "b";
        var step2Value = "2";
        var expectedCount = 2;

        //act
        var map = new TaskDefinitionMapBuilder()
            .Do(step1Name, task => task.Set(step1Key, step1Value))
            .Do(step2Name, task => task.Set(step2Key, step2Value))
            .Build();

        //assert
        map.Should().HaveCount(expectedCount);
        map.Keys.Should().Contain(step1Name);
        map.Keys.Should().Contain(step2Name);
    }

    [Fact]
    public void Build_Should_Accept_Prebuilt_Task()
    {
        //arrange
        var taskName = "step";
        var key = "k";
        var value = "v";
        var task = new SetTaskDefinition { Set = new JsonObject { [key] = value } };
        var expectedCount = 1;

        //act
        var map = new TaskDefinitionMapBuilder()
            .Do(taskName, task)
            .Build();

        //assert
        map.Should().HaveCount(expectedCount);
    }

    [Fact]
    public void Build_Should_Throw_When_Empty()
    {
        //arrange
        var builder = new TaskDefinitionMapBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
