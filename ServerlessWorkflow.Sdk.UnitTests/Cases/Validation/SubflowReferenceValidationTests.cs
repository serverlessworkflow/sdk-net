using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class SubflowReferenceValidationTests
{

    [Fact]
    public void Validate_SubflowReference_WorkflowIdNotSet_ShouldFail()
    {
        //arrange
        var subflowRef = new SubflowReference();

        //act
        var result = new SubflowReferenceValidator(new()).Validate(subflowRef);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.PropertyName == nameof(SubflowReference.WorkflowId));
    }

}
