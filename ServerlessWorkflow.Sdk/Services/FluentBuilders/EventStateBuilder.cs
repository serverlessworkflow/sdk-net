namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IEventStateBuilder"/> interface
/// </summary>
public class EventStateBuilder
    : StateBuilder<EventStateDefinition>, IEventStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="EventStateBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="EventStateBuilder"/> belongs to</param>
    public EventStateBuilder(IPipelineBuilder pipeline) : base(pipeline) { }

    /// <inheritdoc/>
    public virtual IEventStateBuilder Trigger(Action<IEventStateTriggerBuilder> triggerSetup)
    {
        if (triggerSetup == null) throw new ArgumentNullException(nameof(triggerSetup));
        var builder = new EventStateTriggerBuilder(this.Pipeline);
        triggerSetup(builder);
        this.State.OnEvents.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder WaitForAll()
    {
        this.State.Exclusive = false;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder WaitForAny()
    {
        this.State.Exclusive = true;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder For(TimeSpan duration)
    {
        this.State.Timeout = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventStateBuilder Forever()
    {
        this.State.Timeout = null;
        return this;
    }

}
