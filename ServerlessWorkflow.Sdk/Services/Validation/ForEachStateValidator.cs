using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents a service used to validate <see cref="ForEachStateDefinition"/>s
/// </summary>
internal class ForEachStateValidator
    : StateDefinitionValidator<ForEachStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="ForEachStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public ForEachStateValidator(WorkflowDefinition workflow)
        : base(workflow)
    {
        this.RuleFor(s => s.Actions)
            .NotEmpty()
            .WithErrorCode($"{nameof(ForEachStateDefinition)}.{nameof(ForEachStateDefinition.Actions)}");
        this.RuleForEach(s => s.Actions)
            .SetValidator(new ActionDefinitionValidator(this.Workflow))
            .When(s => s.Actions != null && s.Actions.Any())
            .WithErrorCode($"{nameof(ForEachStateDefinition)}.{nameof(ForEachStateDefinition.Actions)}");
    }

}
