namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowRuntime"/>
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/>.</param>
/// <param name="expressionEvaluatorProvider">The service used to provide <see cref="IRuntimeExpressionEvaluator"/>s.</param>
/// <param name="workflowInstanceFactory">The service used to create <see cref="IWorkflowInstance"/>s.</param>
public sealed class WorkflowRuntime(IServiceProvider serviceProvider, IRuntimeExpressionEvaluatorProvider expressionEvaluatorProvider, IWorkflowInstanceFactory workflowInstanceFactory)
    : IWorkflowRuntime
{

    /// <inheritdoc/>
    public RuntimeDescriptor Descriptor { get; } = new()
    {
        Name = "Serverless Workflow Runtime",
        Version = typeof(WorkflowRuntime).Assembly.GetName().Version?.ToString(3) ?? "1.0.0"
    };

    /// <inheritdoc/>
    public async Task<IWorkflowProcess> RunAsync(WorkflowDefinition workflowDefinition, JsonObject input, WorkflowProcessOptions? options = null, CancellationToken cancellationToken = default)
    {
        var instance = workflowInstanceFactory.CreateAsync(workflowDefinition, input);
        var language = workflowDefinition.Evaluate?.Language ?? RuntimeExpressions.Languages.JQ;
        var expressions = expressionEvaluatorProvider.GetEvaluator(language) ?? throw new NullReferenceException($"Failed to find an expression evaluator for the specified language '{language}'");
        var process = ActivatorUtilities.CreateInstance<WorkflowProcess>(serviceProvider, options ?? new(), expressions, instance);
        await process.StartAsync(cancellationToken).ConfigureAwait(false);
        return process;
    }

}
