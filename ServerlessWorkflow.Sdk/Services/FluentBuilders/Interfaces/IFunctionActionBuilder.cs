namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the service used to build <see cref="ActionDefinition"/>s of type <see cref="ActionType.Function"/>
/// </summary>
public interface IFunctionActionBuilder
    : IActionBuilder, IExtensibleBuilder<IFunctionActionBuilder>
{

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use the specified <see href="https://spec.graphql.org/June2018/#sec-Selection-Sets">GraphQL selection set</see>
    /// </summary>
    /// <param name="selectionSet">The <see href="https://spec.graphql.org/June2018/#sec-Selection-Sets">GraphQL selection set</see> to use</param>
    /// <remarks>Only supported for <see cref="FunctionDefinition"/>s of type <see cref="FunctionType.GraphQL"/></remarks>
    /// <returns>The configured <see cref="IFunctionActionBuilder"/></returns>
    IFunctionActionBuilder WithSelectionSet(string selectionSet);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use the specified argument when performing the function call
    /// </summary>
    /// <param name="name">The name of the argument to add</param>
    /// <param name="value">The value or workflow expression of the argument to add</param>
    /// <returns>The configured <see cref="IFunctionActionBuilder"/></returns>
    IFunctionActionBuilder WithArgument(string name, object value);

    /// <summary>
    /// Configures the <see cref="FunctionDefinition"/> to use the specified argument when performing the function call
    /// </summary>
    /// <param name="args">An <see cref="IDictionary{TKey, TValue}"/> containing the name/value pairs of the arguments to use</param>
    /// <returns>The configured <see cref="IFunctionActionBuilder"/></returns>
    IFunctionActionBuilder WithArguments(IDictionary<string, object> args);

}
