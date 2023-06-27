namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents a service used to validate <see cref="DefaultCaseDefinition"/>s
/// </summary>
public class DefaultCaseDefinitionValidator
    : SwitchCaseDefinitionValidator<DefaultCaseDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="DefaultCaseDefinitionValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> the <see cref="DataCaseDefinition"/> to validate belongs to</param>
    /// <param name="state">The <see cref="SwitchStateDefinition"/> the <see cref="DataCaseDefinition"/> to validate belongs to</param>
    public DefaultCaseDefinitionValidator(WorkflowDefinition workflow, SwitchStateDefinition state)
         : base(workflow, state)
    {
        
    }

}
