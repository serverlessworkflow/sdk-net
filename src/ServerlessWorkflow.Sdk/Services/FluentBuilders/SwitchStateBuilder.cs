namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IDataSwitchStateBuilder"/> interface
/// </summary>
public class SwitchStateBuilder
    : StateBuilder<SwitchStateDefinition>, IDataSwitchStateBuilder, IEventSwitchStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="SwitchStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public SwitchStateBuilder(IPipelineBuilder pipeline) : base(pipeline) { }

    /// <inheritdoc/>
    public virtual IDataSwitchStateBuilder SwitchData() => this;

    /// <inheritdoc/>
    public virtual IEventSwitchStateBuilder SwitchEvents() => this;

    /// <inheritdoc/>
    public ISwitchStateBuilder WithDefaultCase(string name, Action<IStateOutcomeBuilder> outcomeSetup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (outcomeSetup == null) throw new ArgumentNullException(nameof(outcomeSetup));
        this.State.DefaultCondition = new() { Name = name };
        var outcomeBuilder = new StateOutcomeBuilder(this.Pipeline);
        outcomeSetup(outcomeBuilder);
        var outcome = outcomeBuilder.Build();
        switch (outcome)
        {
            case EndDefinition end:
                this.State.DefaultCondition.End = end;
                break;
            case TransitionDefinition transition:
                this.State.DefaultCondition.Transition = transition;
                break;
            default:
                throw new NotSupportedException($"The specified outcome type '{outcome.GetType().Name}' is not supported");
        }
        return this;
    }

    IDataSwitchStateBuilder IDataSwitchStateBuilder.WithCase(Action<IDataSwitchCaseBuilder> caseSetup)
    {
        if (caseSetup == null) throw new ArgumentException(nameof(caseSetup));
        var builder = new DataSwitchCaseBuilder(this.Pipeline);
        caseSetup(builder);
        this.State.DataConditions = new()
        {
            builder.Build()
        };
        return this;
    }

    /// <inheritdoc/>
    IDataSwitchStateBuilder IDataSwitchStateBuilder.WithCase(string name, Action<IDataSwitchCaseBuilder> caseSetup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (caseSetup == null) throw new ArgumentException(nameof(caseSetup));
        var builder = new DataSwitchCaseBuilder(this.Pipeline).WithName(name);
        caseSetup(builder);
        this.State.DataConditions ??= new();
        this.State.DataConditions.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    IEventSwitchStateBuilder IEventSwitchStateBuilder.WithCase(Action<IEventSwitchCaseBuilder> caseSetup)
    {
        if (caseSetup == null) throw new ArgumentException(nameof(caseSetup));
        var builder = new EventSwitchCaseBuilder(this.Pipeline);
        caseSetup(builder);
        this.State.EventConditions ??= new();
        this.State.EventConditions.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    IEventSwitchStateBuilder IEventSwitchStateBuilder.WithCase(string name, Action<IEventSwitchCaseBuilder> caseSetup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (caseSetup == null) throw new ArgumentException(nameof(caseSetup));
        var builder = new EventSwitchCaseBuilder(this.Pipeline).WithName(name);
        caseSetup(builder);
        this.State.EventConditions ??= new();
        this.State.EventConditions.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    IEventSwitchStateBuilder IEventSwitchStateBuilder.TimeoutAfter(TimeSpan duration)
    {
        this.State.EventTimeout = duration;
        return this;
    }


}
