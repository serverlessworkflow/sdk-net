using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using ServerlessWorkflow.Sdk.Models;
using ServerlessWorkflow.Sdk.Services.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        protected IWorkflowValidator WorkflowValidator = Sdk.Services.Validation.WorkflowValidator.Create();

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
        public void Validate_Workflow_NoStart_ShouldSucceed()
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
                .NotBeNullOrEmpty();
        }

        [Fact]
        public void Validate_Workflow_NoEnd_ShouldFail()
        {
            //arrange
            var workflow = new WorkflowDefinition
            {
                Id = "fake",
                Name = "fake",
                Version = "fake",
                States = new() { new InjectStateDefinition() { Name = "fake", Data = new() } }
            };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(StateDefinition.End));
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
            var workflow = new WorkflowDefinition
            {
                Id = "fake",
                Name = "fake",
                Version = "fake",
                Events = new() { new() { Name = "fake", Source = "fake", Type = "fake" }, new() { Name = "fake", Source = "fake", Type = "fake" } },
                StartStateName = "sleep",
                States = new() { new SleepStateDefinition() {Name = "sleep", Duration = TimeSpan.FromSeconds(2), IsEnd = true } }
            };

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
            var workflow = new WorkflowDefinition
            {
                Id = "fake",
                Name = "fake",
                Version = "fake",
                Functions = new() { new() { Name = "fake", Operation = "fake" }, new() { Name = "fake", Operation = "fake" } }
            };

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
            var workflow = new WorkflowDefinition
            {
                Id = "fake",
                Name = "fake",
                Version = "fake",
                States = new() { new InjectStateDefinition() { Name = "fake" }, new InjectStateDefinition() { Name = "fake" } }
            };

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
            var workflow = new WorkflowDefinition
            {
                Id = "fake",
                Name = "fake",
                Version = "fake",
                Retries = new() { new() { Name = "fake" }, new() { Name = "fake" } }
            };

            //act
            var result = this.WorkflowDefinitionValidator.Validate(workflow);

            //assert
            result.Should()
                .NotBeNull();
            result.Errors.Should()
                .NotBeNullOrEmpty()
                .And.Contain(e => e.PropertyName == nameof(WorkflowDefinition.Retries));
        }

        [Fact]
        public async Task Validate_Workflow_WithExternalReferences_ShouldWork()
        {
            //arrange
            var json = File.ReadAllText(Path.Combine("resources", "workflows", "external-function-definition.json"));
            var workflow = JsonConvert.DeserializeObject<WorkflowDefinition>(json);

            //act
            var result = await this.WorkflowValidator.ValidateAsync(workflow);
            var loadedWorflowJson = JsonConvert.SerializeObject(workflow);
            var deserializedWorkflow = JsonConvert.DeserializeObject<WorkflowDefinition>(loadedWorflowJson);

            //assert
            result.IsValid.Should().BeTrue();
            deserializedWorkflow.Functions.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Validate_Workflow_WithInputDataSchema_ShouldWork()
        {
            //arrange
            var json = File.ReadAllText(Path.Combine("resources", "workflows", "input-data-schema.json"));
            var workflow = JsonConvert.DeserializeObject<WorkflowDefinition>(json);

            //act
            var result = await this.WorkflowValidator.ValidateAsync(workflow);
            var loadedWorflowJson = JsonConvert.SerializeObject(workflow);
            var deserializedWorkflow = JsonConvert.DeserializeObject<WorkflowDefinition>(loadedWorflowJson);

            //assert
            result.IsValid.Should().BeTrue();
            deserializedWorkflow.DataInputSchema.Should().NotBeNull();
        }

        [Fact]
        public async Task Validate_Workflow_WithExternalInputDataSchema_ShouldWork()
        {
            //arrange
            var json = File.ReadAllText(Path.Combine("resources", "workflows", "external-input-data-schema.json"));
            var workflow = JsonConvert.DeserializeObject<WorkflowDefinition>(json);

            //act
            var result = await this.WorkflowValidator.ValidateAsync(workflow);
            var loadedWorflowJson = JsonConvert.SerializeObject(workflow);
            var deserializedWorkflow = JsonConvert.DeserializeObject<WorkflowDefinition>(loadedWorflowJson);

            //assert
            result.IsValid.Should().BeTrue();
            deserializedWorkflow.DataInputSchemaUri.Should().NotBeNull();
        }

    }

}
