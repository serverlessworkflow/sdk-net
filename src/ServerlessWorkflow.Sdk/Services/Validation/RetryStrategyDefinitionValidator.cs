using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="RetryDefinition"/>s
/// </summary>
public class RetryStrategyDefinitionValidator
    : AbstractValidator<RetryDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="RetryStrategyDefinitionValidator"/>
    /// </summary>
    public RetryStrategyDefinitionValidator()
    {
        this.RuleFor(r => r.Name)
            .NotEmpty()
            .WithErrorCode($"{nameof(RetryDefinition)}.{nameof(RetryDefinition.Name)}");
    }

}
