using FluentAssertions;
using ServerlessWorkflow.Sdk.Services.IO;
using ServerlessWorkflow.Sdk.Services.Validation;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Services
{

    public class WorkflowValidatorTests
    {

        protected IWorkflowReader Reader { get; } = WorkflowReader.Create();

        protected IWorkflowValidator Validator { get; } = WorkflowValidator.Create();

        [Fact(Skip = "YAML parsing issue for non-complex properties (ex: externalRefs)")]
        public async Task Validate_Yaml_ShouldWork()
        {
            //arrange
            var yaml = File.ReadAllText(Path.Combine("Resources", "Workflows", "operation.yaml"));
            var workflow = await this.Reader.ParseAsync(yaml);

            //act
            var validationResult = await this.Validator.ValidateAsync(workflow);

            //assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validate_Json_ShouldWork()
        {
            //arrange
            var yaml = File.ReadAllText(Path.Combine("Resources", "Workflows", "operation.json"));
            var workflow = await this.Reader.ParseAsync(yaml);

            //act
            var validationResult = await this.Validator.ValidateAsync(workflow);

            //assert
            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
        }

    }

}
