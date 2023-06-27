using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents a service used to validate <see cref="CallbackStateDefinition"/>s
/// </summary>
public class CallbackStateValidator
    : StateDefinitionValidator<CallbackStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="CallbackStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public CallbackStateValidator(WorkflowDefinition workflow) 
        : base(workflow)
    {
        this.RuleFor(s => s.Action)
            .NotNull()
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.Action)}");
        this.RuleFor(s => s.Action!)
            .SetValidator(new ActionDefinitionValidator(workflow))
            .When(s => s.Action != null)
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.Action)}");
        this.RuleFor(s => s.EventRef)
            .NotEmpty()
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.EventRef)}");
        this.RuleFor(s => s.EventRef!)
            .Must(ReferenceExistingEvent)
            .When(s => !string.IsNullOrWhiteSpace(s.EventRef))
            .WithErrorCode($"{nameof(CallbackStateDefinition)}.{nameof(CallbackStateDefinition.EventRef)}")
            .WithMessage((state, eventRef) => $"Failed to find the event with name '{eventRef}' defined by the callback state with name '{state.Name}'");
    }

}
