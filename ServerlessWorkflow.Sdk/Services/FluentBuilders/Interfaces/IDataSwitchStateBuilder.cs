namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="SwitchStateDefinition"/>s
/// </summary>
public interface IDataSwitchStateBuilder
    : ISwitchStateBuilder
{

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <param name="caseBuilder">The <see cref="Action{T}"/> used to build the data-based <see cref="SwitchCaseDefinition"/></param>
    /// <returns>The configured <see cref="IDataSwitchCaseBuilder"/></returns>
    IDataSwitchStateBuilder WithCase(Action<IDataSwitchCaseBuilder> caseBuilder);

    /// <summary>
    /// Creates and configures a new data-based <see cref="SwitchCaseDefinition"/>
    /// </summary>
    /// <param name="name">The name of the <see cref="SwitchCaseDefinition"/> to add</param>
    /// <param name="caseBuilder">The <see cref="Action{T}"/> used to build the data-based <see cref="SwitchCaseDefinition"/></param>
    /// <returns>The configured <see cref="IDataSwitchCaseBuilder"/></returns>
    IDataSwitchStateBuilder WithCase(string name, Action<IDataSwitchCaseBuilder> caseBuilder);

}
