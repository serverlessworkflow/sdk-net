using ServerlessWorkflow.Sdk.Services.Validation;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class WorkflowValidationTests
{

    [Fact]
    public async Task Validate_WorkflowDefinition_Should_Work()
    {
        //arrange
        var workflow = WorkflowDefinitionFactory.Create();
        var validator = WorkflowSchemaValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow).ConfigureAwait(false);

        //assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_WorkflowDefinition_WithStateTypeExtensions_Should_Work()
    {
        //arrange
        var workflow = new WorkflowBuilder()
            .WithId("fake")
            .WithName("Fake Workflow")
            .WithDescription("Fake Workflow Description")
            .WithSpecVersion(ServerlessWorkflowSpecVersion.Latest)
            .WithVersion("1.0.0")
            .UseExtension("fake-extension", new($"file://{Path.Combine(AppContext.BaseDirectory, "Assets", "WorkflowExtensions", "condition-state-type.json")}"))
            .StartsWith("fake-state", flow => flow
                .Extension("condition")
                .WithExtensionProperty("if", new { condition = "${ true }", action = new { name = "fake", functionRef = new FunctionReference() { RefName = "fake-function" } } })
                .WithExtensionProperty("else", new { action = new { name = "fake", functionRef = new FunctionReference() { RefName = "fake-function" } } }))
            .End()
            .Build();
        var validator = WorkflowSchemaValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow).ConfigureAwait(false);

        //asserts
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_WorkflowDefinition_WithFunctionTypeExtension_Should_Work()
    {
        //arrange
        var workflow = new WorkflowBuilder()
            .WithId("fake")
            .WithName("Fake Workflow")
            .WithDescription("Fake Workflow Description")
            .WithSpecVersion(ServerlessWorkflowSpecVersion.Latest)
            .WithVersion("1.0.0")
            .UseExtension("fake-extension", new($"file://{Path.Combine(AppContext.BaseDirectory, "Assets", "WorkflowExtensions", "greet-function-type.json")}"))
            .StartsWith("fake-state", flow => flow
                .Execute(action => action
                    .Invoke(function => function
                        .ForOperation("https://unittests.sdk-net.serverlessworkflow.io#fake-operation")
                        .OfType("greet")
                        .WithName("greet"))))
            .End()
            .Build();
        var validator = WorkflowSchemaValidator.Create();

        //act
        var result = await validator.ValidateAsync(workflow).ConfigureAwait(false);

        //assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

}
