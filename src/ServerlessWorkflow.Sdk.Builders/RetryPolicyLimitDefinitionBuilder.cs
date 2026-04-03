namespace ServerlessWorkflow.Sdk.Builders;

/// <summary>
/// Represents the default implementation of the <see cref="IRetryPolicyLimitDefinitionBuilder"/> interface
/// </summary>
public sealed class RetryPolicyLimitDefinitionBuilder
    : IRetryPolicyLimitDefinitionBuilder
{

    /// <summary>
    /// Gets the service used to build the definition of the limits for all retry attempts of a given policy
    /// </summary>
    protected IRetryAttemptLimitDefinitionBuilder? LimitAttempt { get; set; }

    /// <summary>
    /// Gets the maximum duration during which retrying is allowed
    /// </summary>
    protected Duration? LimitDuration { get; set; }

    /// <inheritdoc/>
    public IRetryAttemptLimitDefinitionBuilder Attempt()
    {
        LimitAttempt = new RetryAttemptLimitDefinitionBuilder();
        return LimitAttempt;
    }

    /// <inheritdoc/>
    public IRetryPolicyLimitDefinitionBuilder Duration(Duration duration)
    {
        LimitDuration = duration;
        IRetryPolicyLimitDefinitionBuilder self = this; return self;
    }

    /// <inheritdoc/>
    public RetryPolicyLimitDefinition Build() => new()
    {
        Attempt = LimitAttempt?.Build(),
        Duration = LimitDuration,
    };

}
