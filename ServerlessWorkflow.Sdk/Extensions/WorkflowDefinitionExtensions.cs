namespace ServerlessWorkflow.Sdk;

/// <summary>
/// Defines extensions for <see cref="WorkflowDefinition"/>s
/// </summary>
public static class WorkflowDefinitionExtensions
{

    /// <summary>
    /// Gets all the <see cref="ActionDefinition"/>s of the specified type declared in the <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
    /// <param name="type">The type of <see cref="ActionDefinition"/>s to get. A null value gets all <see cref="ActionDefinition"/>s</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="ActionDefinition"/>s of the specified type declared in the <see cref="WorkflowDefinition"/></returns>
    public static IEnumerable<ActionDefinition> GetActions(this WorkflowDefinition workflow, string? type = null)
    {
        var actions = workflow.States.SelectMany(s => s switch
        {
            CallbackStateDefinition callbackState => new ActionDefinition[] { callbackState.Action! },
            EventStateDefinition eventState => eventState.OnEvents.SelectMany(t => t.Actions),
            ForEachStateDefinition foreachState => foreachState.Actions,
            OperationStateDefinition operationState => operationState.Actions,
            ParallelStateDefinition parallelState => parallelState.Branches.SelectMany(b => b.Actions),
            _ => Array.Empty<ActionDefinition>()
        });
        if (!string.IsNullOrWhiteSpace(type)) actions = actions.Where(a => a.Type == type);
        return actions;
    }

    /// <summary>
    /// Gets all the <see cref="FunctionReference"/>s declared in the <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="FunctionReference"/>s declared in the <see cref="WorkflowDefinition"/></returns>
    public static IEnumerable<FunctionReference> GetFunctionReferences(this WorkflowDefinition workflow) => workflow.GetActions(ActionType.Function).Select(a => a.Function)!;

    /// <summary>
    /// Gets all the <see cref="EventReference"/>s declared in the <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="EventReference"/>s declared in the <see cref="WorkflowDefinition"/></returns>
    public static IEnumerable<EventReference> GetEventReferences(this WorkflowDefinition workflow) => workflow.GetActions(ActionType.Event).Select(a => a.Event)!;

    /// <summary>
    /// Gets all the <see cref="SubflowReference"/>s declared in the <see cref="WorkflowDefinition"/>
    /// </summary>
    /// <param name="workflow">The <see cref="WorkflowDefinition"/> to query</param>
    /// <returns>A new <see cref="IEnumerable{T}"/> containing the <see cref="SubflowReference"/>s declared in the <see cref="WorkflowDefinition"/></returns>
    public static IEnumerable<SubflowReference> GetSubflowReferences(this WorkflowDefinition workflow) => workflow.GetActions(ActionType.Subflow).Select(a => a.Subflow)!;

}
