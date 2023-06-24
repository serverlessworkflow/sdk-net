namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="InjectStateDefinition"/>s
/// </summary>
public interface IInjectStateBuilder
    : IStateBuilder<InjectStateDefinition>
{

    /// <summary>
    /// Injects the specified data into the workflow
    /// </summary>
    /// <param name="data">The data to inject</param>
    /// <returns>A new <see cref="IInjectStateBuilder"/></returns>
    IInjectStateBuilder Data(object data);

}
