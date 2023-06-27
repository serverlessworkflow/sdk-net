using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="TransitionDefinition"/>s
/// </summary>
public class TransitionDefinitionValidator
    : AbstractValidator<TransitionDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="TransitionDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="TransitionDefinition"/>s to validate belong to</param>
    public TransitionDefinitionValidator(WorkflowDefinition workflow)
    {
        this.Workflow = workflow;
        this.RuleFor(t => t.NextState)
            .NotEmpty();
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="TransitionDefinition"/>s to validate belong to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

}
