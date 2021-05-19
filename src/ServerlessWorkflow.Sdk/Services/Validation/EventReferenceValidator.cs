using FluentValidation;
using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
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
            this.RuleFor(e => e.TriggerEvent)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.TriggerEvent)}");
            this.RuleFor(e => e.TriggerEvent)
                .Must(ReferenceExistingEvent)
                .When(e => !string.IsNullOrWhiteSpace(e.TriggerEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.TriggerEvent)}")
                .WithMessage(eventRef => $"Failed to find the event with name '{eventRef}'");
            this.RuleFor(e => e.TriggerEvent)
                .Must(BeProduced)
                .When(e => !string.IsNullOrWhiteSpace(e.TriggerEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.TriggerEvent)}")
                .WithMessage(eventRef => $"The event with name '{eventRef}' must be of kind '{EnumHelper.Stringify(EventKind.Produced)}'");
            this.RuleFor(e => e.ResultEvent)
                .NotEmpty()
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ResultEvent)}");
            this.RuleFor(e => e.ResultEvent)
                .Must(ReferenceExistingEvent)
                .When(e => !string.IsNullOrWhiteSpace(e.ResultEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ResultEvent)}")
                .WithMessage(eventRef => $"Failed to find the event with name '{eventRef}'");
            this.RuleFor(e => e.ResultEvent)
                .Must(BeConsumed)
                .When(e => !string.IsNullOrWhiteSpace(e.ResultEvent))
                .WithErrorCode($"{nameof(EventReference)}.{nameof(EventReference.ResultEvent)}")
                .WithMessage(eventRef => $"The event with name '{eventRef}' must be of kind '{EnumHelper.Stringify(EventKind.Consumed)}'");
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
        protected virtual bool ReferenceExistingEvent(string eventName)
        {
            return this.Workflow.TryGetEvent(eventName, out _);
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="EventDefinition"/> is of kind <see cref="EventKind.Produced"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> of kind <see cref="EventKind.Produced"/></returns>
        protected virtual bool BeProduced(string name)
        {
            if (!this.Workflow.TryGetEvent(name, out EventDefinition e))
                return false;
            return e.Kind == EventKind.Produced;
        }

        /// <summary>
        /// Determines whether or not the specified <see cref="EventDefinition"/> is of kind <see cref="EventKind.Consumed"/>
        /// </summary>
        /// <param name="name">The name of the <see cref="EventDefinition"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="EventDefinition"/> of kind <see cref="EventKind.Consumed"/></returns>
        protected virtual bool BeConsumed(string name)
        {
            if (!this.Workflow.TryGetEvent(name, out EventDefinition e))
                return false;
            return e.Kind == EventKind.Consumed;
        }

    }

}
