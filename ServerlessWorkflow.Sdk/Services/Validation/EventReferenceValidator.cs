using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the service used to validate <see cref="EventReference"/>s
/// </summary>
public class EventReferenceValidator
    : AbstractValidator<EventReference>
{

    /// <summary>
    /// Initializes a new <see cref="EventReferenceValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="EventReference"/>s to validate belong to</param>
    public EventReferenceValidator(WorkflowDefinition workflow)
    {
        this.Workflow = workflow;
        this.RuleFor(e => e.TriggerEventRef)
            .NotEmpty()
            .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.TriggerEventRef)}");
        this.RuleFor(e => e.TriggerEventRef)
            .Must(ReferenceExistingEvent)
            .When(e => !string.IsNullOrWhiteSpace(e.TriggerEventRef))
            .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.TriggerEventRef)}")
            .WithMessage(eventRef => $"Failed to find the event with name '{eventRef.TriggerEventRef}'");
        this.RuleFor(e => e.TriggerEventRef)
            .Must(BeProduced)
            .When(e => !string.IsNullOrWhiteSpace(e.TriggerEventRef))
            .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.TriggerEventRef)}")
            .WithMessage(eventRef => $"The event with name '{eventRef.TriggerEventRef}' must be of kind '{EventKind.Produced}'");
        this.RuleFor(e => e.ResultEventRef)
            .NotEmpty()
            .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ResultEventRef)}");
        this.RuleFor(e => e.ResultEventRef)
            .Must(ReferenceExistingEvent!)
            .When(e => !string.IsNullOrWhiteSpace(e.ResultEventRef))
            .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ResultEventRef)}")
            .WithMessage(eventRef => $"Failed to find the event with name '{eventRef.ResultEventRef}'");
        this.RuleFor(e => e.ResultEventRef)
            .Must(BeConsumed!)
            .When(e => !string.IsNullOrWhiteSpace(e.ResultEventRef))
            .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ResultEventRef)}")
            .WithMessage(eventRef => $"The event with name '{eventRef.ResultEventRef}' must be of kind '{EventKind.Consumed}'");
    }

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> the <see cref="FunctionReference"/>s to validate belong to
    /// </summary>
    protected WorkflowDefinition Workflow { get; }

    /// <summary>
    /// Determines whether or not the specified <see cref="EventDefinition"/> exists
    /// </summary>
    /// <param name="eventName">The name of the <see cref="EventDefinition"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> exists</returns>
    protected virtual bool ReferenceExistingEvent(string eventName) => this.Workflow.TryGetEvent(eventName, out _);

    /// <summary>
    /// Determines whether or not the specified <see cref="EventDefinition"/> is of kind <see cref="EventKind.Produced"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="EventDefinition"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> of kind <see cref="EventKind.Produced"/></returns>
    protected virtual bool BeProduced(string name)
    {
        if (!this.Workflow.TryGetEvent(name, out EventDefinition e)) return false;
        return e.Kind == EventKind.Produced;
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="EventDefinition"/> is of kind <see cref="EventKind.Consumed"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="EventDefinition"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> of kind <see cref="EventKind.Consumed"/></returns>
    protected virtual bool BeConsumed(string name)
    {
        if (!this.Workflow.TryGetEvent(name, out EventDefinition e)) return false;
        return e.Kind == EventKind.Consumed;
    }

}
