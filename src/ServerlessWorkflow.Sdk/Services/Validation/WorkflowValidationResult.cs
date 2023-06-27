namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowValidationResult"/>
/// </summary>
public class WorkflowValidationResult
    : IWorkflowValidationResult
{

    /// <summary>
    /// Inherits a new <see cref="WorkflowValidationResult"/>
    /// </summary>
    protected WorkflowValidationResult() { }

    /// <summary>
    /// Inherits a new <see cref="WorkflowValidationResult"/>
    /// </summary>
    /// <param name="schemaValidationErrors">An <see cref="IEnumerable{T}"/> containing the schema-related validation errors  that have occured while validating the read <see cref="WorkflowDefinition"/></param>
    /// <param name="dslValidationErrors">An <see cref="IEnumerable{T}"/> containing the Serverless Workflow DSL-related validation errors that have occured while validating the read <see cref="WorkflowDefinition"/></param>
    public WorkflowValidationResult(IEnumerable<KeyValuePair<string, string>>? schemaValidationErrors, IEnumerable<KeyValuePair<string, string>>? dslValidationErrors)
    {
        this.SchemaValidationErrors = schemaValidationErrors;
        this.DslValidationErrors = dslValidationErrors;
    }

    /// <inheritdoc/>
    public virtual IEnumerable<KeyValuePair<string, string>>? SchemaValidationErrors { get; }

    /// <inheritdoc/>
    public virtual IEnumerable<KeyValuePair<string, string>>? DslValidationErrors { get; }

    /// <inheritdoc/>
    public virtual bool IsValid => this.SchemaValidationErrors?.Any() != true && this.DslValidationErrors?.Any() != true;

}
