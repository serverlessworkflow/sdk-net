using FluentAssertions;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.Validation;
using System;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation
{

    public class FunctionDefinitionValidationTests
    {

        [Fact]
        public void Validate_Function_WithAutentication_ShouldWork()
        {
            //arrange
            var function = new FunctionDefinition()
            {
                Name = "Fake",
                Operation = "http://fake.com/fake#fake",
                AuthRef = "fake"
            };
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
                .AddBasicAuthentication("fake", auth => auth.LoadFromSecret("fake"))
                .StartsWith("fake", flow =>
                    flow.Execute(action =>
                        action.Invoke(function)))
                .End()
                .Build();
           

            //act
            var result = new FunctionDefinitionValidator(workflow).Validate(function);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .BeNullOrEmpty();
        }

        [Fact]
        public void Validate_Function_NoAuthentication_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
                .StartsWith("fake", flow => 
                    flow.Execute(action => 
                        action.Invoke(function => 
                            function.WithName("fake")
                                .OfType(FunctionType.Rest)
                                .ForOperation(new Uri("http://fake.com/fake#fake"))
                                .UseAuthentication("basic"))))
                .End()
                .Build();
            var function = new FunctionDefinition()
            {
                Name = "Fake",
                Operation = "http://fake.com/fake#fake",
                AuthRef = "fake"
            };

            //act
            var result = new FunctionDefinitionValidator(workflow).Validate(function);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.HaveCount(1)
                .And.Contain(e => e.PropertyName == nameof(FunctionDefinition.AuthRef));
        }

    }

}
