namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Represents the default implementation of the <see cref="IStateOutcomeBuilder"/> interface
/// </summary>
public class StateOutcomeBuilder
    : IStateOutcomeBuilder
{

    /// <summary>
    /// Initializes a new <see cref="StateOutcomeBuilder"/>
    /// </summary>
    /// <param name="pipeline">The <see cref="IPipelineBuilder"/> the <see cref="StateOutcomeBuilder"/> belongs to</param>
    public StateOutcomeBuilder(IPipelineBuilder pipeline)
    {
        this.Pipeline = pipeline;
    }

    /// <summary>
    /// Gets the <see cref="IPipelineBuilder"/> the <see cref="IStateOutcomeBuilder"/> belongs to
    /// </summary>
    protected IPipelineBuilder Pipeline { get; }

    /// <summary>
    /// Gets the <see cref="StateOutcomeDefinition"/> to configure
    /// </summary>
    protected StateOutcomeDefinition Outcome { get; set; } = null!;

    /// <inheritdoc/>
    public virtual void TransitionTo(Func<IStateBuilderFactory, IStateBuilder> stateSetup)
    {
        //TODO: configure transition
        StateDefinition state = this.Pipeline.AddState(stateSetup);
        this.Outcome = new TransitionDefinition() { NextState = state.Name };
    }

    /// <inheritdoc/>
    public virtual void End()
    {
        //TODO: configure end
        this.Outcome = new EndDefinition();
    }

    /// <inheritdoc/>
    public virtual StateOutcomeDefinition Build()
    {
        return this.Outcome;
    }

}
