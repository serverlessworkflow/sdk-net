using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;


/// <summary>
/// Represents a service used to validate <see cref="EventStateDefinition"/>s
/// </summary>
internal class EventStateValidator
    : StateDefinitionValidator<EventStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="EventStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public EventStateValidator(WorkflowDefinition workflow) 
        : base(workflow)
    {
        this.RuleFor(s => s.OnEvents)
            .NotEmpty()
            .WithErrorCode($"{nameof(EventStateDefinition)}.{nameof(EventStateDefinition.OnEvents)}");
        this.RuleForEach(s => s.OnEvents)
            .SetValidator(state => new EventStateTriggerDefinitionValidator(this.Workflow, state))
            .When(s => s.OnEvents != null && s.OnEvents.Any())
            .WithErrorCode($"{nameof(EventStateDefinition)}.{nameof(EventStateDefinition.OnEvents)}");
    }

}
