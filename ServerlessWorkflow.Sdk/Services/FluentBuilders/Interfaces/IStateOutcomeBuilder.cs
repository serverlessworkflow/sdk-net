namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="StateOutcomeDefinition"/>s
/// </summary>
public interface IStateOutcomeBuilder
{

    /// <summary>
    /// Transitions to the specified state definition
    /// </summary>
    /// <param name="stateSetup">An <see cref="Func{T, TResult}"/> used to setup the state definition to transition to</param>
    /// <returns>A new <see cref="IStateBuilder{TState}"/> used to configure the state definition to transition to</returns>
    void TransitionTo(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

    /// <summary>
    /// Configure the state definition to end the workflow
    /// </summary>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    void End();

    /// <summary>
    /// Builds the <see cref="StateOutcomeDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="StateOutcomeDefinition"/></returns>
    StateOutcomeDefinition Build();

}
