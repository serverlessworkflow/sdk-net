namespace ServerlessWorkflow.Sdk.Runtime.Services;

/// <summary>
/// Represents the default implementation of the <see cref="IWorkflowExecutionContextFactory"/> interface
/// </summary>
/// <param name="serviceProvider">The current <see cref="IServiceProvider"/></param>
/// <param name="expressionEvaluatorProvider">The service used to resolve <see cref="IRuntimeExpressionEvaluator"/>s based on the specified language</param>
public sealed class WorkflowExecutionContextFactory(IServiceProvider serviceProvider, IRuntimeExpressionEvaluatorProvider expressionEvaluatorProvider)
    : IWorkflowExecutionContextFactory
{

    /// <inheritdoc/>
    public IWorkflowExecutionContext Create(WorkflowDefinition definition, IWorkflowState state, WorkflowExecutionsOptions executionsOptions)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(executionsOptions);
        var language = definition.Evaluate?.Language ?? RuntimeExpressions.Languages.JQ;
        var expressionEvaluator = expressionEvaluatorProvider.GetEvaluator(language) ?? throw new NullReferenceException($"Failed to resolve an expression evaluator for the specified language '{language}'");
        return ActivatorUtilities.CreateInstance<WorkflowExecutionContext>(serviceProvider, definition, state, executionsOptions, expressionEvaluator);
    }

}
