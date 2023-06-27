using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class OperationStateValidationTests
{

    [Fact]
    public void Validate_OperationState_NoActions_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Events())
            .End()
            .Build();
        var state = new OperationStateDefinition();

        //act
        var result = new OperationStateValidator(workflow).Validate(state);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(OperationStateDefinition.Actions));
    }

}
