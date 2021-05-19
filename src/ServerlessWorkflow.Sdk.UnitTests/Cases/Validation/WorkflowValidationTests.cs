using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ServerlessWorkflow.Sdk.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation
{

    public class WorkflowValidationTests
    {

        public WorkflowValidationTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServerlessWorkflow();
            this.ServiceProvider = services.BuildServiceProvider();
            this.WorkflowDefinitionValidator = this.ServiceProvider.GetRequiredService<IValidator<WorkflowDefinition>>();
        }

        protected IServiceProvider ServiceProvider { get; }

        protected IValidator<WorkflowDefinition> WorkflowDefinitionValidator { get; }

        [Fact]
        public void Validate_Workflow_NoId_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Id));
        }

        [Fact]
        public void Validate_Workflow_NoName_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();
            workflowMock.Setup(w => w.Id).Returns("fake");

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Name));
        }

        [Fact]
        public void Validate_Workflow_NoVersion_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();
            workflowMock.Setup(w => w.Id).Returns("fake");
            workflowMock.Setup(w => w.Name).Returns("fake");

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Version));
        }

        [Fact]
        public void Validate_Workflow_NoExpressionLanguage_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();
            workflowMock.Setup(w => w.Id).Returns("fake");
            workflowMock.Setup(w => w.Name).Returns("fake");
            workflowMock.Setup(w => w.Version).Returns("fake");

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.ExpressionLanguage));
        }

        [Fact]
        public void Validate_Workflow_NoStates_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();
            workflowMock.Setup(w => w.Id).Returns("fake");
            workflowMock.Setup(w => w.Name).Returns("fake");
            workflowMock.Setup(w => w.Version).Returns("fake");
            workflowMock.Setup(w => w.ExpressionLanguage).Returns("fake");
            workflowMock.Setup(w => w.Start).Returns(new StartDefinition());

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.States));
        }

        [Fact]
        public void Validate_Workflow_NoStart_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();
            workflowMock.Setup(w => w.Id).Returns("fake");
            workflowMock.Setup(w => w.Name).Returns("fake");
            workflowMock.Setup(w => w.Version).Returns("fake");
            workflowMock.Setup(w => w.ExpressionLanguage).Returns("fake");

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Start));
        }

        [Fact]
        public void Validate_Workflow_NoEnd_ShouldFail()
        {
            //arrange
            var workflow = new WorkflowDefinition();
            workflow.Id = "fake";
            workflow.Name = "fake";
            workflow.Version = "fake";
            workflow.States = new() { new InjectStateDefinition() { Name = "fake" } };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == "End");
        }

        [Fact]
        public void Validate_Workflow_StartStateNotFound_ShouldFail()
        {
            //arrange
            var workflowMock = new Mock<WorkflowDefinition>();
            workflowMock.Setup(w => w.Id).Returns("fake");
            workflowMock.Setup(w => w.Name).Returns("fake");
            workflowMock.Setup(w => w.Version).Returns("fake");
            workflowMock.Setup(w => w.ExpressionLanguage).Returns("fake");
            workflowMock.Setup(w => w.Start).Returns(new StartDefinition() { StateName = "unknown" });
            workflowMock.Setup(w => w.States).Returns(new List<StateDefinition>() { new InjectStateDefinition() { Name = "fake" } });

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflowMock.Object);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Start));
        }

        [Fact]
        public void Validate_Workflow_DuplicateEventNames_ShouldFail()
        {
            //arrange
            var workflow = new WorkflowDefinition();
            workflow.Id = "fake";
            workflow.Name = "fake";
            workflow.Version = "fake";
            workflow.Events = new() { new() { Name = "fake" }, new() { Name = "fake" } };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Events));
        }

        [Fact]
        public void Validate_Workflow_DuplicateFunctionNames_ShouldFail()
        {
            //arrange
            var workflow = new WorkflowDefinition();
            workflow.Id = "fake";
            workflow.Name = "fake";
            workflow.Version = "fake";
            workflow.Functions = new () { new() { Name = "fake" }, new() { Name = "fake" } };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Functions));
        }

        [Fact]
        public void Validate_Workflow_DuplicateStateNames_ShouldFail()
        {
            //arrange
            var workflow = new WorkflowDefinition();
            workflow.Id = "fake";
            workflow.Name = "fake";
            workflow.Version = "fake";
            workflow.States = new() { new InjectStateDefinition() { Name = "fake" }, new InjectStateDefinition() { Name = "fake" } };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.States));
        }

        [Fact]
        public void Validate_Workflow_DuplicateRetryPolicyNames_ShouldFail()
        {
            //arrange
            var workflow = new WorkflowDefinition();
            workflow.Id = "fake";
            workflow.Name = "fake";
            workflow.Version = "fake";
            workflow.Retries = new() { new () { Name = "fake" }, new () { Name = "fake" } };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
               .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Retries));
        }

    }

}
