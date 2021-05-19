using FluentValidation;
using ServerlessWorkflow.Sdk.Models;
using System.Linq;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents a service used to validate <see cref="SwitchStateDefinition"/>s
    /// </summary>
    public class SwitchStateValidator
        : StateDefinitionValidator<SwitchStateDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="SwitchStateValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        public SwitchStateValidator(WorkflowDefinition workflow)
            : base(workflow)
        {
            this.RuleFor(s => s.DataConditions)
                .NotEmpty()
                .When(s => s.EventConditions == null)
                .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DataConditions)}")
                .WithMessage($"One of either '{nameof(SwitchStateDefinition.DataConditions)}' or '{nameof(SwitchStateDefinition.EventConditions)}' properties must be set");
            this.RuleForEach(s => s.DataConditions)
                .SetValidator(state => new DataCaseDefinitionValidator(this.Workflow, state))
                .When(s => s.DataConditions != null && s.DataConditions.Any())
                .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.DataConditions)}");
            this.RuleFor(s => s.EventConditions)
                .NotEmpty()
                .When(s => s.DataConditions == null)
                .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.EventConditions)}")
                .WithMessage($"One of either '{nameof(SwitchStateDefinition.DataConditions)}' or '{nameof(SwitchStateDefinition.EventConditions)}' properties must be set");
            this.RuleForEach(s => s.EventConditions)
                .SetValidator(state => new EventCaseDefinitionValidator(this.Workflow, state))
                .When(s => s.EventConditions != null && s.EventConditions.Any())
                .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.EventConditions)}");
            this.RuleFor(s => s.Default)
                .NotNull()
                .WithErrorCode($"{nameof(SwitchStateDefinition)}.{nameof(SwitchStateDefinition.Default)}");
 
        }

    }

}
