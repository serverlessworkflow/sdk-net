using FluentAssertions;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.Validation;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation
{

    public class FunctionReferenceValidationTests
    {

        [Fact]
        public void Validate_FunctionReference_NameNotSet_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
                .StartsWith("fake", flow => flow.Callback())
                .End()
                .Build();
            var functionRef = new FunctionReference();

            //act
            var result = new FunctionReferenceValidator(workflow).Validate(functionRef);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(FunctionReference.Name));
        }

        [Fact]
        public void Validate_FunctionReference_FunctionNotFound_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
               .StartsWith("fake", flow => flow.Callback())
               .End()
               .Build();
            var functionRef = new FunctionReference() { Name = "fake" };

            //act
            var result = new FunctionReferenceValidator(workflow).Validate(functionRef);

            //assert
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(FunctionReference.Name));
        }

    }

}
