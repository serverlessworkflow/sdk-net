namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SwitchCaseDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Case_With_When_And_Then()
    {
        //arrange
        var whenExpr = "${ .status == \"active\" }";

        //act
        var @case = new SwitchCaseDefinitionBuilder()
            .When(whenExpr)
            .Then(FlowDirective.Continue)
            .Build();

        //assert
        @case.When.Should().Be(whenExpr);
        @case.Then.Should().Be(FlowDirective.Continue);
    }

    [Fact]
    public void Build_Should_Throw_When_Then_Missing()
    {
        //arrange
        var whenExpr = "${ true }";

        //act
        var act = () => new SwitchCaseDefinitionBuilder().When(whenExpr).Build();

        //assert
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Create_Default_Case_Without_When()
    {
        //arrange
        var builder = new SwitchCaseDefinitionBuilder();

        //act
        var @case = builder
            .Then(FlowDirective.End)
            .Build();

        //assert
        @case.When.Should().BeNull();
        @case.Then.Should().Be(FlowDirective.End);
    }

}
