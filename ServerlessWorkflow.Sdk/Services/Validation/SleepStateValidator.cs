namespace ServerlessWorkflow.Sdk.Services.Validation;


/// <summary>
/// Represents a service used to validate <see cref="SleepStateDefinition"/>s
/// </summary>
internal class SleepStateValidator
    : StateDefinitionValidator<SleepStateDefinition>
{

    /// <summary>
    /// Initializes a new <see cref="SleepStateValidator"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    public SleepStateValidator(WorkflowDefinition workflow)
        : base(workflow)
    {
        
    }

}
