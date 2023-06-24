namespace ServerlessWorkflow.Sdk.Services.FluentBuilders;

/// <summary>
/// Defines the fundamentals of a service used to build <see cref="RetryDefinition"/>s
/// </summary>
public interface IRetryStrategyBuilder
{

    /// <summary>
    /// Sets the name of the <see cref="RetryDefinition"/> to build
    /// </summary>
    /// <param name="name">The name of the <see cref="RetryDefinition"/> to build</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithName(string name);

    /// <summary>
    /// Sets the duration between successive retry attempts
    /// </summary>
    /// <param name="duration">The duration to wait between two retry attempts</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithDelayOf(TimeSpan duration);

    /// <summary>
    /// Configures the <see cref="RetryDefinition"/> to not delay successive retry attempts
    /// </summary>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithNoDelay();

    /// <summary>
    /// Configures the <see cref="RetryDefinition"/>'s max delay between retry attempts
    /// </summary>
    /// <param name="duration">The maximum duration to wait between two retry attempt</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithMaxDelay(TimeSpan duration);

    /// <summary>
    /// Configures the maximum amount of retry attempts
    /// </summary>
    /// <param name="maxAttempts">The maximum amount of retry attempts</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder MaxAttempts(uint maxAttempts);

    /// <summary>
    /// Configures the duration which will be added to the delay between successive retries
    /// </summary>
    /// <param name="duration">The duration which will be added to the delay between successive retries</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithDelayIncrementation(TimeSpan duration);

    /// <summary>
    /// Configures the value by which the delay is multiplied before each attempt.
    /// </summary>
    /// <param name="multiplier">The value by which the delay is multiplied before each attempt.</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithDelayMultiplier(float multiplier);

    /// <summary>
    /// Configures the maximum amount of random time added or subtracted from the delay between each retry relative to total delay
    /// </summary>
    /// <param name="multiplier">The maximum amount of random time added or subtracted from the delay between each retry relative to total delay</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithJitterMultiplier(float multiplier);

    /// <summary>
    /// Configures the absolute maximum amount of random time added or subtracted from the delay between each retry
    /// </summary>
    /// <param name="duration">The absolute maximum amount of random time added or subtracted from the delay between each retry</param>
    /// <returns>The configured <see cref="IRetryStrategyBuilder"/></returns>
    IRetryStrategyBuilder WithJitterDuration(TimeSpan duration);

    /// <summary>
    /// Builds the <see cref="RetryDefinition"/>
    /// </summary>
    /// <returns>A new <see cref="RetryDefinition"/></returns>
    RetryDefinition Build();

}
