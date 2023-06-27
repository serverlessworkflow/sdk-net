namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IStateBuilderFactory"/> interface
/// </summary>
public class StateBuilderFactory
    : IStateBuilderFactory
{

    /// <summary>
    /// Initializes a new <see cref="StateBuilderFactory"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilderFactory"/> belongs to</param>
    public StateBuilderFactory(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="StateBuilderFactory"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <inheritdoc/>
    public virtual ICallbackStateBuilder Callback()
    {
        return new CallbackStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IDelayStateBuilder Delay(TimeSpan duration)
    {
        return this.Delay().For(duration);
    }

    /// <inheritdoc/>
    public virtual IDelayStateBuilder Delay()
    {
        return new SleepStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder Events()
    {
        return new EventStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Execute(ActionDefinition action)
    {
        if (action == null)
            throw new ArgumentNullException(nameof(action));
        IOperationStateBuilder builder = new OperationStateBuilder(this.Pipeline);
        builder.Execute(action);
        return builder;
    }

    /// <inheritdoc/>
    public virtual IOperationStateBuilder Execute(Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null)
            throw new ArgumentNullException(nameof(actionSetup));
        IOperationStateBuilder builder = new OperationStateBuilder(this.Pipeline);
        builder.Execute(actionSetup);
        return builder;
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
    public virtual IParallelStateBuilder ExecuteInParallel()
    {
        return new ParallelStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IForEachStateBuilder ForEach(string inputCollection, string iterationParameter, string outputCollection)
    {
        if (string.IsNullOrWhiteSpace(inputCollection))
            throw new ArgumentNullException(nameof(inputCollection));
        if (string.IsNullOrWhiteSpace(iterationParameter))
            throw new ArgumentNullException(nameof(iterationParameter));
        if (string.IsNullOrWhiteSpace(outputCollection))
            throw new ArgumentNullException(nameof(outputCollection));
        return new ForEachStateBuilder(this.Pipeline)
            .UseInputCollection(inputCollection)
            .UseIterationParameter(iterationParameter)
            .UseOutputCollection(outputCollection);
    }

    /// <inheritdoc/>
    public virtual IInjectStateBuilder Inject()
    {
        return new InjectStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IInjectStateBuilder Inject(object data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        return this.Inject().Data(data);
    }

    /// <inheritdoc/>
    public virtual IDataSwitchStateBuilder Switch()
    {
        return new SwitchStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IEventSwitchStateBuilder SwitchEvents()
    {
        return new SwitchStateBuilder(this.Pipeline);
    }

    /// <inheritdoc/>
    public virtual IExtensionStateBuilder Extension(string type)
    {
        if(string.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type));
        return new ExtensionStateBuilder(this.Pipeline, type);
    }

}
