using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents a service used to validate <see cref="OperationStateDefinition"/>s
/// </summary>
public class OperationStateValidator
    : StateDefinitionValidator<OperationStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="OperationStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public OperationStateValidator(WorkflowDefinition workflow)
        : base(workflow)
    {
        this.RuleFor(s => s.Actions)
            .NotEmpty()
            .WithErrorCode($"{nameof(OperationStateDefinition)}.{nameof(OperationStateDefinition.Actions)}");
        this.RuleForEach(s => s.Actions)
            .SetValidator(new ActionDefinitionValidator(this.Workflow))
            .When(s => s.Actions != null && s.Actions.Any())
            .WithErrorCode($"{nameof(OperationStateDefinition)}.{nameof(OperationStateDefinition.Actions)}");
    }

}
