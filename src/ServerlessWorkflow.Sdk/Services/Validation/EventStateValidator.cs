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

}
