namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build data-based <see cref="SwitchCaseDefinition"/>
/// </summary>
public interface IDataSwitchCaseBuilder
    : ISwitchCaseBuilder<IDataSwitchCaseBuilder>
{

    /// <summary>
    /// Sets the <see cref="SwitchCaseDefinition"/>'s workflow expression used to evaluate the data
    /// </summary>
    /// <param name="expression">The workflow expression used to evaluate the data</param>
    /// <returns>The configured <see cref="ISwitchStateBuilder"/></returns>
    IDataSwitchCaseBuilder WithExpression(string expression);

    /// <summary>
    /// Builds the <see cref="DataCaseDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="DataCaseDefinition"/></returns>
    new DataCaseDefinition Build();

}
