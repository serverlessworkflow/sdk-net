namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IStateBuilder{TState}"/> interface
/// </summary>
/// <typeparam name="TState">The type of state definition to build</typeparam>
public abstract class StateBuilder<TState>
    : MetadataContainerBuilder<IStateBuilder<TState>>, IStateBuilder<TState>
    where TState : StateDefinition, new()
{

    /// <summary>
    /// Initializes a new <see cref="StateBuilder{TState}"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    protected StateBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <summary>
    /// Gets the state definition to configure
    /// </summary>
    protected TState State { get; set; } = new TState();

    /// <inheritdoc/>
    public override DynamicMapping? Metadata
    {
        get
        {
            return this.State.Metadata;
        }
        protected set
        {
            this.State.Metadata = value;
        }
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> WithName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.State.Name = name;
        return this;
    }

    IStateBuilder IStateBuilder.WithName(string name) => this.WithName(name);

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> WithExtensionProperty(string name, object value)
    {
        this.State.ExtensionData ??= new Dictionary<string, object>();
        this.State.ExtensionData[name] = value;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> WithExtensionProperties(IDictionary<string, object> properties)
    {
        this.State.ExtensionData = properties;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> FilterInput(string expression)
    {
        if (this.State.DataFilter == null) this.State.DataFilter = new();
        this.State.DataFilter.Input = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> FilterOutput(string expression)
    {
        if (this.State.DataFilter == null) this.State.DataFilter = new();
        this.State.DataFilter.Output = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> CompensateWith(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        this.State.CompensatedBy = name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> CompensateWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
    {
        if (stateSetup == null) throw new ArgumentNullException(nameof(stateSetup));
        var compensatedBy = this.Pipeline.AddState(stateSetup);
        compensatedBy.UsedForCompensation = true;
        this.State.CompensatedBy = compensatedBy.Name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> CompensateWith(StateDefinition state)
    {
        if (state == null) throw new ArgumentNullException(nameof(state));
        state.UsedForCompensation = true;
        this.State.CompensatedBy = this.Pipeline.AddState(state).Name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateBuilder<TState> HandleError(Action<IErrorHandlerBuilder> setupAction)
    {
        if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));
        var builder = new ErrorHandlerBuilder(this.Pipeline);
        setupAction(builder);
        var errorHandler = builder.Build();
        if (this.State.Errors == null) this.State.Errors = new();
        this.State.Errors.Add(errorHandler);
        return this;
    }

    /// <inheritdoc/>
    public virtual StateDefinition Build() => this.State;

}
