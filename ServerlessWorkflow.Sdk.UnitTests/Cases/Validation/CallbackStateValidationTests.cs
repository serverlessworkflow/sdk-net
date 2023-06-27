using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;

public class CallbackStateValidationTests
{

    public CallbackStateValidationTests()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddServerlessWorkflow();
        this.ServiceProvider = services.BuildServiceProvider();
        this.WorkflowDefinitionValidator = this.ServiceProvider.GetRequiredService<IValidator<WorkflowDefinition>>();
    }

    protected IServiceProvider ServiceProvider { get; }

    protected IValidator<WorkflowDefinition> WorkflowDefinitionValidator { get; }

    [Fact]
    public void Validate_CallbackState_ActionNull_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();

        //act
        var result = this.WorkflowDefinitionValidator.Validate(workflow);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.ErrorCode.StartsWith($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.Action)}"));
    }

    [Fact]
    public void Validate_CallbackState_EventNull_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Callback())
            .End()
            .Build();

        //act
        var result = this.WorkflowDefinitionValidator.Validate(workflow);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.ErrorCode.Contains($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.EventRef)}"));
    }

    [Fact]
    public void Validate_CallbackState_EventNotFound_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => 
                flow.Callback()
                    .On("fake")
                    .Execute(action => action.Invoke("fake")))
            .End()
            .Build();

        //act
        var result = this.WorkflowDefinitionValidator.Validate(workflow);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.ErrorCode.Contains($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.EventRef)}"));
    }

}
