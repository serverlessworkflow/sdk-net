using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class ActionValidationTests
{

    [Fact]
    public void Validate_Action_NoFunctionNorEvent_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();
        var action = new ActionDefinition();

        //act
        var result = new ActionDefinitionValidator(workflow).Validate(action);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(3)
            .And.Contain(e => e.PropertyName == nameof(ActionDefinition.Event))
            .And.Contain(e => e.PropertyName == nameof(ActionDefinition.Function))
            .And.Contain(e => e.PropertyName == nameof(ActionDefinition.Subflow));
    }

}
