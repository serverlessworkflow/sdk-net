using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServerlessWorkflow.Sdk.Services.IO;

namespace ServerlessWorkflow.Sdk.Services.Validation;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowValidator"/> interface
/// </summary>
public class WorkflowValidator
    : IWorkflowValidator
{

    /// <summary>
    /// Initializes a new <see cref="WorkflowValidator"/>
    /// </summary>
    /// <param name="logger">The service used to perform logging</param>
    /// <param name="externalDefinitionResolver">The service used to resolve external definitions referenced by <see cref="WorkflowDefinition"/>s</param>
    /// <param name="schemaValidator">The service used to validate <see cref="WorkflowDefinition"/>s</param>
    /// <param name="dslValidators">An <see cref="IEnumerable{T}"/> containing the services used to validate Serverless Workflow DSL</param>
    public WorkflowValidator(ILogger<WorkflowValidator> logger, IWorkflowExternalDefinitionResolver externalDefinitionResolver, IWorkflowSchemaValidator schemaValidator, IEnumerable<IValidator<WorkflowDefinition>> dslValidators)
    {
        this.Logger = logger;
        this.ExternalDefinitionResolver = externalDefinitionResolver;
        this.SchemaValidator = schemaValidator;
        this.DslValidators = dslValidators;
    }

    /// <summary>
    /// Gets the service used to perform logging
    /// </summary>
    protected ILogger Logger { get; }

    /// <summary>
    /// Gets the service used to resolve external definitions referenced by <see cref="WorkflowDefinition"/>s
    /// </summary>
    protected IWorkflowExternalDefinitionResolver ExternalDefinitionResolver { get; }

    /// <summary>
    /// Gets the service used to validate <see cref="WorkflowDefinition"/>s
    /// </summary>
    protected IWorkflowSchemaValidator SchemaValidator { get; }

    /// <summary>
    /// Gets an <see cref="IEnumerable{T}"/> containing the services used to validate Serverless Workflow DSL
    /// </summary>
    protected IEnumerable<IValidator<WorkflowDefinition>> DslValidators { get; }

    /// <inheritdoc/>
    public virtual async Task<IWorkflowValidationResult> ValidateAsync(WorkflowDefinition workflowDefinition, bool validateSchema = true, bool validateDsl = true, CancellationToken cancellationToken = default)
    {
        workflowDefinition = await this.ExternalDefinitionResolver.LoadExternalDefinitionsAsync(workflowDefinition, new(), cancellationToken);
        IEnumerable<KeyValuePair<string, string>>? schemaValidationErrors = null;
        if (validateSchema)
        {
            var evaluationResults = await this.SchemaValidator.ValidateAsync(workflowDefinition, cancellationToken).ConfigureAwait(false);
            if(!evaluationResults.IsValid) schemaValidationErrors = evaluationResults.GetErrors();
        }
        var dslValidationErrors = new List<KeyValuePair<string, string>>();
        if (validateDsl)
        {
            foreach (var dslValidator in this.DslValidators)
            {
                var validationResult = await dslValidator.ValidateAsync(workflowDefinition, cancellationToken);
                if (validationResult.Errors != null) dslValidationErrors.AddRange(validationResult.Errors.Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage)));
            }
        }
        return new WorkflowValidationResult(schemaValidationErrors, dslValidationErrors);
    }

    /// <summary>
    /// Creates a new default instance of the <see cref="IWorkflowValidator"/> interface
    /// </summary>
    /// <returns>A new <see cref="IWorkflowValidator"/></returns>
    public static IWorkflowValidator Create()
    {
        var services = new ServiceCollection();
        services.AddServerlessWorkflow();
        return services.BuildServiceProvider().GetRequiredService<IWorkflowValidator>();
    }

}
