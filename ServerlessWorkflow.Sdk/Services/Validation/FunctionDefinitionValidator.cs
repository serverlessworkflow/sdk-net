using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="FunctionDefinition"/>s
/// </summary>
internal class FunctionDefinitionValidator
    : AbstractValidator<FunctionDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="FunctionDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to</param>
    public FunctionDefinitionValidator(WorkflowDefinition workflow)
    {
        this.Workflow = workflow;
        this.RuleFor(f => f.Name)
            .NotEmpty()
            .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.Name)}");
        this.RuleFor(f => f.Operation)
            .NotEmpty()
            .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.Operation)}");
        this.RuleFor(f => f.AuthRef!)
            .Must(ReferenceExistingAuthentication)
            .WithErrorCode($"{nameof(FunctionDefinition)}.{nameof(FunctionDefinition.AuthRef)}")
            .WithMessage(f => $"Failed to find an authentication definition with name '{f.AuthRef}'")
            .When(f => !string.IsNullOrWhiteSpace(f.AuthRef));
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionDefinition"/>s to validate belong to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

    /// <summary>
    /// Determines whether or not the specified <see cref="AuthenticationDefinition"/> exists
    /// </summary>
    /// <param name="authenticationName">The name of the <see cref="AuthenticationDefinition"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="AuthenticationDefinition"/> exists</returns>
    protected virtual bool ReferenceExistingAuthentication(string authenticationName) => this.Workflow.TryGetAuthentication(authenticationName, out _);

}
