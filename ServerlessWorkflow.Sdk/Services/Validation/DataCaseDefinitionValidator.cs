using FluentValidation;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents a service used to validate <see cref="DataCaseDefinition"/>s
/// </summary>
public class DataCaseDefinitionValidator
    : SwitchCaseDefinitionValidator<DataCaseDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="DataCaseDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="DataCaseDefinition"/> to validate belongs to</param>
    /// <param name="state">The <see cref="SwitchStateDefinition"/> the <see cref="DataCaseDefinition"/> to validate belongs to</param>
    public DataCaseDefinitionValidator(WorkflowDefinition workflow, SwitchStateDefinition state)
         : base(workflow, state)
    {
        this.RuleFor(c => c.Condition)
            .NotEmpty()
            .WithErrorCode($"{nameof(DataCaseDefinition)}.{nameof(DataCaseDefinition.Condition)}");
    }

}
