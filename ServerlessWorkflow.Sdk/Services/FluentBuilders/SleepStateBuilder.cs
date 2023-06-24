namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;


/// <summary>
/// Represents the default implementation of the <see cref="IDelayStateBuilder"/> interface
/// </summary>
public class SleepStateBuilder
    : StateBuilder<SleepStateDefinition>, IDelayStateBuilder
{

    /// <summary>
    /// Initializes a new <see cref="CallbackStateDefinition"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateBuilder{TState}"/> belongs to</param>
    public SleepStateBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {

    }

    /// <inheritdoc/>
    public virtual IDelayStateBuilder For(TimeSpan duration)
    {
        this.State.Duration = duration;
        return this;
    }

}
