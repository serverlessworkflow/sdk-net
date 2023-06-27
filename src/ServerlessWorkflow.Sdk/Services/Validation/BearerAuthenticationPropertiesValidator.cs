using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="BearerAuthenticationProperties"/>s
/// </summary>
public class BearerAuthenticationPropertiesValidator
    : AbstractValidator<BearerAuthenticationProperties>
{

    /// <summary>
    /// Initializes a new <see cref="BearerAuthenticationPropertiesValidator"/>
    /// </summary>
    public BearerAuthenticationPropertiesValidator()
    {
        this.RuleFor(p => p.Token)
            .NotEmpty();
    }

}
