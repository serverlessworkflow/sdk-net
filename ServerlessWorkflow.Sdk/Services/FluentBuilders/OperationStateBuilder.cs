namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IOperationStateBuilder"/> interface
/// </summary>
public class OperationStateBuilder
    : StateBuilder<OperationStateDefinition>, IOperationStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="OperationStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public OperationStateBuilder(IPipelineBuilder pipeline) 
        : base(pipeline)
    {
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Execute(ActionDefinition action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        this.State.Actions.Add(action);
        return this;
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Execute(Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null)
            throw new ArgumentNullException(nameof(actionSetup));
        IActionBuilder actionBuilder = new ActionBuilder(this.Pipeline);
        actionSetup(actionBuilder);
        this.State.Actions.Add(actionBuilder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Execute(string name, Action<IActionBuilder> actionSetup)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        if (actionSetup == null)
            throw new ArgumentNullException(nameof(actionSetup));
        return this.Execute(a =>
        {
            actionSetup(a);
            a.WithName(name);
        });
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Concurrently()
    {
        this.State.ActionMode = ActionExecutionMode.Parallel;
        return this;
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Sequentially()
    {
        this.State.ActionMode = ActionExecutionMode.Sequential;
        return this;
    }

}
