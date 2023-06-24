namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="SleepStateDefinition"/>s
/// </summary>
public interface IDelayStateBuilder
    : IStateBuilder<SleepStateDefinition>
{

    /// <summary>
    /// Configures the duration of the workflow execution's delay
    /// </summary>
    /// <param name="duration">The duration of the workflow execution's delay</param>
    /// <returns>The configured <see cref="IDelayStateBuilder"/></returns>
    IDelayStateBuilder For(TimeSpan duration);

}
