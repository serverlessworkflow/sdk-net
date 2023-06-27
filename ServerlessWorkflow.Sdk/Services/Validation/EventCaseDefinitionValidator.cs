using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents a service used to validate <see cref="EventCaseDefinition"/>s
/// </summary>
internal class EventCaseDefinitionValidator
    : SwitchCaseDefinitionValidator<EventCaseDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="EventCaseDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="EventCaseDefinition"/> to validate belongs to</param>
    /// <param name="state">The <see cref="SwitchStateDefinition"/> the <see cref="EventCaseDefinition"/> to validate belongs to</param>
    public EventCaseDefinitionValidator(WorkflowDefinition workflow, SwitchStateDefinition state)
        : base(workflow, state)
    {
        this.RuleFor(c => c.EventRef)
            .NotEmpty()
            .WithErrorCode($"{nameof(EventCaseDefinition)}.{nameof(EventCaseDefinition.EventRef)}");
        this.RuleFor(c => c.EventRef)
            .Must(ReferenceExistingEvent)
            .When(c => !string.IsNullOrWhiteSpace(c.EventRef))
            .WithErrorCode($"{nameof(EventCaseDefinition)}.{nameof(EventCaseDefinition.EventRef)}")
            .WithMessage(e => $"Failed to find an event definition with the specified name '{e.EventRef}'");
    }

    /// <summary>
    /// Determines whether or not the specified <see cref="EventDefinition"/> exists
    /// </summary>
    /// <param name="eventName">The name of the <see cref="EventDefinition"/> to check</param>
    /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> exists</returns>
    protected virtual bool ReferenceExistingEvent(string eventName)
    {
        return this.Workflow.TryGetEvent(eventName, out _);
    }

}
