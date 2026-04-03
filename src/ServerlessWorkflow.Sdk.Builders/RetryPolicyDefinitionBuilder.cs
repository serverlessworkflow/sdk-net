namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRetryPolicyDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryPolicyDefinitionBuilder
    : IRetryPolicyDefinitionBuilder
{

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    protected string? RetryWhen { get; set; }

    /// <summary>
    /// Gets/sets a runtime expression used to determine whether or not to retry running the task, in a given context
    /// </summary>
    protected string? RetryExceptWhen { get; set; }

    /// <summary>
    /// Gets/sets the parameters, if any, that control the randomness or variability of the delay between retry attempts
    /// </summary>
    protected RetryPolicyLimitDefinition? RetryLimit { get; set; }

    /// <summary>
    /// Gets/sets the delay duration between retry attempts
    /// </summary>
    protected Duration? RetryDelay { get; set; }

    /// <summary>
    /// Gets/sets the limits, if any, of the retry policy to build
    /// </summary>
    protected BackoffStrategyDefinition? RetryBackoff { get; set; }

    /// <summary>
    /// Gets/sets the backoff strategy to use, if any
    /// </summary>
    protected JitterDefinition? RetryJitter { get; set; }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder When(string expression)
    {
        RetryWhen = expression;
        IRetryPolicyDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder ExceptWhen(string expression)
    {
        RetryExceptWhen = expression;
        IRetryPolicyDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Limit(RetryPolicyLimitDefinition limits)
    {
        RetryLimit = limits;
        IRetryPolicyDefinitionBuilder self = this; return self;
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
        RetryDelay = duration;
        IRetryPolicyDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public IRetryPolicyDefinitionBuilder Backoff(BackoffStrategyDefinition backoff)
    {
        RetryBackoff = backoff;
        IRetryPolicyDefinitionBuilder self = this; return self;
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
        RetryJitter = jitter;
        IRetryPolicyDefinitionBuilder self = this; return self;
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
        When = RetryWhen,
        ExceptWhen = RetryExceptWhen,
        Limit = RetryLimit,
        Delay = RetryDelay,
        Backoff = RetryBackoff,
        Jitter = RetryJitter
    };

}
