namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class TryTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Try_With_Tasks_And_Catch()
    {
        //arrange
        var taskName = "risky";
        var key = "k";
        var value = "v";
        var expectedCount = 1;

        //act
        var task = new TryTaskDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Catch(c => { })
            .Build();

        //assert
        task.Try.Should().HaveCount(expectedCount);
        task.Try.Keys.Should().Contain(taskName);
        task.Catch.Should().NotBeNull();
    }

    [Fact]
    public void Build_Should_Throw_When_Try_Tasks_Missing()
    {
        //arrange
        var builder = new TryTaskDefinitionBuilder();

        //act
        var act = () => builder
            .Catch(c => { })
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Throw_When_Catch_Missing()
    {
        //arrange
        var taskName = "step";
        var key = "k";
        var value = "v";

        //act
        var act = () => new TryTaskDefinitionBuilder()
            .Do(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
