using ServerlessWorkflow.Sdk.Models;

namespace ServerlessWorkflow.Sdk.Services.Validation
{
    /// <summary>
    /// Represents a service used to validate <see cref="DelayStateDefinition"/>s
    /// </summary>
    public class DelayStateValidator
        : StateDefinitionValidator<DelayStateDefinition>
    {

        /// <summary>
        /// Initializes a new <see cref="DelayStateValidator"/>
        /// </summary>
        /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
        public DelayStateValidator(WorkflowDefinition workflow)
            : base(workflow)
        {
            
        }

    }

}
