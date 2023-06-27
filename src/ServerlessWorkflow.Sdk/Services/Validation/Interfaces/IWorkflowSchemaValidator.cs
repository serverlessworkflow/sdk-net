namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Defines the fundamentals of a service used to validate <see cref="WorkflowDefinition"/>s against the adequate version of the <see href="https://serverlessworkflow.io/schemas/latest/workflow.json">Serverless Workflow Specification schema</see>
/// </summary>
public interface IWorkflowSchemaValidator
{

    /// <summary>
    /// Validates the specified <see cref="WorkflowDefinition"/> against the adequate version of the <see href="https://serverlessworkflow.io/schemas/latest/workflow.json">Serverless Workflow Specification schema</see> 
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to validate</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/></param>
    /// <returns>A new <see cref="EvaluationResults"/> that describes the result of the validation</returns>
    Task<EvaluationResults> ValidateAsync(WorkflowDefinition workflow, CancellationToken cancellationToken = default);

}
