namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ForkTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Branches_And_Compete()
    {
        //arrange
        var branch1Name = "branch1";
        var branch1Key = "a";
        var branch1Value = "1";
        var branch2Name = "branch2";
        var branch2Key = "b";
        var branch2Value = "2";
        var expectedCount = 2;

        //act
        var task = new ForkTaskDefinitionBuilder()
            .Branch(tasks =>
            {
                tasks.Do(branch1Name, t => t.Set(branch1Key, branch1Value));
                tasks.Do(branch2Name, t => t.Set(branch2Key, branch2Value));
            })
            .Compete()
            .Build();

        //assert
        task.Fork.Branches.Should().HaveCount(expectedCount);
        task.Fork.Branches.Keys.Should().Contain(branch1Name);
        task.Fork.Branches.Keys.Should().Contain(branch2Name);
        task.Fork.Compete.Should().BeTrue();
    }

    [Fact]
    public void Build_Should_Default_Compete_To_False()
    {
        //arrange
        var b1Name = "b1";
        var b1Key = "a";
        var b1Value = "1";
        var b2Name = "b2";
        var b2Key = "b";
        var b2Value = "2";

        //act
        var task = new ForkTaskDefinitionBuilder()
            .Branch(tasks =>
            {
                tasks.Do(b1Name, t => t.Set(b1Key, b1Value));
                tasks.Do(b2Name, t => t.Set(b2Key, b2Value));
            })
            .Build();

        //assert
        task.Fork.Compete.Should().BeFalse();
    }

    [Fact]
    public void Build_Should_Throw_When_Less_Than_Two_Branches()
    {
        //arrange
        var taskName = "only-one";
        var key = "k";
        var value = "v";

        //act
        var act = () => new ForkTaskDefinitionBuilder()
            .Branch(tasks => tasks.Do(taskName, t => t.Set(key, value)))
            .Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

}
