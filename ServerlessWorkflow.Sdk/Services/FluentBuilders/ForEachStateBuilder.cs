namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IForEachStateBuilder"/> interface
/// </summary>
public class ForEachStateBuilder
    : StateBuilder<ForEachStateDefinition>, IForEachStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="ForEachStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public ForEachStateBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {

    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder Execute(ActionDefinition action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        this.State.Actions.Add(action);
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder Execute(Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null)
            throw new ArgumentNullException(nameof(actionSetup));
        IActionBuilder actionBuilder = new ActionBuilder(this.Pipeline);
        actionSetup(actionBuilder);
        this.State.Actions.Add(actionBuilder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder Execute(string name, Action<IActionBuilder> actionSetup)
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
    public virtual IForEachStateBuilder Concurrently()
    {
        this.State.Mode = ActionExecutionMode.Parallel;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder Sequentially()
    {
        this.State.Mode = ActionExecutionMode.Sequential;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder UseInputCollection(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentNullException(nameof(expression));
        this.State.InputCollection = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder UseIterationParameter(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentNullException(nameof(expression));
        this.State.IterationParam = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder UseOutputCollection(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            throw new ArgumentNullException(nameof(expression));
        this.State.OutputCollection = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder WithBatchSize(int? batchSize)
    {
        this.State.BatchSize = batchSize;
        return this;
    }

}
