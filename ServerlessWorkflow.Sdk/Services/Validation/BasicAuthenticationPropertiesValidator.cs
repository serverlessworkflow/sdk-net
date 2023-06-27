using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="BasicAuthenticationProperties"/>s
/// </summary>
internal class BasicAuthenticationPropertiesValidator
    : AbstractValidator<BasicAuthenticationProperties>
{

    /// <summary>
    /// Initializes a new <see cref="BasicAuthenticationPropertiesValidator"/>
    /// </summary>
    public BasicAuthenticationPropertiesValidator()
    {
        this.RuleFor(p => p.Username)
            .NotEmpty();
        this.RuleFor(p => p.Password)
            .NotEmpty();
    }

}
