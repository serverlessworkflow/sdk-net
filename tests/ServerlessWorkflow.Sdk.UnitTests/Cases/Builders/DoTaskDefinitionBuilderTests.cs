namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class DoTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Do_With_Subtasks()
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
        var task = new DoTaskDefinitionBuilder()
            .Do(tasks =>
            {
                tasks.Do(step1Name, t => t.Set(step1Key, step1Value));
                tasks.Do(step2Name, t => t.Set(step2Key, step2Value));
            })
            .Build();

        //assert
        task.Do.Should().HaveCount(expectedCount);
        task.Do.Keys.Should().Contain(step1Name);
        task.Do.Keys.Should().Contain(step2Name);
    }

    [Fact]
    public void Build_Should_Throw_When_Less_Than_Two_Tasks()
    {
        //arrange
        var taskName = "only-one";
        var key = "k";
        var value = "v";

        //act
        var act = () => new DoTaskDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Do_Not_Set()
    {
        //arrange
        var builder = new DoTaskDefinitionBuilder();

        //act
        var act = () => builder.Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
