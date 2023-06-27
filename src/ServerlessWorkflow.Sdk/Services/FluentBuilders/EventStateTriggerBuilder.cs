namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IEventStateTriggerBuilder"/> interface
/// </summary>
public class EventStateTriggerBuilder
    : IEventStateTriggerBuilder
{

    /// <summary>
    /// Initializes a new <see cref="EventStateTriggerBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="EventStateTriggerBuilder"/> belongs to</param>
    public EventStateTriggerBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <summary>
    /// Gets the <see cref="EventStateTriggerDefinition"/> to configure
    /// </summary>
    protected EventStateTriggerDefinition Trigger { get; } = new EventStateTriggerDefinition();

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder On(params string[] events)
    {
        if (events != null)
        {
            foreach(string e in events)
            {
                this.Trigger.EventRefs.Add(e);
            }
        }
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder On(params Action<IEventBuilder>[] eventSetups)
    {
        if (eventSetups != null)
        {
            foreach (Action<IEventBuilder> eventSetup in eventSetups)
            {
                this.Trigger.EventRefs.Add(this.Pipeline.AddEvent(eventSetup).Name);
            }
        }
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder On(params EventDefinition[] events)
    {
        if (events != null)
        {
            foreach (EventDefinition e in events)
            {
                this.Trigger.EventRefs.Add(this.Pipeline.AddEvent(e).Name);
            }
        }
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder Execute(ActionDefinition action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action));
        this.Trigger.Actions.Add(action);
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder Execute(Action<IActionBuilder> actionSetup)
    {
        if (actionSetup == null) throw new ArgumentNullException(nameof(actionSetup));
        var builder = new ActionBuilder(this.Pipeline);
        actionSetup(builder);
        return this.Execute(builder.Build());
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder Execute(string name, Action<IActionBuilder> actionSetup)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
        if (actionSetup == null) throw new ArgumentNullException(nameof(actionSetup));
        return this.Execute(a =>
        {
            actionSetup(a);
            a.WithName(name);
        });
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder Sequentially()
    {
        this.Trigger.ActionMode = ActionExecutionMode.Sequential;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder Concurrently()
    {
        this.Trigger.ActionMode = ActionExecutionMode.Parallel;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder FilterPayload(string expression)
    {
        this.Trigger.EventDataFilter.Data = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateTriggerBuilder ToStateData(string expression)
    {
        this.Trigger.EventDataFilter.ToStateData = expression;
        return this;
    }

    /// <inheritdoc/>
    public virtual EventStateTriggerDefinition Build() => this.Trigger;

}
