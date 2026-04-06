namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRetryPolicyDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryPolicyDefinitionBuilder
    : IRetryPolicyDefinitionBuilder
{

    string? retryWhen;
    string? retryExceptWhen;
    RetryPolicyLimitDefinition? retryLimit;
    Duration? retryDelay;
    BackoffStrategyDefinition? retryBackoff;
    JitterDefinition? retryJitter;

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder When(string expression)
    {
        retryWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder ExceptWhen(string expression)
    {
        retryExceptWhen = expression;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Limit(RetryPolicyLimitDefinition limits)
    {
        retryLimit = limits;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Limit(Action<IRetryPolicyLimitDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new RetryPolicyLimitDefinitionBuilder();
        setup(builder);
        return Limit(builder.Build());
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Delay(Duration duration)
    {
        retryDelay = duration;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Backoff(BackoffStrategyDefinition backoff)
    {
        retryBackoff = backoff;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Backoff(Action<IBackoffStrategyDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new BackoffStrategyDefinitionBuilder();
        setup(builder);
        return Backoff(builder.Build());
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Jitter(JitterDefinition jitter)
    {
        retryJitter = jitter;
        return this;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Jitter(Action<IJitterDefinitionBuilder> setup)
    {
        ArgumentNullException.ThrowIfNull(setup);
        var builder = new JitterDefinitionBuilder();
        setup(builder);
        return Jitter(builder.Build());
    }

    /// <inheritdoc/>
    public RetryPolicyDefinition Build() => new()
    {
        When = retryWhen,
        ExceptWhen = retryExceptWhen,
        Limit = retryLimit,
        Delay = retryDelay,
        Backoff = retryBackoff,
        Jitter = retryJitter
    };

}
