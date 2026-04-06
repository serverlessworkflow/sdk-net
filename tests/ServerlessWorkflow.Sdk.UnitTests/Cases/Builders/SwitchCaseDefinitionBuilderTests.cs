namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SwitchCaseDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Case_With_When_And_Then()
    {
        var @case = new SwitchCaseDefinitionBuilder()
            .When("${ .status == \"active\" }")
            .Then(FlowDirective.Continue)
            .Build();
        @case.When.Should().Be("${ .status == \"active\" }");
        @case.Then.Should().Be(FlowDirective.Continue);
    }

    [Fact]
    public void Build_Should_Throw_When_Then_Missing()
    {
        var act = () => new SwitchCaseDefinitionBuilder().When("${ true }").Build();
        act.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void Build_Should_Create_Default_Case_Without_When()
    {
        var @case = new SwitchCaseDefinitionBuilder()
            .Then(FlowDirective.End)
            .Build();
        @case.When.Should().BeNull();
        @case.Then.Should().Be(FlowDirective.End);
    }

}
