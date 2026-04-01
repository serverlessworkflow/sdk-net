namespace ServerlessWorkflow.Sdk.Runtime;

/// <summary>
/// Defines the fundamentals of the context of a workflow's execution
/// </summary>
public interface IWorkflowExecutionContext
{

    /// <summary>
    /// Gets the <see cref="WorkflowDefinition"/> of the current workflow
    /// </summary>
    WorkflowDefinition Definition { get; }

    /// <summary>
    /// Gets the workflow instance being executed
    /// </summary>
    IWorkflowInstance Instance { get; }

    /// <summary>
    /// Gets/sets the workflow's context data
    /// </summary>
    JsonObject ContextData { get; }

    /// <summary>
    /// Gets/sets the workflow's arguments
    /// </summary>
    JsonObject Arguments { get; }

    /// <summary>
    /// Gets/sets the workflow's output data, if any, in case the workflow ran to completion
    /// </summary>
    JsonObject? Output { get; }

    /// <summary>
    /// Gets the service used to evaluate expressions in the workflow
    /// </summary>
    IRuntimeExpressionEvaluator Expressions { get; }

    /// <summary>
    /// Gets the current workflow's runtime
    /// </summary>
    IWorkflowRuntime Runtime { get; }

}
