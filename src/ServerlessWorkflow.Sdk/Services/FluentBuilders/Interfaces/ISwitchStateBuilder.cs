namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="SwitchStateDefinition"/>s
/// </summary>
public interface ISwitchStateBuilder
    : IStateBuilder<SwitchStateDefinition>
{

    /// <summary>
    /// Switches on the <see cref="SwitchStateDefinition"/>'s data 
    /// </summary>
    /// <returns>The configured <see cref="IDataSwitchStateBuilder"/></returns>
    IDataSwitchStateBuilder SwitchData();

    /// <summary>
    /// Switches on consumed <see cref="CloudEvent"/>s
    /// </summary>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder SwitchEvents();

    /// <summary>
    /// Configures the <see cref="SwitchStateDefinition"/>'s default case
    /// </summary>
    /// <param name="name">The name of the default case</param>
    /// <param name="outcomeSetup">An action used to configure the outcome of the default case</param>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    ISwitchStateBuilder WithDefaultCase(string name, Action<IStateOutcomeBuilder> outcomeSetup);

}
