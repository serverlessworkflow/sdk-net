using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ServerlessWorkflow.Sdk.UnitTests.Cases.Validation;
public class InjectStateValidationTests
{

    public InjectStateValidationTests()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddServerlessWorkflow();
        this.ServiceProvider = services.BuildServiceProvider();
        this.WorkflowDefinitionValidator = this.ServiceProvider.GetRequiredService<IValidator<WorkflowDefinition>>();
    }

    protected IServiceProvider ServiceProvider { get; }

    protected IValidator<WorkflowDefinition> WorkflowDefinitionValidator { get; }

    [Fact]
    public void Validate_InjectState_NoData_ShouldFail()
    {
        //arrange
        var workflow = WorkflowDefinition.Create("fake", "fake", "fake")
            .StartsWith("fake", flow => flow.Inject())
            .End()
            .Build();

        //act
        var result = this.WorkflowDefinitionValidator.Validate(workflow);

        //assert
        result.Should()
            .NotBeNull();
        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.Contain(e => e.ErrorCode.StartsWith($"{nameof(InjectStateDefinition)}.{nameof(InjectStateDefinition.Data)}"));
    }

}
