using FluentValidation;
using ServerlessWorkflow.Sdk.Models;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{

    /// <summary>
    /// Represents a service used to validate <see cref="EventStateDefinition"/>s
    /// </summary>
    public class EventStateValidator
        : StateDefinitionValidator<EventStateDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="EventStateValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        public EventStateValidator(WorkflowDefinition workflow) 
            : base(workflow)
        {
            this.RuleFor(s => s.Triggers)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventStateDefinition)}.{nameof(EventStateDefinition.Triggers)}");
            this.RuleForEach(s => s.Triggers)
                .SetValidator(state => new EventStateTriggerDefinitionValidator(this.Workflow, state))
                .When(s => s.Triggers != null && s.Triggers.Any())
                .WithErrorCode($"{nameof(EventStateDefinition)}.{nameof(EventStateDefinition.Triggers)}");
        }

    }

    /// <summary>
    /// Represents a service used to validate <see cref="EventStateTriggerDefinition"/>s
    /// </summary>
    public class EventStateTriggerDefinitionValidator
        : AbstractValidator<EventStateTriggerDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="EventStateTriggerDefinitionValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to</param>
        /// <param name="eventState">The <see cref="EventStateDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to</param>
        public EventStateTriggerDefinitionValidator(WorkflowDefinition workflow, EventStateDefinition eventState)
        {
            this.Workflow = workflow;
            this.EventState = eventState;
            this.RuleFor(t => t.Actions)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Actions)}");
            this.RuleForEach(t => t.Actions)
                .SetValidator(new ActionDefinitionValidator(this.Workflow))
                .When(t => t.Actions != null && t.Actions.Any())
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Actions)}");
            this.RuleFor(t => t.Events)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Events)}");
            this.RuleForEach(t => t.Events)
                .Must(ReferenceExistingEvent)
                .When(t => t.Events != null && t.Events.Any())
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Events)}")
                .WithMessage(eventRef => $"Failed to find an event with name '{eventRef}'");
            this.RuleForEach(t => t.Events)
                .Must(ReferenceExistingEvent)
                .When(t => t.Events != null && t.Events.Any())
                .WithErrorCode($"{nameof(EventStateTriggerDefinition)}.{nameof(EventStateTriggerDefinition.Events)}")
                .WithMessage(eventRef => $"Failed to find an event with name '{eventRef}'");
        }

        /// <summary>
        /// Gets the <see cref="WorkflowDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to
        /// </summary>
        protected WorkflowDefinition Workflow { get; }

        /// <summary>
        /// Gets the <see cref="EventStateDefinition"/> the <see cref="EventStateTriggerDefinition"/> to validate belongs to
        /// </summary>
        protected EventStateDefinition EventState { get; }

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

}
