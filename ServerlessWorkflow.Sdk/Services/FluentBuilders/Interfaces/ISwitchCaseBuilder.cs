namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="SwitchCaseDefinition"/>s
/// </summary>
public interface ISwitchCaseBuilder<TBuilder>
    : IStateOutcomeBuilder
    where TBuilder : ISwitchCaseBuilder<TBuilder>
{

    /// <summary>
    /// Sets the <see cref="SwitchCaseDefinition"/>'s name
    /// </summary>
    /// <param name="name">The name of the <see cref="SwitchCaseDefinition"/> to build</param>
    /// <returns>The configured <see cref="ISwitchStateBuilder"/></returns>
    TBuilder WithName(string name);

}
