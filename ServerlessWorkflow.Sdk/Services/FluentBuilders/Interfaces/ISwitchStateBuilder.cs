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
    IDataSwitchStateBuilder Data();

    /// <summary>
    /// Switches on consumed <see cref="CloudEvent"/>s
    /// </summary>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder Events();

}
