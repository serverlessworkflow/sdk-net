using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="OAuth2AuthenticationProperties"/>s
/// </summary>
internal class OAuth2AuthenticationPropertiesValidator
    : AbstractValidator<OAuth2AuthenticationProperties>
{

    /// <summary>
    /// Initializes a new <see cref="OAuth2AuthenticationPropertiesValidator"/>
    /// </summary>
    public OAuth2AuthenticationPropertiesValidator()
    {
        this.RuleFor(a => a.Authority)
            .NotNull();
        this.RuleFor(a => a.ClientId)
            .NotEmpty();
        this.RuleFor(a => a.Username)
            .NotEmpty()
            .When(a => a.GrantType == OAuth2GrantType.Password);
        this.RuleFor(a => a.Password)
            .NotEmpty()
            .When(a => a.GrantType == OAuth2GrantType.Password);
    }

}
