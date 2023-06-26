namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="CloudEvent"/>-based <see cref="SwitchStateDefinition"/>
/// </summary>
public interface IEventSwitchStateBuilder
    : ISwitchStateBuilder
{

    /// <summary>
    /// Sets the duration after which the <see cref="SwitchStateDefinition"/>'s execution times out
    /// </summary>
    /// <param name="duration">The duration after which the <see cref="SwitchStateDefinition"/>'s execution times out</param>
    /// <returns>The configured <see cref="IDataSwitchCaseBuilder"/></returns>
    IEventSwitchStateBuilder TimeoutAfter(TimeSpan duration);

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <param name="caseBuilder">The <see cref="Action{T}"/> used to build the <see cref="CloudEvent"/>-based <see cref="SwitchCaseDefinition"/></param>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder WithCase(Action<IEventSwitchCaseBuilder> caseBuilder);

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <param name="name">The name of the case to add</param>
    /// <param name="caseBuilder">The <see cref="Action{T}"/> used to build the <see cref="CloudEvent"/>-based <see cref="SwitchCaseDefinition"/></param>
    /// <returns>The configured <see cref="IEventSwitchStateBuilder"/></returns>
    IEventSwitchStateBuilder WithCase(string name, Action<IEventSwitchCaseBuilder> caseBuilder);

}
