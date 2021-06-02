using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServerlessWorkflow.Sdk.Models;
using System;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation
{
    public class SwitchStateValidationTests
    {

        public SwitchStateValidationTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServerlessWorkflow();
            this.ServiceProvider = services.BuildServiceProvider();
            this.WorkflowDefinitionValidator = this.ServiceProvider.GetRequiredService<IValidator<WorkflowDefinition>>();
        }

        protected IServiceProvider ServiceProvider { get; }

        protected IValidator<WorkflowDefinition> WorkflowDefinitionValidator { get; }

        [Fact]
        public void Validate_SwitchState_NoDataOrEventConditions_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
                .StartsWith("fake", flow => flow.Switch())
                .End()
                .Build();

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.ErrorCode == "States[0]" && e.ErrorMessage.Contains($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DataConditions)}"))
                .And.Contain(e => e.ErrorCode == "States[0]" && e.ErrorMessage.Contains($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.EventConditions)}"));
        }

        [Fact]
        public void Validate_SwitchState_NoDefaultCondition_ShouldFail()
        {
            //arrange
            var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
                .StartsWith("fake", flow => flow.Switch())
                .End()
                .Build();

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.ErrorCode == "States[0]" && e.ErrorMessage.Contains($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DefaultCondition)}"));
        }

    }

}
