namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class ForkTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Set_Branches_And_Compete()
    {
        var task = new ForkTaskDefinitionBuilder()
            .Branch(tasks =>
            {
                tasks.Do("branch1", t => t.Set("a", "1"));
                tasks.Do("branch2", t => t.Set("b", "2"));
            })
            .Compete()
            .Build();
        task.Fork.Branches.Should().HaveCount(2);
        task.Fork.Branches.Keys.Should().Contain("branch1");
        task.Fork.Branches.Keys.Should().Contain("branch2");
        task.Fork.Compete.Should().BeTrue();
    }

    [Fact]
    public void Build_Should_Default_Compete_To_False()
    {
        var task = new ForkTaskDefinitionBuilder()
            .Branch(tasks =>
            {
                tasks.Do("b1", t => t.Set("a", "1"));
                tasks.Do("b2", t => t.Set("b", "2"));
            })
            .Build();
        task.Fork.Compete.Should().BeFalse();
    }

    [Fact]
    public void Build_Should_Throw_When_Less_Than_Two_Branches()
    {
        var act = () => new ForkTaskDefinitionBuilder()
            .Branch(tasks => tasks.Do("only-one", t => t.Set("k", "v")))
            .Build();
        act.Should().Throw<NullReferenceException>();
    }

}
