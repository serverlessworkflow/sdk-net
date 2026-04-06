namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Builders;

public class SwitchTaskDefinitionBuilderTests
{

    [Fact]
    public void Build_Should_Create_Switch_With_Cases()
    {
        //arrange
        var activeCaseName = "active";
        var defaultCaseName = "default";
        var whenExpr = "${ .status == \"active\" }";
        var expectedCount = 2;

        //act
        var task = new SwitchTaskDefinitionBuilder()
            .Case(activeCaseName, c => c.When(whenExpr).Then(FlowDirective.Continue))
            .Case(defaultCaseName, c => c.Then(FlowDirective.End))
            .Build();

        //assert
        task.Switch.Should().HaveCount(expectedCount);
        task.Switch[activeCaseName].When.Should().Be(whenExpr);
        task.Switch[activeCaseName].Then.Should().Be(FlowDirective.Continue);
        task.Switch[defaultCaseName].When.Should().BeNull();
        task.Switch[defaultCaseName].Then.Should().Be(FlowDirective.End);
    }

}
