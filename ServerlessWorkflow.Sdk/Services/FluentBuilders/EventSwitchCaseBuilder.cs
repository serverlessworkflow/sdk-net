namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IEventSwitchCaseBuilder"/> interface
/// </summary>
public class EventSwitchCaseBuilder
    : SwitchCaseBuilder<IEventSwitchCaseBuilder, EventCaseDefinition>, IEventSwitchCaseBuilder
{

    /// <summary>
    /// Initializes a new <see cref="EventSwitchCaseBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="EventSwitchCaseBuilder"/> belongs to</param>
    public EventSwitchCaseBuilder(IPipelineBuilder pipeline) : base(pipeline) { }

    /// <inheritdoc/>
    public virtual IStateOutcomeBuilder On(string e)
    {
        if(string.IsNullOrWhiteSpace(e)) throw new ArgumentNullException(nameof(e));
        this.Case.EventRef = e;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateOutcomeBuilder On(Action<IEventBuilder> eventSetup)
    {
        if (eventSetup == null) throw new ArgumentNullException(nameof(eventSetup));
        var e = this.Pipeline.AddEvent(eventSetup);
        this.Case.EventRef = e.Name;
        return this;
    }

    /// <inheritdoc/>
    public virtual IStateOutcomeBuilder On(EventDefinition e)
    {
        if (e == null) throw new ArgumentNullException(nameof(e));
        this.Pipeline.AddEvent(e);
        this.Case.EventRef = e.Name;
        return this;
    }

    /// <inheritdoc/>
    public virtual new EventCaseDefinition Build()
    {
        var outcome = base.Build();
        switch (outcome)
        {
            case EndDefinition end:
                this.Case.End = end;
                break;
            case TransitionDefinition transition:
                this.Case.Transition = transition;
                break;
            default:
                throw new NotSupportedException($"The specified outcome type '{outcome.GetType().Name}' is not supported");
        }
        return this.Case;
    }

}
