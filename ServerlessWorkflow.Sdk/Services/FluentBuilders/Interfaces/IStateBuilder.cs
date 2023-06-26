namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to configure a state definition
/// </summary>
public interface IStateBuilder
{

    /// <summary>
    /// Sets the name of the state definition to build
    /// </summary>
    /// <param name="name">The name of the state definition to build</param>
    /// <returns>The configured <see cref="IStateBuilder"/></returns>
    IStateBuilder WithName(string name);

    /// <summary>
    /// Builds the state definition
    /// </summary>
    /// <returns>A new state definition</returns>
    StateDefinition Build();

}

/// <summary>
/// Defines the fundamentals of a service used to configure a state definition
/// </summary>
/// <typeparam name="TState">The type of state definition to build</typeparam>
public interface IStateBuilder<TState>
    : IStateBuilder, IMetadataContainerBuilder<IStateBuilder<TState>>, IExtensibleBuilder<IStateBuilder<TState>>
    where TState : StateDefinition, new()
{

    /// <summary>
    /// Sets the name of the state definition to build
    /// </summary>
    /// <param name="name">The name of the state definition to build</param>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    new IStateBuilder<TState> WithName(string name);

    /// <summary>
    /// Filters the state definition's input
    /// </summary>
    /// <param name="expression">The workflow expression used to filter the state definition's input</param>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    IStateBuilder<TState> FilterInput(string expression);

    /// <summary>
    /// Filters the state definition's output
    /// </summary>
    /// <param name="expression">The workflow expression used to filter the state definition's output</param>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    IStateBuilder<TState> FilterOutput(string expression);

    /// <summary>
    /// Configures the handling for the specified error
    /// </summary>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    IStateBuilder<TState> HandleError(Action<IErrorHandlerBuilder> builder);

    /// <summary>
    /// Compensates the state definition with the specified state definition
    /// </summary>
    /// <param name="name">The name of the state definition to use for compensation</param>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    IStateBuilder<TState> CompensateWith(string name);

    /// <summary>
    /// Compensates the state definition with the specified state definition
    /// </summary>
    /// <param name="stateSetup">A <see cref="Func{T, TResult}"/> used to create the state definition to use for compensation</param>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    IStateBuilder<TState> CompensateWith(Func<IStateBuilderFactory, IStateBuilder> stateSetup);

    /// <summary>
    /// Compensates the state definition with the specified state definition
    /// </summary>
    /// <param name="state">Tthe state definition to use for compensation</param>
    /// <returns>The configured <see cref="IStateBuilder{TState}"/></returns>
    IStateBuilder<TState> CompensateWith(StateDefinition state);

}
