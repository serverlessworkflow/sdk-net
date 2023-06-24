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
    public SwitchStateBuilder(IPipelineBuilder pipeline)
        : base(pipeline)
    {

    }

    /// <inheritdoc/>
    public virtual IDataSwitchStateBuilder Data()
    {
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventSwitchStateBuilder Events()
    {
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventSwitchStateBuilder Timeout(TimeSpan duration)
    {
        this.State.EventTimeout = duration;
        return this;
    }

    /// <inheritdoc/>
    public virtual IDataSwitchStateBuilder Case(Action<IDataSwitchCaseBuilder> caseSetup)
    {
        if (caseSetup == null)
            throw new ArgumentException(nameof(caseSetup));
        IDataSwitchCaseBuilder builder = new DataSwitchCaseBuilder(this.Pipeline);
        caseSetup(builder);
        this.State.DataConditions = new();
        this.State.DataConditions.Add(builder.Build());
        return this;
    }

    /// <inheritdoc/>
    public virtual IEventSwitchStateBuilder Case(Action<IEventSwitchCaseBuilder> caseSetup)
    {
        if (caseSetup == null)
            throw new ArgumentException(nameof(caseSetup));
        IEventSwitchCaseBuilder builder = new EventSwitchCaseBuilder(this.Pipeline);
        caseSetup(builder);
        this.State.EventConditions.Add(builder.Build());
        return this;
    }

}
