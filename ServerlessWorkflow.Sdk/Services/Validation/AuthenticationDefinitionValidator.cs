using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="AuthenticationDefinition"/>s
/// </summary>
internal class AuthenticationDefinitionValidator
    : AbstractValidator<AuthenticationDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="AuthenticationDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="AuthenticationDefinition"/>s to validate belong to</param>
    public AuthenticationDefinitionValidator(WorkflowDefinition workflow)
    {
        this.Workflow = workflow;
        this.RuleFor(a => a.Name)
            .NotEmpty()
            .WithErrorCode($"{nameof(AuthenticationDefinition)}.{nameof(AuthenticationDefinition.Name)}");
        this.RuleFor(a => a.Properties)
            .NotNull()
            .WithErrorCode($"{nameof(AuthenticationDefinition)}.{nameof(AuthenticationDefinition.Properties)}");
        this.RuleFor(a => a.Properties)
            .Must(BeOfExpectedType)
            .WithErrorCode($"{nameof(AuthenticationDefinition)}.{nameof(AuthenticationDefinition.Properties)}")
            .WithMessage((auth, properties) => $"The specified properties object cannot be discriminated against the expected type for scheme '{auth.Scheme}'")
            .When(a => a.Properties != null);
        this.RuleFor(a => a.Properties)
            .SetInheritanceValidator(v =>
            {
                v.Add(new BasicAuthenticationPropertiesValidator());
                v.Add(new BearerAuthenticationPropertiesValidator());
                v.Add(new OAuth2AuthenticationPropertiesValidator());
            });
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="AuthenticationDefinition"/>s to validate belong to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

    /// <summary>
    /// Determines whether or not the specified <see cref="AuthenticationProperties"/> match the defined <see cref="AuthenticationDefinition.Scheme"/> and are valid
    /// </summary>
    /// <param name="authentication">The <see cref="AuthenticationDefinition"/> to check</param>
    /// <param name="properties">The <see cref="AuthenticationProperties"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="AuthenticationProperties"/> match the defined <see cref="AuthenticationDefinition.Scheme"/> and are valid</returns>
    protected virtual bool BeOfExpectedType(AuthenticationDefinition authentication, AuthenticationProperties properties)
    {
        return properties switch
        {
            BasicAuthenticationProperties => authentication.Scheme == AuthenticationScheme.Basic,
            BearerAuthenticationProperties => authentication.Scheme == AuthenticationScheme.Bearer,
            OAuth2AuthenticationProperties => authentication.Scheme == AuthenticationScheme.OAuth2,
            SecretBasedAuthenticationProperties => true,
            _ => false,
        };
    }

}
